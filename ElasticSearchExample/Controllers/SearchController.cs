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
        private readonly IRecordSearchService _recordSearchService;
        private readonly IMessageBusService _messageBusService;
        
        public SearchController(
            IRecordSearchService recordSearchService, 
            IMessageBusService messageBusService)
        {
            _recordSearchService = recordSearchService;
            _messageBusService = messageBusService;
        }

        [HttpGet]
        [Route("/search/record/{searchQuery}")]
        public async Task<IActionResult> SearchRecordsAsync(string searchQuery)
        {
            var searchResults = await _recordSearchService.SearchRecordsAsync(searchQuery);
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

            await _messageBusService.PublicCreateRecordMessage(createRecordMessage);
            return Ok();
        }
    }
}