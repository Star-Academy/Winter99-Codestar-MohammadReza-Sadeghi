using Phase05;
using Xunit;
using Moq;
using System.Collections.Generic;
using Search;

namespace Test
{
    public class SearchEngineTest
    {
        [Fact]
        public void SearchQueryTest()
        {
            var query = Creator.CreateQueryString();
            var mockIndex = new Mock<InvertedIndex>();
            mockIndex.SetupGet(x => x.Index).Returns(Creator.CreateIndex);
            Assert.Equal(new HashSet<int> { 0 }, new SearchEngine(mockIndex.Object).SearchQuery(query));
        }
    }
}
