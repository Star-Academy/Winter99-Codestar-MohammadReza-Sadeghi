using Xunit;
using Moq;
using System.Collections.Generic;
using Phase05;
using Phase05.Search;
using Phase05.Utils;

namespace Test.UtilsTest
{
    public class OperationsTest
    {
        Mock<InvertedIndex> mockIndex;

        public OperationsTest()
        {
            mockIndex = new Mock<InvertedIndex>();
            mockIndex.SetupGet(x => x.Index).Returns(SampleCreator.CreateIndex());
        }

        [Fact]
        public void OrWordsTest()
        {
            var words = new List<string> { "i", "woultake", "anyone" };
            Assert.Equal(new HashSet<int> { 0, 1 }, Operations.OrWords(words, mockIndex.Object));
        }

        [Fact]
        public void AndWordsTest()
        {
            var words = new List<string> { "overstating", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { 0 }, Operations.AndWords(words, mockIndex.Object));
        }

        [Fact]
        public void AndWordsTest2()
        {
            var words = new List<string> { "from", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, Operations.AndWords(words, mockIndex.Object));
        }

        [Fact]
        public void AndWordsTest3()
        {
            var words = new List<string> { "overstating", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, Operations.AndWords(words, new HashSet<int> { 1 }, mockIndex.Object));
        }

        [Fact]
        public void AndWordsTest4()
        {
            var words = new List<string> { "from", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, Operations.AndWords(words, new HashSet<int> { }, mockIndex.Object));
        }

        [Fact]
        public void ExcludeWordsTest()
        {
            var words = new List<string> { "overstating", "conclusion" };
            var baseSet = new HashSet<int> { 8, 0, 5, 6 };
            Assert.Equal(new HashSet<int> { 8, 5, 6 }, Operations.ExcludeWords(baseSet, words, mockIndex.Object));
        }
    }
}
