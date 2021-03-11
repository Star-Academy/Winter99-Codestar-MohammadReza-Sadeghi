using System;
using System.Collections.Generic;

namespace Phase05
{
    public class SearchEngine
    {
        private InvertedIndex Index { get; set; }

        public SearchEngine(InvertedIndex Index)
        {
            this.Index = Index;
        }
        public HashSet<int> SearchQuery(string Query)
        {
            List<string> andWords;
            List<string> orWords;
            List<string> exWords;
            (andWords, orWords, exWords) = Tokenizer.ExtractQuery(Query);
            var result = Operations.OrWords(orWords, Index);
            if (andWords.Count > 0)
            {
                if (orWords.Count == 0)
                    result = Operations.AndWords(andWords, Index);
                else
                    result = Operations.AndWords(andWords, result, Index);
            }
            Operations.ExcludeWords(result, exWords, Index);
            return result;
        }
    }
}
