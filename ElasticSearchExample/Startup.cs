using System;
using MassTransit;
using MessageBus.Core.Configuration;
using MessageBus.Core.Messages;
using MessageBus.Core.Services;
using MessageBus.Core.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Search.Core.ClientCreator;
using Search.Core.ClientCreator.Interfaces;
using Search.Core.Configuration;
using Search.Core.Configuration.Interfaces;
using Search.Core.Services;
using Search.Core.Services.Interfaces;

namespace ElasticSearchExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ElasticSearchExample",
                    Version = "v1"
                });
            });

            ConfigureMassTransitServices(services);

            services.AddTransient<IRecordSearchService, RecordSearchService>();
            services.AddTransient<IMessageBusService, MessageBusService>();
            services.AddTransient<IRecordDocumentService, RecordDocumentService>();
            services.AddTransient<ISearchConfiguration, SearchConfiguration>();
            services.AddTransient<IClientCreator, ClientCreator>();
        }

        private void ConfigureMassTransitServices(IServiceCollection services)
        {
            var messageBusConfiguration = new MessageBusConfiguration(Configuration);

            services.AddMassTransit(serviceCollectionBusConfigurator =>
            {
                serviceCollectionBusConfigurator.UsingRabbitMq((_, rabbitMqBusFactoryConfigurator) =>
                {
                    rabbitMqBusFactoryConfigurator.Host(
                        messageBusConfiguration.Hostname,
                        messageBusConfiguration.Port,
                        messageBusConfiguration.VirtualHost,
                        null);
                });

                EndpointConvention.Map<CreateRecordMessageBusMessage>(new Uri($"queue:{messageBusConfiguration.SearchRecordQueueName}"));
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElasticSearchExample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}