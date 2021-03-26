using Microsoft.EntityFrameworkCore;
using Phase05.Utils;
using System.Collections.Generic;
using Phase05.DataSet;
using System.Linq;
using System;

namespace Phase05.Search
{
    public class InvertedIndex : DbContext
    {
        public virtual Dictionary<string, HashSet<int>> Index { get; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordDoc> WordDocs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\MRSADEGHI78;Database=InvertedIndexDB;Trusted_Connection=True;");
        }

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
            //SetIdentityInsert();
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

        public void SetIdentityInsert()
        {
            this.Database.ExecuteSqlRaw("SET IDENTITY_INSERT InvertedIndexDB.Documents ON");
            //this.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Words ON");
            //this.Database.ExecuteSqlRaw("SET IDENTITY_INSERT WordDocs ON");
            this.SaveChanges();
        }

        public int AddDocument(string content)
        {
            var newDoc = new Document(content);
            this.Documents.Add(newDoc);
            this.SaveChanges();
            return newDoc.DocId;
        }

        public void AddToIndex(string key, int docId)
        {
            //Console.WriteLine(key + " " + docId);
            //using (var context = new InvertedIndex())
            //{
              /*  Document newDoc;
                if (*//*context.*//*Documents.Any(d => d.DocId == docId))
                {
                    newDoc = *//*context.*//*Documents.Single(d => d.DocId == docId);
                    Console.WriteLine("doc exist");
                }
                else
                {
                    newDoc = new Document(docId);
                    *//*context.*//*Documents.Add(newDoc);
                    *//*context.*//*Database.ExecuteSqlRaw("SET IDENTITY_INSERT Documents ON");
                    *//*context.*//*SaveChanges();
                    Console.WriteLine("doc doesn't exist");
                }*/

                Word newWord;
                if (/*context.*/this.Words.Any(w => w.Value.Equals(key)))
                {
                    newWord = /*context.*/this.Words.Single(w => w.Value.Equals(key));
                    //Console.WriteLine("word exist");
                }
                else
                {
                    newWord = new Word(key);
                    /*context.*/this.Words.Add(newWord);
                    /*context.*/this.SaveChanges();
                    //Console.WriteLine("word doesn't exist");
                }

                if (!this.WordDocs.Any(wd => wd.WordId.Equals(key) && wd.DocId == docId))
                {
                    WordDoc wordDoc = new WordDoc { DocId = docId, WordId = key };
                    this.WordDocs.Add(wordDoc);
                    this.SaveChanges();
                }
            /*context.*/
            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT WordDocs OFF");
            /*context.*/
            /* if (!newWord.Documents.Any(d => d.DocId == docId))
             {
                 newWord.Documents.Add(newDoc);
                 context.SaveChanges();
             }*/
            //}
        }

        public HashSet<int> GetDocsByWord(string word)
        {
            ICollection<int> docs;
            using (var context = new InvertedIndex())
            {
                if (context.Words.Any(w => w.Value.Equals(word)))
                    //docs = context.Words.Single(w => w.Value.Equals(word)).Documents.Select(d => d.DocId).ToHashSet();
                    docs = context.Documents.Where(doc => doc.WordDocs.Any(j => j.WordId == word)).Select(doc => doc.DocId).ToHashSet();
                else
                    docs = new HashSet<int>();
            }
            return (HashSet<int>) docs;
        }

        public bool ContainsWord(string word)
        {
            bool result = false;
            using (var context = new InvertedIndex())
            {
                result = context.Words.Any(w => w.Value.Equals(word));
            }
            return result;
        }

    }
}
