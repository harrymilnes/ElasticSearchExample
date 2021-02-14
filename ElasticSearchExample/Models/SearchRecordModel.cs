namespace ElasticSearchExample.Models
{
    public class SearchRecordRequestModel
    {
        public string Sku { get; private init; }
        public string Title { get; private init; }
        public string Description { get; private init; }
        public decimal Price { get; private init; }

        public static SearchRecordRequestModel Create(
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