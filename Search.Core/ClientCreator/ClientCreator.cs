using Nest;
using Search.Core.ClientCreator.Interfaces;
using Search.Core.Configuration.Interfaces;

namespace Search.Core.ClientCreator
{
    public class ClientCreator : IClientCreator
    {
        private readonly ISearchConfiguration _searchConfiguration;

        public ClientCreator(ISearchConfiguration searchConfiguration)
        {
            _searchConfiguration = searchConfiguration;
        }

        public ElasticClient CreateClient()
        {
            var connectionSettings = new ConnectionSettings(_searchConfiguration.Uri);
            var elasticClient = new ElasticClient(connectionSettings);
            return elasticClient;
        }
    }
}