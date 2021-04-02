using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phase05.Utils
{
    public class Tokenizer
    {
        private static readonly string DocumentRegex = "\\w+";
        private static readonly string QueryRegex = "\\s+";

        public static string Tokenize(string str)
        {
            return str.ToLower();
        }

        public static string[] SplitDocument(string document)
        {
            return Regex.Matches(document, DocumentRegex).OfType<Match>().Select(m => m.Value).ToArray();
        }

        public static string[] SplitInput(string input)
        {
            return Regex.Split(input, QueryRegex);
        }

        public static List<string> ExtractAndWords(string query)
        {
            query = Tokenize(query);
            var queryWords = SplitInput(query);
            var andWords = new List<string>();
            foreach (string word in queryWords)
                if (word[0] != '+' && word[0] != '-')
                    andWords.Add(word);
            return andWords;
        }

        public static List<string> ExtractOrWords(string query)
        {
            query = Tokenize(query);
            var queryWords = SplitInput(query);
            var orWords = new List<string>();
            foreach (string word in queryWords)
                if (word[0] == '+')
                    orWords.Add(word.Substring(1));
            return orWords;
        }

        public static List<string> ExtractExcludeWords(string query)
        {
            query = Tokenize(query);
            var queryWords = SplitInput(query);
            var exWords = new List<string>();
            foreach (string word in queryWords)
                if (word[0] == '-')
                    exWords.Add(word.Substring(1));
            return exWords;
        }

    }
}
