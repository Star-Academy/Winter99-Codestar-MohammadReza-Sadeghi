using Phase05.Utils;
using System.Collections.Generic;

namespace Phase05.Search
{
    public class SearchEngine
    {
        private InvertedIndex Index { get; set; }

        public SearchEngine(InvertedIndex index)
        {
            Index = index;
        }
        public HashSet<int> SearchQuery(string query)
        {
            List<string> andWords = Tokenizer.ExtractAndWords(query);
            List<string> orWords = Tokenizer.ExtractOrWords(query);
            List<string> exWords = Tokenizer.ExtractExcludeWords(query);
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
