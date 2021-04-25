using Moq;
using Phase05.Search;
using System.Collections.Generic;

namespace Test.Mock
{
    public class InvertedIndexMock
    {
        private static readonly Dictionary<string, HashSet<int>> Index = SampleCreator.CreateIndex();

        public static void MockIndex(Mock<InvertedIndex> mockIndex)
        {
            mockIndex.Setup(x => x.GetDocsByWord(It.IsAny<string>())).Returns((string word) => GetDocsByWordMock(word));
            mockIndex.Setup(x => x.ContainsWord(It.IsAny<string>())).Returns((string word) => ContainsWordMock(word));
        }
        public static HashSet<int> GetDocsByWordMock(string word)
        {
            return Index[word];
        }

        public static bool ContainsWordMock(string word)
        {
            return Index.ContainsKey(word);
        }
    }
}
