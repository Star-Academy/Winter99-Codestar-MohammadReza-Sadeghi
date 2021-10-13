using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phase05.IO
{
    public class FileReader
    {
        public static List<string> ReadFromFolder(string folderPath)
        {
            var documents = new List<string>();
            try
            {
                foreach (string file in Directory.EnumerateFiles(folderPath, "*"))
                {
                    string content = ReadFile(file);
                    if (!string.IsNullOrEmpty(content))
                        documents.Add(content);
                }
            }
            catch
            {
                Output.PrintString("Invalid path :)");
            }
            return documents;
        }

        public static string ReadFile(string file)
        {
            return File.ReadAllText(file);
        }
    }
}
