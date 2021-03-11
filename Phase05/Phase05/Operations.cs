using System.Collections.Generic;

namespace Phase05
{
    public class Operations
    {
        public static HashSet<int> OrWords(List<string> Words, InvertedIndex Index)
        {
            var orDocs = new HashSet<int>();
            foreach (string word in Words)
            {
                if (Index.Index.ContainsKey(word))
                {
                    var wordDocs = Index.Index[word];
                    orDocs.UnionWith(wordDocs);
                }
            }
            return orDocs;
        }

        public static HashSet<int> AndWords(List<string> Words, HashSet<int> BaseSet, InvertedIndex Index)
        {
            if (BaseSet.Count == 0)
                return new HashSet<int>();
            var andDocs = AndWords(Words, Index);
            andDocs.IntersectWith(BaseSet);
            return andDocs;
        }

        public static HashSet<int> AndWords(List<string> Words, InvertedIndex Index)
        {
            var andDocs = new HashSet<int>();
            for (int i = 0; i < Words.Count; i++)
            {
                if (!Index.Index.ContainsKey(Words[i]))
                    return new HashSet<int>();
                if (i == 0)
                    andDocs = new HashSet<int>(Index.Index[Words[i]]);
                else
                {
                    var wordDocs = Index.Index[Words[i]];
                    andDocs.IntersectWith(wordDocs);
                }
            }
            return andDocs;
        }

        public static HashSet<int> ExcludeWords(HashSet<int> BaseSet, List<string> Words, InvertedIndex Index)
        {
            foreach (string word in Words)
            {
                if (Index.Index.ContainsKey(word))
                    BaseSet.ExceptWith(Index.Index[word]);
            }
            return BaseSet;
        }
    }
}
