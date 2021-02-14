using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ElasticSearchExample.Models
{
    public class CreateSearchRecordRequestModel
    {
        [JsonProperty]
        [Required(AllowEmptyStrings = false)]
        public string Sku { get; private set; }
        
        [JsonProperty]
        [Required(AllowEmptyStrings = false)]
        public decimal Price { get; private set; }
        
        [JsonProperty]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; private set; }
        
        [JsonProperty]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; private set; }
    }
}