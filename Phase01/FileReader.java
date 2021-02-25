import java.util.*;
import java.io.*;

public class FileReader
{
    private String path;
    public FileReader(String path)
    {
        this.path = path;
    }

    public ArrayList<String> read()
    {
        final File folder = new File(path);
        final File[] listOfFiles = folder.listFiles();
        ArrayList<String> docs = new ArrayList<>();
        for (File file: listOfFiles)
        {
            Scanner scanner;
            try {
                scanner = new Scanner(file);
            } catch (IOException e) {
                System.out.println(e);
                return docs;
            }
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