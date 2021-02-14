using MessageBus.Core.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MessageBus.Core.Configuration
{
    public class MessageBusConfiguration : IMessageBusConfiguration
    {
        public string Hostname { get; private set; }
        public int Port { get; private set;  }

        private readonly IConfiguration _configuration;

        public MessageBusConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        //TODO: Pull configuration from AppSettings.
    }
}