using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phase05
{
    public class Tokenizer
    {
        private static readonly string DocumentRegex = "\\w+";

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
            return Regex.Split(input, "\\s+");
        }

        public static (List<string>, List<string>, List<string>) ExtractQuery(string query)
        {
            query = Tokenizer.Tokenize(query);
            var queryWords = Tokenizer.SplitInput(query);
            var andWords = new List<string>();
            var orWords = new List<string>();
            var exWords = new List<string>();
            foreach (string word in queryWords)
            {
                if (word[0] == '+')
                    orWords.Add(word.Substring(1));
                else if (word[0] == '-')
                    exWords.Add(word.Substring(1));
                else
                    andWords.Add(word);
            }
            return (andWords, orWords, exWords);
        }

    }
}
