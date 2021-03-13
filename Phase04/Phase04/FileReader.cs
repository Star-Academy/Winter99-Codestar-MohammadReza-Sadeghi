using System.IO;
using Newtonsoft.Json;


namespace Phase04
{
    class FileReader
    {
        public static string Read(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
