using Phase05.Elastic;
using Phase05.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApi
{
    public class SearchApi
    {
        private static readonly string indexName = "simple_docs";
        private static readonly string elasticServerUrl = "http://localhost";
        private static readonly int elasticServerPort = 9200;

        public static InvertedIndex InitializeInvertedIndex()
        {
            var elasticIndex = new ElasticIndex(elasticServerUrl, elasticServerPort, indexName);
            var invertedIndex = new InvertedIndex(elasticIndex);
            return invertedIndex;
        }
    }
}
