using System;
using System.Collections.Generic;
using ElasticSearchExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchExample.Controllers
{
    public class SearchController : BaseController
    {
        [HttpGet]
        [Route("/search/record/{query}")]
        public IEnumerable<SearchRecordRequestModel> SearchRecordsAsync(string query)
        {
            //TODO: Implement ElasticSearch, return results.
            throw new NotImplementedException();
        }
        
        [HttpPost]
        [Route("/search/record")]
        public IEnumerable<SearchRecordRequestModel> CreateRecordAsync([FromBody] CreateSearchRecordRequestModel createSearchRecordRequestModel)
        {
            //TODO: Use Mass Transit to flick message on to RMQ queue.
            throw new NotImplementedException();
        }
    }
}