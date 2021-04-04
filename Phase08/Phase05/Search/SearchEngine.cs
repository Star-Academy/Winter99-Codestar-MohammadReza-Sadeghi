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
            Operations operations = new Operations(Index);
            var result = operations.OrWords(orWords);
            if (andWords.Count > 0)
            {
                if (orWords.Count == 0)
                    result = operations.AndWords(andWords);
                else
                    result = operations.AndWords(andWords, result);
            }
            operations.ExcludeWords(result, exWords);
            return result;
        }
    }
}
