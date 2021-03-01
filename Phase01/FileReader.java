import java.util.*;
import java.io.*;

public class FileReader
{
    private static final String delimiter = "\\A";

    public static ArrayList<String> read(String path)
    {
        final File folder = new File(path);
        final File[] listOfFiles = folder.listFiles();
        final ArrayList<String> docs = new ArrayList<>();
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
                String fileData = scanner.useDelimiter(delimiter).next();
                docs.add(InputTokenizer.tokenize(fileData));
            }
            scanner.close();
        }
        return docs;
    }
}