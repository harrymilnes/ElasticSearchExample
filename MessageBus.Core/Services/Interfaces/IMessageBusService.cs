using System.Threading.Tasks;
using MessageBus.Core.Messages;

namespace MessageBus.Core.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task PublicCreateRecordMessage(CreateRecordMessageBusMessage createRecordMessageBusMessage);
    }
}