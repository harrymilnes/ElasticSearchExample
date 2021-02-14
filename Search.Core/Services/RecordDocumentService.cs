using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Search.Core.ClientCreator.Interfaces;
using Search.Core.Documents;
using Search.Core.Services.Interfaces;

namespace Search.Core.Services
{
    public class RecordDocumentService : IRecordDocumentService
    {
        private readonly IClientCreator _clientCreator;
        
        public RecordDocumentService(
            IClientCreator clientCreator)
        {
            _clientCreator = clientCreator;
        }

        public async Task IndexAsync(RecordDocument recordDocument)
        {
            var elasticClient = _clientCreator.CreateClient();
            await elasticClient.IndexAsync(recordDocument, x => x.Index(nameof(RecordDocument).ToLower()));
        }

        public async Task<IEnumerable<RecordDocument>> SearchAsync(string searchQuery)
        {
            var elasticClient = _clientCreator.CreateClient();
            var recordDocumentSearch = await elasticClient.SearchAsync<RecordDocument>(searchDescriptor =>
                searchDescriptor.Query(query =>
                    query.Bool(boolQuery =>
                        boolQuery.Should(should =>
                            should.MultiMatch(multiMatch =>
                                multiMatch.Fields(fields =>
                                    fields.Fields
                                    (
                                        field => field.Sku,
                                        field => field.Title,
                                        field => field.Description
                                    )
                                ).Query(searchQuery)
                            )
                        )
                    )
                ).Index(nameof(RecordDocument).ToLower())
            );

            return recordDocumentSearch.Documents;
        }
    }
}