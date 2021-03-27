using Moq;
using Phase05.Search;
using System.Collections.Generic;
using TypeMock.ArrangeActAssert;

namespace Test.Mock
{
    public class InvertedIndexMock
    {
        private static readonly Dictionary<string, HashSet<int>> Index = SampleCreator.CreateIndex();

        public static void MockIndex(Mock<InvertedIndex> mockIndex)
        {
            /*mockIndex = Isolate.Fake.Instance<InvertedIndex>();
            Isolate.WhenCalled(() => mockIndex.GetDocsByWord(null)).DoInstead(contaxt =>
            {
                return GetDocsByWordMock(contaxt.Parameters[0] as string);
            });
            Isolate.WhenCalled(() => mockIndex.ContainsWord(null)).DoInstead(contaxt =>
            {
                return ContainsWordMock(contaxt.Parameters[0] as string);
            });*/
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
