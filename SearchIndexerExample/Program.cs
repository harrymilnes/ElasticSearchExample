using System.Threading.Tasks;
using MassTransit;
using MessageBus.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchIndexerExample.Consumers;
using Microsoft.Extensions.Hosting;
using SearchIndexerExample.HostedServices;

namespace SearchIndexerExample
{
    class Program
    {
        public static async Task Main()
        {
            var host = ConfigureServices();
            await host.UseSystemd().StartAsync();
        }
        
        private static HostBuilder ConfigureServices()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
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
            });
            

            return hostBuilder;
        }
    }
}