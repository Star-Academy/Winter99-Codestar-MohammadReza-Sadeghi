using System.Collections.Generic;
using System.IO;

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
                    string content = File.ReadAllText(file);
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
    }
}
