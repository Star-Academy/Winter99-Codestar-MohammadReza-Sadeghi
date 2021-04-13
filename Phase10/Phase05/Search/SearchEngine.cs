﻿using Phase05.Utils;
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
            List<string> andWords = Tokenizer.ExtractAndWords(query);
            List<string> orWords = Tokenizer.ExtractOrWords(query);
            List<string> exWords = Tokenizer.ExtractExcludeWords(query);
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

        /*public List<QueryContainer> CreateAndQueries(List<string> andWords)
        {
            var andQueries = new List<QueryContainer>();
            foreach (var word in andWords)
                andQueries.Add(
                    new MatchQuery
                );
        }*/
    }
}
