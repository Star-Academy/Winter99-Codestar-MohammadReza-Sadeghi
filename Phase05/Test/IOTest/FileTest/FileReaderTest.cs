using Phase05.IO;
using System.Collections.Generic;
using Xunit;

namespace Test.IOTest.FileTest
{
    public class FileReaderTest
    {
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
            string path = "../../../abc/";
            Assert.Equal(new List<string> { }, FileReader.ReadFromFolder(path));
        }
    }
}
