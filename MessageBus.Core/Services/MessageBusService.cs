using System;
using System.Threading.Tasks;
using MassTransit;
using MessageBus.Core.Messages;
using MessageBus.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessageBus.Core.Services
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ILogger<MessageBusService> _logger;
        private readonly IBusControl _busControl;
        
        public MessageBusService(
            ILogger<MessageBusService> logger,
            IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        public async Task SendCreateRecordMessageAsync(CreateRecordMessageBusMessage messageBusMessage)
        {
            try
            {
                await _busControl.Send(messageBusMessage);
            }
            catch(Exception exception)
            {
                _logger.Log(LogLevel.Critical, exception, exception.Message);
            }
        }
    }
}