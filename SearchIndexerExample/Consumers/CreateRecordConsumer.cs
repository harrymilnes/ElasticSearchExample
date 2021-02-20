using System.Threading.Tasks;
using MassTransit;
using MessageBus.Core.Messages;
using Microsoft.Extensions.Logging;
using Search.Core.Documents;
using Search.Core.Services.Interfaces;

namespace SearchIndexerExample.Consumers
{
    public class CreateRecordConsumer : IConsumer<CreateRecordMessageBusMessage>
    {
        private readonly IRecordDocumentService _recordDocumentService;
        private readonly ILogger<CreateRecordConsumer> _logger;
        
        public CreateRecordConsumer(
            IRecordDocumentService recordDocumentService, 
            ILogger<CreateRecordConsumer> logger)
        {
            _recordDocumentService = recordDocumentService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateRecordMessageBusMessage> context)
        {
            var message = context.Message;
            var recordDocument = RecordDocument.Create(
                message.Sku, 
                message.Title,
                message.Description,
                message.Price);
            
            await _recordDocumentService.IndexAsync(recordDocument);
            _logger.LogInformation("Message consumed!");
        }
    }
}