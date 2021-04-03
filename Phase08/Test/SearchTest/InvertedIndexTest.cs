using Microsoft.EntityFrameworkCore;
using Phase05.Search;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Test.SearchTest
{
    public class InvertedIndexTest
    {
        InvertedIndex invertedIndex;
        public InvertedIndexTest()
        {
            var options = new DbContextOptionsBuilder<InvertedIndex>()
                .UseInMemoryDatabase("Test")
                .Options;
            invertedIndex = new InvertedIndex(options);
            invertedIndex.Database.EnsureDeleted();
            invertedIndex.Database.EnsureCreated();
        }

        [Fact]
        public void CreateIndexTest()
        {
            var docs = SampleCreator.CreateStringList();
            invertedIndex.CreateIndex(docs);
            Assert.Equal(SampleCreator.CreateIndex(), GetDataBaseAsDictionary());
        }

        [Fact]
        public void AddToIndexTest()
        {
            var index = new Dictionary<string, HashSet<int>>();
            index.Add("chocolate", new HashSet<int> { 0 });
            invertedIndex.AddDocument("");
            invertedIndex.AddToIndex("chocolate", 1);
            Assert.Equal(index, GetDataBaseAsDictionary());
        }

        /// <summary>
        /// Checks adding the same content to the index twice
        /// </summary>
        [Fact]
        public void AddToIndexTest2()
        {
            var index = new Dictionary<string, HashSet<int>>();
            index.Add("chocolate", new HashSet<int> { 0 });
            invertedIndex.AddDocument("");
            invertedIndex.AddToIndex("chocolate", 1);
            invertedIndex.AddToIndex("chocolate", 1);
            Assert.Equal(index, GetDataBaseAsDictionary());
        }

        [Fact]
        public void AddDocumentTest()
        {
            string str = "something";
            invertedIndex.AddDocument(str);
            Assert.Equal(1, invertedIndex.Documents.Count());
            Assert.Equal(str, invertedIndex.Documents.Select(d => d.Content).First());
        }

        [Fact]
        public void AddWordTest()
        {
            string word = "something";
            invertedIndex.AddWord(word);
            Assert.Equal(1, invertedIndex.Words.Count());
            Assert.Equal(word, invertedIndex.Words.Select(w => w.Value).First());
        }

        /// <summary>
        /// Checks adding the same word to the database twice
        /// </summary>
        [Fact]
        public void AddWordTest2()
        {
            string word = "something";
            invertedIndex.AddWord(word);
            invertedIndex.AddWord(word);
            Assert.Equal(1, invertedIndex.Words.Count());
            Assert.Equal(word, invertedIndex.Words.Select(w => w.Value).First());
        }

        [Fact]
        public void GetDocsByWordTest()
        {
            var docs = SampleCreator.CreateStringList();
            invertedIndex.CreateIndex(docs);
            var result = invertedIndex.GetDocsByWord("conclusion");
            Assert.Equal(new HashSet<int> { 2, 3 }, result);
        }

        /// <summary>
        /// Tests getting documents of a word that doesn't exist in the database
        /// </summary>
        [Fact]
        public void GetDocsByWordTest2()
        {
            var docs = SampleCreator.CreateStringList();
            invertedIndex.CreateIndex(docs);
            var result = invertedIndex.GetDocsByWord("something that not exist in the database");
            Assert.Equal(new HashSet<int> { }, result);
        }

        [Fact]
        public void ContainsWordTest()
        {
            string word = "some word";
            invertedIndex.AddWord(word);
            Assert.True(invertedIndex.ContainsWord(word));
        }

        /// <summary>
        /// Tests a word that doesn't exist in the database
        /// </summary>
        [Fact]
        public void ContainsWordTest2()
        {
            var docs = SampleCreator.CreateStringList();
            invertedIndex.CreateIndex(docs);
            string word = "some word that not exist in the database";
            Assert.False(invertedIndex.ContainsWord(word));
        }

        public Dictionary<string, HashSet<int>> GetDataBaseAsDictionary()
        {
            var result = new Dictionary<string, HashSet<int>>();
            foreach (string word in invertedIndex.Words.Select(w => w.Value))
            {
                var docIds = invertedIndex.GetDocsByWord(word).Select(i => i - 1).ToHashSet();
                result.Add(word, docIds);
            }
            return result;
        }
    }
}
