using System;
using System.Collections.Generic;
using System.IO;

namespace Phase05
{
    public class FileReader
    {
        public static List<string> ReadFromFolder(string PathFolder)
        {
            var documents = new List<string>();
            try
            {
                foreach (string file in Directory.EnumerateFiles(PathFolder, "*"))
                {
                    string content = File.ReadAllText(file);
                    if (!content.Equals(""))
                        documents.Add(content);
                }
            }
            catch 
            {
                Console.WriteLine("Invalid path :)");
            }
            return documents;
        }
    }
}
