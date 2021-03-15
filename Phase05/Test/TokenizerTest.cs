using System.Collections.Generic;
using Xunit;

namespace Phase05
{
    public class TokenizerTest
    {
        [Fact]
        public void TokenizeTest()
        {
            string doc = SampleCreator.CreateStr();
            Assert.Equal(SampleCreator.CreateTekenizedStr(), Tokenizer.Tokenize(doc));
        }

        [Fact]
        public void SplitDocumentTest()
        {
            string doc = SampleCreator.CreateStr();
            Assert.Equal(SampleCreator.CreateSplitdedDocument(), Tokenizer.SplitDocument(doc));
        }

        [Fact]
        public void SplitInputTest()
        {
            string doc = SampleCreator.CreateStr();
            Assert.Equal(SampleCreator.CreateSplittedInput(), Tokenizer.SplitInput(doc));
        }

        [Fact]
        public void ExtractQueryTest1()
        {
            var query = SampleCreator.CreateQueryString();
            List<string> andWords = new List<string>();
            andWords = Tokenizer.ExtractAndWords(query);
            Assert.Equal(new List<string> { "i" }, andWords);
        }

        [Fact]
        public void ExtractQueryTest2()
        {
            var query = SampleCreator.CreateQueryString();
            List<string> orWords = new List<string>();
            orWords = Tokenizer.ExtractOrWords(query);
            Assert.Equal(new List<string> { "conclusion", "woultake" }, orWords);
        }

        [Fact]
        public void ExtractQueryTest3()
        {
            var query = SampleCreator.CreateQueryString();
            List<string> exWords = new List<string>();
            exWords = Tokenizer.ExtractExcludeWords(query);
            Assert.Equal(new List<string> { "issue" }, exWords);
        }
    }
}
