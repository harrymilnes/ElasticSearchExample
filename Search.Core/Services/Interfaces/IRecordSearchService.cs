using System.Collections.Generic;
using System.Threading.Tasks;
using Search.Core.Documents;

namespace Search.Core.Services.Interfaces
{
    public interface IRecordSearchService
    {
        Task<IEnumerable<RecordDocument>> SearchRecordsAsync(string searchQuery);
    }
}