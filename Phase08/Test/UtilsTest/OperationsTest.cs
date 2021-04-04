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
        private Operations operations;

        public OperationsTest()
        {
            Mock<InvertedIndex> MockIndex = new Mock<InvertedIndex>();
            InvertedIndexMock.MockIndex(MockIndex);
            operations = new Operations(MockIndex.Object);
        }

        [Fact]
        public void OrWordsTest()
        {
            var words = new List<string> { "i", "woultake", "anyone" };
            Assert.Equal(new HashSet<int> { 0, 1 }, operations.OrWords(words));
        }

        [Fact]
        public void AndWordsTest()
        {
            var words = new List<string> { "overstating", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { 0 }, operations.AndWords(words));
        }

        /// <summary>
        /// Tests the case in which one of the 'and' operands ("from") doesn't exist in the data base,
        /// so the result should be an empty hashset
        /// </summary>
        [Fact]
        public void AndWordsTest2()
        {
            var words = new List<string> { "from", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, operations.AndWords(words));
        }

        /// <summary>
        /// Tests the case in which there is no document containing all 'and' operands,
        /// so the result should be an empty hashset
        /// </summary>
        [Fact]
        public void AndWordsTest3()
        {
            var words = new List<string> { "overstating", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, operations.AndWords(words, new HashSet<int> { 1 }));
        }

        [Fact]
        public void AndWordsTest4()
        {
            var words = new List<string> { "from", "woultake", "issue" };
            Assert.Equal(new HashSet<int> { }, operations.AndWords(words, new HashSet<int> { }));
        }

        [Fact]
        public void ExcludeWordsTest()
        {
            var words = new List<string> { "overstating", "conclusion" };
            var baseSet = new HashSet<int> { 8, 0, 5, 6 };
            Assert.Equal(new HashSet<int> { 8, 5, 6 }, operations.ExcludeWords(baseSet, words));
        }
    }
}
