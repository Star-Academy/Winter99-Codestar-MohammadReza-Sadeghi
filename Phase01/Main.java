import java.util.*;
import java.io.*;

public class Main
{
    
    public static void main(String[] args)
    {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.readAndAddData("EnglishData/");
        
        String[] inStr = Tokenizer.readAndTokenize();

        QuerySearcher qs = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = qs.search(inStr);
        Output.printSet(result);
    }

}