import java.util.*;
import java.io.*;

public class FileReader
{
    private static final String delimiter = "\\A";

    public static ArrayList<String> read(String path) throws FileNotFoundException {
        final File folder = new File(path);
         File[] listOfFiles = folder.listFiles();
        listOfFiles = listOfFiles != null ? listOfFiles : new File[0];
        final ArrayList<String> docs = new ArrayList<>();
        for (File file: listOfFiles)
        {
            Scanner scanner = new Scanner(file);
            if (scanner.hasNext())
            {
                String fileData = scanner.useDelimiter(delimiter).next();
                docs.add(Tokenizer.tokenize(fileData));
            }
            scanner.close();
        }
        return docs;
    }
}