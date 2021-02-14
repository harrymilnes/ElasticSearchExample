
namespace MessageBus.Core.Messages
{
    public class CreateRecordMessageBusMessage
    {
        public string Sku { get; private init; }
        public string Title { get; private init; }
        public string Description { get; private init; }
        public decimal Price { get; private init; }

        public static CreateRecordMessageBusMessage Create(
            string sku,
            string title,
            string description,
            decimal price)
        {
            return new()
            {
                Sku = sku,
                Title = title,
                Description = description,
                Price = price
            };
        }
    }
}