import java.util.*;
import java.io.*;

public class Main
{
    final static String inputDir = "EnglishData/";

    public static void main(String[] args) throws FileNotFoundException
    {
        QuerySearcher qs = createSearchEngine();
        String[] inStr = Input.readAndTokenize();
        HashSet<Integer> result = qs.search(inStr);
        Output.printSet(result);
    }

    static QuerySearcher createSearchEngine() throws FileNotFoundException {
        InvertedIndex invertedIndex = new InvertedIndex();
        ArrayList<String> docs = FileReader.read(inputDir);
        invertedIndex.addData(docs);
        return new QuerySearcher(invertedIndex);
    }
}