using System;
using System.Collections.Generic;
using Phase05.Search;

namespace Phase05.Utils
{
    public class Operations
    {
        private InvertedIndex Index;

        public Operations(InvertedIndex index)
        {
            this.Index = index;
        }

        public HashSet<int> OrWords(List<string> words)
        {
            var orDocs = new HashSet<int>();
            foreach (string word in words)
            {
                if (Index.ContainsWord(word))
                {
                    var wordDocs = Index.GetDocsByWord(word);
                    orDocs.UnionWith(wordDocs);
                }
            }
            return orDocs;
        }

        public HashSet<int> AndWords(List<string> words, HashSet<int> baseSet)
        {
            if (baseSet.Count == 0)
                return new HashSet<int>();
            var andDocs = AndWords(words);
            andDocs.IntersectWith(baseSet);
            return andDocs;
        }

        public HashSet<int> AndWords(List<string> words)
        {
            var andDocs = new HashSet<int>();
            for (int i = 0; i < words.Count; i++)
            {
                if (!Index.ContainsWord(words[i]))
                    return new HashSet<int>();
                if (i == 0)
                    andDocs = new HashSet<int>(Index.GetDocsByWord(words[i]));
                else
                {
                    var wordDocs = Index.GetDocsByWord(words[i]);
                    andDocs.IntersectWith(wordDocs);
                }
            }
            return andDocs;
        }

        public HashSet<int> ExcludeWords(HashSet<int> baseSet, List<string> words)
        {
            foreach (string word in words)
            {
                if (Index.ContainsWord(word))
                    baseSet.ExceptWith(Index.GetDocsByWord(word));
            }
            return baseSet;
        }
    }
}
