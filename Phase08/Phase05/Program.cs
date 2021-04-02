using Microsoft.EntityFrameworkCore;
using Phase05.IO;
using Phase05.Search;

namespace Phase05
{
    public class Program
    {
        private static readonly string folderPath = @"..\..\..\..\EnglishData\";
        private static readonly string sqlServer = @"Server=.\MRSADEGHI78;Database=InvertedIndexDB;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            var documents = FileReader.ReadFromFolder(folderPath);
            var options = new DbContextOptionsBuilder<InvertedIndex>()
                .UseSqlServer(sqlServer)
                .Options;
            var invertedIndex = new InvertedIndex(options);
            invertedIndex.Database.EnsureCreated();
            invertedIndex.CreateIndex(documents);
            var inputStr = Input.ReadFromConsole();
            var searchEngine = new SearchEngine(invertedIndex);
            var result = searchEngine.SearchQuery(inputStr);
            Output.PrintSet(result);
        }
    }
}
