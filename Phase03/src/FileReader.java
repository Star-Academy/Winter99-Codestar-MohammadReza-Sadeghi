import java.util.*;
import java.io.*;

public class FileReader
{
    private static final String delimiter = "\\A";
    // static HashMap<Integer, String> fileNames = new HashMap<>();

    public static ArrayList<String> read(String path) throws FileNotFoundException {
        final File folder = new File(path);
        final File[] listOfFiles = folder.listFiles();
        final ArrayList<String> docs = new ArrayList<>();
        for (File file: listOfFiles != null ? listOfFiles : new File[0])
        {
            Scanner scanner;
            scanner = new Scanner(file);
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