using Nest;

namespace Search.Core.ClientCreator.Interfaces
{
    public interface IClientCreator
    {
        ElasticClient CreateClient();
    }
}