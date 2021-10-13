using Phase05.IO;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Test.IOTest.FileTest
{
    public class FileReaderTest : IDisposable
    {
        private static readonly string directoryPath = @"../../../../Test/SampleData/";
        private static readonly string[] fileNames = new string[] { "a.txt", "b.txt", "c.txt" };

        public FileReaderTest()
        {
            Directory.CreateDirectory(directoryPath);
            string filePath = directoryPath + fileNames[0];
            if (!File.Exists(filePath))
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write("is what got me");
                }
            filePath = directoryPath + fileNames[1];
            if (!File.Exists(filePath))
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write("h>subject to a high-voltag");
                }
            filePath = directoryPath + fileNames[2];
            if (!File.Exists(filePath))
                using (StreamWriter sw = File.CreateText(filePath)){}
        }

        public void Dispose()
        {
            foreach (string fileName in fileNames)
                if (File.Exists(Path.Combine(directoryPath, fileName)))
                    File.Delete(Path.Combine(directoryPath, fileName));
            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath);
        }

        [Fact]
        public void ReadFromFolderTest()
        {
            string path = @"../../../../Test/SampleData/";
            var documents = new List<string>();
            documents.Add("is what got me");
            documents.Add("h>subject to a high-voltag");
            Assert.Equal(documents, FileReader.ReadFromFolder(path));
        }

        [Fact]
        public void ReadFromFolderTest2()
        {
            string path = "../../../not_existed_path/";
            Assert.Equal(new List<string> { }, FileReader.ReadFromFolder(path));
        }

        [Fact]
        public void ReadFileTest()
        {
            string path = @"../../../../Test/SampleData/a.txt";
            Assert.Equal("is what got me", FileReader.ReadFile(path));
        }
    }
}
