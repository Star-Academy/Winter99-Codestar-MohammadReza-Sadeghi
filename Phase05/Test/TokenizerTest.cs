using System.Collections.Generic;
using Xunit;

namespace Phase05
{
    public class TokenizerTest
    {
        [Fact]
        public void TokenizeTest()
        {
            string doc = Creator.CreateStr();
            Assert.Equal("tus in  some people8. there is a nati", Tokenizer.Tokenize(doc));
        }

        [Fact]
        public void SplitDocumentTest()
        {
            string doc = Creator.CreateStr();
            Assert.Equal(new[] { "tus", "in", "some", "people8", "There", "is", "a", "nati" }, Tokenizer.SplitDocument(doc));
        }

        [Fact]
        public void SplitInputTest()
        {
            string doc = Creator.CreateStr();
            Assert.Equal(new[] { "tus", "in", "some", "people8.", "There", "is", "a", "nati" }, Tokenizer.SplitInput(doc));
        }

        [Fact]
        public void ExtractQueryTest()
        {
            var query = Creator.CreateQueryString();
            List<string> andWords = new List<string>();
            List<string> orWords = new List<string>();
            List<string> exWords = new List<string>();
            (andWords, orWords, exWords) = Tokenizer.ExtractQuery(query);
            Assert.Equal(new List<string> { "i" }, andWords);
            Assert.Equal(new List<string> { "conclusion", "woultake" }, orWords);
            Assert.Equal(new List<string> { "issue" }, exWords);
        }
    }
}
