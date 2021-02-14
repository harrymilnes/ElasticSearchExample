using System.Threading.Tasks;
using MassTransit;
using MessageBus.Core.Messages;

namespace SearchIndexerExample.Consumers
{
    public class CreateRecordConsumer : IConsumer<CreateRecordMessageBusMessage>
    {
        public async Task Consume(ConsumeContext<CreateRecordMessageBusMessage> context)
        {
            //TODO: Implement search building logic.
        }
    }
}