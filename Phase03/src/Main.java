import java.util.*;
import java.io.*;

public class Main
{

    public static void main(String[] args) throws FileNotFoundException {
        InvertedIndex invertedIndex = new InvertedIndex();
        ArrayList<String> docs = FileReader.read("EnglishData/");
        invertedIndex.addData(docs);

        String[] inStr = InputTokenizer.readAndTokenize();

        QuerySearcher qs = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = qs.search(inStr);
        Output.printSet(result);
    }

}