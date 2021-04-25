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
            HashSet<int> result = operations.OrWords(orWords);
            result = operations.AndWords(andWords, result);
            operations.ExcludeWords(result, exWords);
            return result;
        }
    }
}
