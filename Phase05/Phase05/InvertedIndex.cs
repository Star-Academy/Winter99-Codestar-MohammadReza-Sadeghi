using System.Collections.Generic;

namespace Phase05
{
    public class InvertedIndex
    {
        public virtual Dictionary<string, HashSet<int>> Index { get; }

        public InvertedIndex()
        {
                                          
            Index = new Dictionary<string, HashSet<int>>();
        }

        public void CreateIndex(List<string> documents)
        {
            for (int i = 0; i < documents.Count; i++)
            {
                var tokenizedDoc = Tokenizer.Tokenize(documents[i]);
                var docWords = Tokenizer.SplitDocument(tokenizedDoc);
                foreach (string word in docWords)
                {
                    AddToIndex(word, i);
                }
            }
        }

        public void AddToIndex(string key, int document)
        {
            var docs = new HashSet<int>();
            if (Index.ContainsKey(key))
                docs = Index[key];
            docs.Add(document);
            Index[key] = docs;
        }

    }
}
