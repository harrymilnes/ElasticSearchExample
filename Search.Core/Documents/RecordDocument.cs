using Nest;

namespace Search.Core.Documents
{
    public class RecordDocument
    {
        public string Sku { get; init; }
        [Keyword(SplitQueriesOnWhitespace = false)]
        public string Title { get; init; }
        [Keyword(SplitQueriesOnWhitespace = false)]
        public string Description { get; init; }
        public decimal Price { get; init; }

        public static RecordDocument Create(
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