import java.util.*;
import java.io.*;

public class Main
{
    
    public static void main(String[] args)
    {
        InvertedIndex invertedIndex = new InvertedIndex();
        final FileReader fileReader = new FileReader("EnglishData/");
        ArrayList<String> docs = fileReader.read();
        invertedIndex.addData(docs);
        
        String[] inStr = Tokenizer.readAndTokenize();

        QuerySearcher qs = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = qs.search(inStr);
        Output.printSet(result);
    }

}