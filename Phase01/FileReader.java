import java.util.*;
import java.io.*;

public class FileReader
{
    String path;
    public FileReader(String p)
    {
        path = p;
    }

    public ArrayList<String> read() throws IOException
    {
        File folder = new File(path);
        File[] listOfFiles = folder.listFiles();
        ArrayList<String> docs = new ArrayList<>();
        for (File file: listOfFiles)
        {
            Scanner scanner = new Scanner(file);
            if (scanner.hasNext())
            {
                String fileData = scanner.useDelimiter("\\A").next();
                docs.add(fileData.toLowerCase());
            }
            scanner.close();
        }
        return docs;
    }
}