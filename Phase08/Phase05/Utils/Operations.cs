using System;
using System.Collections.Generic;
using Phase05.Search;

namespace Phase05.Utils
{
    public class Operations
    {
        public static HashSet<int> OrWords(List<string> words, InvertedIndex index)
        {
            var orDocs = new HashSet<int>();
            foreach (string word in words)
            {
                if (index.ContainsWord(word))
                {
                    var wordDocs = index.GetDocsByWord(word);
                    orDocs.UnionWith(wordDocs);
                }
            }
            return orDocs;
        }

        public static HashSet<int> AndWords(List<string> words, HashSet<int> baseSet, InvertedIndex index)
        {
            if (baseSet.Count == 0)
                return new HashSet<int>();
            var andDocs = AndWords(words, index);
            andDocs.IntersectWith(baseSet);
            return andDocs;
        }

        public static HashSet<int> AndWords(List<string> words, InvertedIndex index)
        {
            var andDocs = new HashSet<int>();
            for (int i = 0; i < words.Count; i++)
            {
                if (!index.ContainsWord(words[i]))
                    return new HashSet<int>();
                if (i == 0)
                    andDocs = new HashSet<int>(index.GetDocsByWord(words[i]));
                else
                {
                    var wordDocs = index.GetDocsByWord(words[i]);
                    andDocs.IntersectWith(wordDocs);
                }
            }
            return andDocs;
        }

        public static HashSet<int> ExcludeWords(HashSet<int> baseSet, List<string> words, InvertedIndex index)
        {
            foreach (string word in words)
            {
                if (index.ContainsWord(word))
                    baseSet.ExceptWith(index.GetDocsByWord(word));
            }
            return baseSet;
        }
    }
}
