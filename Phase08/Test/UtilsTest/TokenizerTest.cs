using Phase05;
using Phase05.Utils;
using System.Collections.Generic;
using Xunit;

namespace Test.UtilsTest
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

        /// <summary>
        /// Tests 'and' words of the query
        /// </summary>
        [Fact]
        public void ExtractQueryTest1()
        {
            var query = SampleCreator.CreateQueryString();
            List<string> andWords = new List<string>();
            andWords = Tokenizer.ExtractAndWords(query);
            Assert.Equal(new List<string> { "i" }, andWords);
        }

        /// <summary>
        /// Tests 'or' words of the query
        /// </summary>
        [Fact]
        public void ExtractQueryTest2()
        {
            var query = SampleCreator.CreateQueryString();
            List<string> orWords = new List<string>();
            orWords = Tokenizer.ExtractOrWords(query);
            Assert.Equal(new List<string> { "conclusion", "woultake" }, orWords);
        }

        /// <summary>
        /// Tests 'exclude' words of the query
        /// </summary>
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
