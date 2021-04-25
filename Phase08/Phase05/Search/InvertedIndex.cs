using Phase05.Utils;
using System.Collections.Generic;
using Phase05.DataSet;
using System.Linq;

namespace Phase05.Search
{
    public class InvertedIndex
    {
        private InvertedIndexDbContext Context;

        public InvertedIndex(InvertedIndexDbContext context)
        {
            this.Context = context;
        }

        public InvertedIndex() { }

        public void CreateIndex(List<string> documents)
        {
            for (int i = 0; i < documents.Count; i++)
            {
                var docId = AddDocument(documents[i]);
                var tokenizedDoc = Tokenizer.Tokenize(documents[i]);
                var docWords = Tokenizer.SplitDocument(tokenizedDoc);
                foreach (string word in docWords)
                    if (!word.Equals(""))
                        AddToIndex(word, docId);
            }
            Context.SaveChanges();
        }

        public int AddDocument(string content)
        {
            var newDoc = new Document(content);
            Context.Documents.Add(newDoc);
            return newDoc.DocId;
        }

        public void AddWord(string word)
        {
            if (!Context.Words.Any(w => w.Value.Equals(word)))
            {
                var newWord = new Word(word);
                Context.Words.Add(newWord);
            }
        }

        public void AddToIndex(string key, int docId)
        {
            AddWord(key);
            if (!Context.WordDocs.Any(wd => wd.WordId.Equals(key) && wd.DocId == docId))
            {
                WordDoc wordDoc = new WordDoc { DocId = docId, WordId = key };
                Context.WordDocs.Add(wordDoc);
            }
        }

        public virtual HashSet<int> GetDocsByWord(string word)
        {
            HashSet<int> docs;
            if (Context.Words.Any(w => w.Value.Equals(word)))
                docs = Context.Documents.Where(doc => doc.WordDocs.Any(j => j.WordId == word)).Select(doc => doc.DocId).ToHashSet();
            else
                docs = new HashSet<int>();
            return docs;
        }

        public virtual bool ContainsWord(string word)
        {
            return Context.Words.Any(w => w.Value.Equals(word));
        }

    }
}
