using Xunit;

namespace Phase05
{
    public class TokenizerTest
    {
        [Fact]
        public void TokenizeAndSplitDocumentTest()
        {
            string doc = "tus in some people8. There is a nati";
            Assert.Equal(new[] { "tus", "in", "som", "people8", "there", "is", "a", "nati" }, Tokenizer.TokenizeAndSplitDocument(doc));
        }
    }
}
