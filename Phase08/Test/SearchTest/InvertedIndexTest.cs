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
            //Assert.Equal(new List<int> { }, invertedIndex.Documents.Select(d => d.DocId).ToList());
        }

        [Fact]
        public void AddToIndexTest2()
        {
            var index = new Dictionary<string, HashSet<int>>();
            index.Add("chocolate", new HashSet<int> { 0 });
            index.Add("place", new HashSet<int> { 0 });
            invertedIndex.AddDocument("");
            invertedIndex.AddToIndex("chocolate", 1);
            invertedIndex.AddToIndex("place", 1);
            Assert.Equal(index, GetDataBaseAsDictionary());
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
