using Phase05.IO;
using Phase05.Search;

namespace Phase05
{
    public class Program
    {
        private static readonly string folderPath = @"..\..\..\..\EnglishData\";

        static void Main(string[] args)
        {
            var documents = FileReader.ReadFromFolder(folderPath);
            var invertedIndex = new InvertedIndex();
            invertedIndex.CreateIndex(documents);
            var inputStr = Input.ReadFromConsole();
            var searchEngine = new SearchEngine(invertedIndex);
            var result = searchEngine.SearchQuery(inputStr);
            Output.PrintSet(result);
        }
    }
}
