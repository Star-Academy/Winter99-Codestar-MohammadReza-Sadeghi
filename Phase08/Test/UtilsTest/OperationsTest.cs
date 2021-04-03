using Xunit;
using Moq;
using System.Collections.Generic;
using Phase05.Search;
using Phase05.Utils;
using Test.Mock;
using Microsoft.EntityFrameworkCore;

namespace Test.UtilsTest
{
    public class OperationsTest
    {
        Mock<InvertedIndex> MockIndex;
        public OperationsTest()
        {
            MockIndex = new Mock<InvertedIndex>();
            InvertedIndexMock.MockIndex(MockIndex);
        }

        [Fact]
        public void OrWordsTest()
        {
            var words = new List<string> { "i", "woultake", "anyone" };
            Assert.Equal(new HashSet<int> { 0, 1 }, Operations.OrWords(words, MockIndex.Object));
        }

        [Fact]
        public void AndWordsTest()
        {
            var words = new List<string> { "overstating", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { 0 }, Operations.AndWords(words, MockIndex.Object));
        }

        /// <summary>
        /// Tests the case in which one of the 'and' operands ("from") doesn't exist in the data base,
        /// so the result should be an empty hashset
        /// </summary>
        [Fact]
        public void AndWordsTest2()
        {
            var words = new List<string> { "from", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, Operations.AndWords(words, MockIndex.Object));
        }

        /// <summary>
        /// Tests the case in which there is no document containing all 'and' operands,
        /// so the result should be an empty hashset
        /// </summary>
        [Fact]
        public void AndWordsTest3()
        {
            var words = new List<string> { "overstating", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, Operations.AndWords(words, new HashSet<int> { 1 }, MockIndex.Object));
        }

        [Fact]
        public void AndWordsTest4()
        {
            var words = new List<string> { "from", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, Operations.AndWords(words, new HashSet<int> { }, MockIndex.Object));
        }

        [Fact]
        public void ExcludeWordsTest()
        {
            var words = new List<string> { "overstating", "conclusion" };
            var baseSet = new HashSet<int> { 8, 0, 5, 6 };
            Assert.Equal(new HashSet<int> { 8, 5, 6 }, Operations.ExcludeWords(baseSet, words, MockIndex.Object));
        }
    }
}
