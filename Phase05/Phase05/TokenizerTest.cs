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
            Assert.Equal(new[] { "tus", "in", "som", "people8", "There", "is", "a", "nati" }, Tokenizer.SplitDocument(doc));
        }

        [Fact]
        public void SplitInputTest()
        {
            string doc = Creator.CreateStr();
            Assert.Equal(new[] { "tus", "in", "som", "people8.", "There", "is", "a", "nati" }, Tokenizer.SplitInput(doc));
        }

        [Fact]
        public void ExtractQueryTest()
        {
            var query = Creator.CreateQueryArray();
            List<string> andWords = new List<string>();
            List<string> orWords = new List<string>();
            List<string> exWords = new List<string>();
            (andWords, orWords, exWords) = Tokenizer.ExtractQuery(query);
            Assert.Equal(new List<string> { "tus" }, andWords);
            Assert.Equal(new List<string> { "is", "som" }, orWords);
            Assert.Equal(new List<string> { "people8" }, exWords);
        }
    }
}
