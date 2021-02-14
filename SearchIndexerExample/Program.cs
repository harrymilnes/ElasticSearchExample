using System.Threading.Tasks;
using MassTransit;
using MessageBus.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchIndexerExample.Consumers;
using Microsoft.Extensions.Hosting;
using Search.Core.ClientCreator;
using Search.Core.ClientCreator.Interfaces;
using Search.Core.Configuration;
using Search.Core.Configuration.Interfaces;
using Search.Core.Services;
using Search.Core.Services.Interfaces;
using SearchIndexerExample.HostedServices;

namespace SearchIndexerExample
{
    internal static class Program
    {
        private static async Task Main()
        {
            var hostBuilder = ConfigureServices();
            await hostBuilder.UseSystemd().StartAsync();
        }
        
        private static HostBuilder ConfigureServices()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile("appsettings.json", true);
            });
            
            hostBuilder.ConfigureServices((hostContext, serviceCollection) =>
            {
                var messageBusConfiguration = new MessageBusConfiguration(hostContext.Configuration);
                
                serviceCollection.AddMassTransit(serviceCollectionBusConfigurator =>
                {
                    serviceCollectionBusConfigurator.AddConsumer<CreateRecordConsumer>().Endpoint(consumerEndpointRegistrationConfigurator =>
                    {
                        consumerEndpointRegistrationConfigurator.Name = messageBusConfiguration.SearchRecordQueueName;
                    });
                    
                    serviceCollectionBusConfigurator.UsingRabbitMq((provider, rabbitMqBusFactoryConfigurator) =>
                    {
                        rabbitMqBusFactoryConfigurator.Host(
                            messageBusConfiguration.Hostname,
                            messageBusConfiguration.Port,
                            messageBusConfiguration.VirtualHost,
                            null);
                        
                        rabbitMqBusFactoryConfigurator.ConfigureEndpoints(provider);
                    });
                });

                serviceCollection.AddHostedService<MessageBusHostedService>();
                serviceCollection.AddTransient<ISearchConfiguration, SearchConfiguration>();
                serviceCollection.AddTransient<IClientCreator, ClientCreator>();
                serviceCollection.AddTransient<IRecordDocumentService, RecordDocumentService>();
            });
            

            return hostBuilder;
        }
    }
}