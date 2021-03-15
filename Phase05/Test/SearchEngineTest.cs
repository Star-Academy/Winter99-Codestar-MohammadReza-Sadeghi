using Xunit;
using Moq;
using System.Collections.Generic;

namespace Phase05
{
    public class SearchEngineTest
    {
        [Fact]
        public void SearchQueryTest()
        {
            var query = SampleCreator.CreateQueryString();
            var mockIndex = new Mock<InvertedIndex>();
            mockIndex.SetupGet(x => x.Index).Returns(SampleCreator.CreateIndex());
            Assert.Equal(new HashSet<int> { 1 }, new SearchEngine(mockIndex.Object).SearchQuery(query));
        }
    }
}
