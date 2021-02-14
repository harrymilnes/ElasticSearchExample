using System.Collections.Generic;
using System.Threading.Tasks;
using Search.Core.Documents;

namespace Search.Core.Services.Interfaces
{
    public interface IRecordDocumentService
    {
        Task IndexAsync(RecordDocument recordDocument);
        Task<IEnumerable<RecordDocument>> SearchAsync(string searchQuery);
    }
}