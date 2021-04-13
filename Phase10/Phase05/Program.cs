using Phase05.Elastic;
using Phase05.IO;
using Phase05.Search;

namespace Phase05
{
    public class Program
    {
        private static readonly string folderPath = @"..\..\..\..\EnglishData\";
        private static readonly string indexName = "simple_docs";

        static void Main(string[] args)
        {
            var documents = FileReader.ReadFromFolder(folderPath);
            var invertedIndex = InitializeInvertedIndex();
            invertedIndex.AddDocuments(documents);
            var inputStr = Input.ReadFromConsole();
            var searchEngine = new SearchEngine(invertedIndex);
            var result = searchEngine.SearchQuery(inputStr);
            Output.PrintStringList(result);
        }

        private static InvertedIndex InitializeInvertedIndex()
        {
            ElasticIndex elasticIndex = new ElasticIndex();
            InvertedIndex invertedIndex = new InvertedIndex(elasticIndex, indexName);
            return invertedIndex;
        }
    }
}
