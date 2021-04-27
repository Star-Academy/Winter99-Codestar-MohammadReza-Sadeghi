using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phase05.Search;

namespace SearchApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchApiController : ControllerBase
    {

        private readonly ILogger<SearchApiController> _logger;
        private readonly ISearchEngine searchEngine;

        public SearchApiController(ILogger<SearchApiController> logger)
        {
            _logger = logger;
            var invertedIndex = SearchApi.InitializeInvertedIndex();
            searchEngine = new SearchEngine(invertedIndex);
        }

        [HttpGet]
        public string TestApi()
        {
            return "I got it.";
        }

        [HttpPost]
        public List<string> Search([FromBody] string query)
        {
            return searchEngine.SearchQuery(query);
        }
    }
}
