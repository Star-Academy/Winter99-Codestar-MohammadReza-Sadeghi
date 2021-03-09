using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Phase05
{
    public class InvertedIndexTest
    {
        [Fact]
        public void CreateIndexTest()
        {
            var invertedIndex = new InvertedIndex();
            var docs = Creator.CreateStringList();
            invertedIndex.CreateIndex(docs);
            Assert.Equal(invertedIndex.Index, Creator.CreateIndex());
        }

        [Fact]
        public void AddToIndexTest()
        {
            var index = new Dictionary<string, ArrayList>();
            index.Add("chocolate", new ArrayList { 3 });
            var invertedIndex = new InvertedIndex();
            invertedIndex.AddToIndex("chocolate", 3);
            Assert.Equal(index, invertedIndex.Index);
        }

        public void AddToIndexTest2()
        {
            var index = new Dictionary<string, ArrayList>();
            index.Add("chocolate", new ArrayList { 3 });
            index.Add("place", new ArrayList { 3 });
            var invertedIndex = new InvertedIndex();
            invertedIndex.AddToIndex("chocolate", 3);
            invertedIndex.AddToIndex("place", 3);
            Assert.Equal(index, invertedIndex.Index);
        }
    }
}
