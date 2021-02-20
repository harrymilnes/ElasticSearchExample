using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchExample.Models;
using MessageBus.Core.Messages;
using MessageBus.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Search.Core.Services.Interfaces;

namespace ElasticSearchExample.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IRecordDocumentService _recordSearchService;
        private readonly IMessageBusService _messageBusService;
        
        public SearchController(
            IRecordDocumentService recordSearchService, 
            IMessageBusService messageBusService)
        {
            _recordSearchService = recordSearchService;
            _messageBusService = messageBusService;
        }

        [HttpGet]
        [Route("/search/record/{searchQuery}")]
        public async Task<IActionResult> SearchRecordsAsync(string searchQuery)
        {
            var searchResults = await _recordSearchService.SearchAsync(searchQuery);
            var mappedSearchResults = searchResults.Select(it => SearchRecordRequestModel.Create(it.Sku, it.Title, it.Description, it.Price));
            return Ok(mappedSearchResults);
        }
        
        [HttpPost]
        [Route("/search/record")]
        public async Task<IActionResult> CreateRecordAsync([FromBody] CreateSearchRecordRequestModel createSearchRecordRequestModel)
        {
            var createRecordMessage = CreateRecordMessageBusMessage.Create(
                createSearchRecordRequestModel.Sku,
                createSearchRecordRequestModel.Title,
                createSearchRecordRequestModel.Description,
                createSearchRecordRequestModel.Price
            );

            await _messageBusService.SendCreateRecordMessageAsync(createRecordMessage);
            return Ok();
        }

        [HttpPost]
        [Route("/search/records")]
        public IActionResult CreateExampleRecordsAsync()
        {
            var createRecordMessages = new List<CreateRecordMessageBusMessage>
            {
                CreateRecordMessageBusMessage.Create("DF-90", "Dog Food", "Food for Dogs", (decimal)54.32),
                CreateRecordMessageBusMessage.Create("BF-431", "Bat Food", "Food for Bats", (decimal)12.12), 
                CreateRecordMessageBusMessage.Create("DT-65", "Dog Treats", "Dog Treats", (decimal)19.32), 
                CreateRecordMessageBusMessage.Create("HB-5412", "Hamster Ball", "Hamster Ball", (decimal)5.54), 
                CreateRecordMessageBusMessage.Create("CF-21", "Cat Food", "Food for Cats", (decimal)9.12),
                CreateRecordMessageBusMessage.Create("HB-4312", "Hamster Bedding", "Bedding for Hamsters", (decimal)32.12), 
                CreateRecordMessageBusMessage.Create("HT-1231", "Hamster Treats", "Hamster Treats", (decimal)43.12),
            };

            createRecordMessages.ForEach(async it => await _messageBusService.SendCreateRecordMessageAsync(it));
            return Ok();
        }
    }
}