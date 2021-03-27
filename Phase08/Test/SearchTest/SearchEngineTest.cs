using Xunit;
using Moq;
using System.Collections.Generic;
using Phase05.Search;
using Phase05;
using Test.Mock;

namespace Test.SearchTest
{
    public class SearchEngineTest
    {
        [Fact]
        public void SearchQueryTest()
        {
            Mock<InvertedIndex> mockIndex = new Mock<InvertedIndex>();
            InvertedIndexMock.MockIndex(mockIndex);
            var query = SampleCreator.CreateQueryString();
            Assert.Equal(new HashSet<int> { 1 }, new SearchEngine(mockIndex.Object).SearchQuery(query));
        }
    }
}
