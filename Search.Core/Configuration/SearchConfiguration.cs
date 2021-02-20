using System;
using Microsoft.Extensions.Configuration;
using Search.Core.Configuration.Interfaces;

namespace Search.Core.Configuration
{
    public class SearchConfiguration : ISearchConfiguration
    {
        private readonly IConfiguration _configuration;

        public SearchConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri Uri => GetHostname();
        private Uri GetHostname()
        {
            return _configuration.GetValue<Uri>("ElasticSearch:Hostname");
        }
    }
}