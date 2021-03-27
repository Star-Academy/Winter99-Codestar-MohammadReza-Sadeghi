using Microsoft.EntityFrameworkCore;
using Phase05.Utils;
using System.Collections.Generic;
using Phase05.DataSet;
using System.Linq;

namespace Phase05.Search
{
    public class InvertedIndex : DbContext
    {
        //public virtual Dictionary<string, HashSet<int>> Index { get; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordDoc> WordDocs { get; set; }

        public InvertedIndex(DbContextOptions<InvertedIndex> options): base(options) { }
        
        public InvertedIndex() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDoc>().HasKey(wd => new { wd.WordId, wd.DocId });

            modelBuilder.Entity<WordDoc>()
                .HasOne<Word>(wd => wd.Word)
                .WithMany(w => w.WordDocs)
                .HasForeignKey(wd => wd.WordId);

            modelBuilder.Entity<WordDoc>()
                .HasOne<Document>(wd => wd.Doc)
                .WithMany(d => d.WordDocs)
                .HasForeignKey(wd => wd.DocId);
        }

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
        }

        public int AddDocument(string content)
        {
            var newDoc = new Document(content);
            this.Documents.Add(newDoc);
            this.SaveChanges();
            return newDoc.DocId;
        }

        public void AddWord(string word)
        {
            if (!this.Words.Any(w => w.Value.Equals(word)))
            {
                var newWord = new Word(word);
                this.Words.Add(newWord);
                this.SaveChanges();
            }
        }

        public void AddToIndex(string key, int docId)
        {
            AddWord(key);
            if (!this.WordDocs.Any(wd => wd.WordId.Equals(key) && wd.DocId == docId))
            {
                WordDoc wordDoc = new WordDoc { DocId = docId, WordId = key };
                this.WordDocs.Add(wordDoc);
                this.SaveChanges();
            }
        }

        public virtual HashSet<int> GetDocsByWord(string word)
        {
            ICollection<int> docs;
            if (this.Words.Any(w => w.Value.Equals(word)))
                docs = this.Documents.Where(doc => doc.WordDocs.Any(j => j.WordId == word)).Select(doc => doc.DocId).ToHashSet();
            else
                docs = new HashSet<int>();
            return (HashSet<int>) docs;
        }

        public virtual bool ContainsWord(string word)
        {
            bool result = false;
            result = this.Words.Any(w => w.Value.Equals(word));
            return result;
        }

    }
}
