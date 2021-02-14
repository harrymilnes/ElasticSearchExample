using MessageBus.Core.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MessageBus.Core.Configuration
{
    public class MessageBusConfiguration : IMessageBusConfiguration
    {
        private readonly IConfiguration _configuration;

        public MessageBusConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string Hostname => GetHostname();
        private string GetHostname()
        {
            return _configuration.GetValue<string>("MessageBus:Hostname");
        }

        public ushort Port => GetPort();
        private ushort GetPort()
        {
            return _configuration.GetValue<ushort>("MessageBus:Port");
        }

        public string VirtualHost => GetVirtualHost();
        private string GetVirtualHost()
        {
            return _configuration.GetValue<string>("MessageBus:VirtualHost");
        }
        
        public string SearchRecordQueueName => GetSearchRecordQueueName();
        private string GetSearchRecordQueueName()
        {
            return _configuration.GetValue<string>("MessageBus:SearchRecordQueueName");
        }
    }
}