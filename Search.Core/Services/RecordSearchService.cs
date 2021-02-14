using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Search.Core.Documents;
using Search.Core.Services.Interfaces;

namespace Search.Core.Services
{
    public class RecordSearchService : IRecordSearchService
    {
        private readonly IRecordDocumentService _recordDocumentService;

        public RecordSearchService(IRecordDocumentService recordDocumentService)
        {
            _recordDocumentService = recordDocumentService;
        }

        public async Task<IEnumerable<RecordDocument>> SearchRecordsAsync(string searchQuery)
        {
            var searchResults = await _recordDocumentService.SearchAsync(searchQuery);
            return searchResults.Select(it => RecordDocument.Create(it.Sku, it.Title, it.Description, it.Price));
        }
    }
}