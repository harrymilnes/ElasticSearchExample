namespace Search.Core.Documents
{
    public class RecordDocument
    {
        public string Sku { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

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