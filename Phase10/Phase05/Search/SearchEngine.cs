using Phase05.Utils;
using System.Collections.Generic;
using Nest;

namespace Phase05.Search
{
    public class SearchEngine
    {
        private InvertedIndex Index { get; set; }

        public SearchEngine(InvertedIndex index)
        {
            Index = index;
        }
        public List<string> SearchQuery(string query)
        {
            var andWords = Tokenizer.ExtractAndWords(query);
            var orWords = Tokenizer.ExtractOrWords(query);
            var exWords = Tokenizer.ExtractExcludeWords(query);
            var elasticQuery = CreateElasticQuery(andWords, orWords, exWords);
            var result = Index.SearchQuery(elasticQuery);
            return result;
        }

        public QueryContainer CreateElasticQuery(List<string> andWords, List<string> orWords, List<string> excludeWords)
        {
            return new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    new TermsQuery
                    {
                        Field = "content",
                        Terms = andWords
                    }
                }, 
                Should = new List<QueryContainer>
                {
                    new TermsQuery
                    {
                        Field = "content",
                        Terms = orWords
                    }
                },
                MustNot = new List<QueryContainer>
                {
                    new TermsQuery
                    {
                        Field = "content",
                        Terms = excludeWords
                    }
                }
            };
        }
    }
}
