using System.Threading.Tasks;
using MassTransit;
using MessageBus.Core.Messages;
using Search.Core.Documents;
using Search.Core.Services.Interfaces;

namespace SearchIndexerExample.Consumers
{
    public class CreateRecordConsumer : IConsumer<CreateRecordMessageBusMessage>
    {
        private readonly IRecordDocumentService _recordDocumentService;

        public CreateRecordConsumer(IRecordDocumentService recordDocumentService)
        {
            _recordDocumentService = recordDocumentService;
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
        }
    }
}