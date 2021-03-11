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

        public void CreateIndex(List<string> Documents)
        {
            for (int i = 0; i < Documents.Count; i++)
            {
                var tokenizedDoc = Tokenizer.Tokenize(Documents[i]);
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
