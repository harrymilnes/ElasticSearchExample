using System.Collections.Generic;
using System.Threading.Tasks;
using Search.Core.Documents;
using Search.Core.Services.Interfaces;

namespace Search.Core.Services
{
    public class RecordSearchService : IRecordSearchService
    {
        public async Task<IEnumerable<RecordDocument>> SearchRecordsAsync(string searchQuery)
        {
            //TODO: Implement ElasticSearch Client and return results.
            throw new System.NotImplementedException();
        }
    }
}