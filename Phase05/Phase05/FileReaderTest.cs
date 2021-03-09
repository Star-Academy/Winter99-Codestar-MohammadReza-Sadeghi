using System.Collections;
using Xunit;

namespace Phase05
{
    public class FileReaderTest
    {
        [Fact]
        public void ReadFromFolderTest()
        {
            string path = "../../../SampleData/";
            ArrayList documents = new ArrayList();
            documents.Add("is what got me");
            documents.Add("h>subject to a high-voltag");
            Assert.Equal(documents, FileReader.ReadFromFolder(path));
        }

        [Fact]
        public void ReadFromFolderTest2()
        {
            string path = "../../../SampleData/";
            Assert.Equal(new ArrayList(), FileReader.ReadFromFolder(path));
        }
    }
}
