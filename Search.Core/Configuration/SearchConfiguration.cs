using Microsoft.Extensions.Configuration;
using Search.Core.Configuration.Interfaces;

namespace Search.Core.Configuration
{
    public class SearchConfiguration : ISearchConfiguration
    {
        public string Hostname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int Fuzziness { get; private set; }

        private readonly IConfiguration _configuration;

        public SearchConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        //TODO: Pull configuration from AppSettings.
    }
}