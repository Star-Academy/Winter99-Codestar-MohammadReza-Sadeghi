import java.util.*;
import java.io.*;

public class Main
{
    
    public static void main(String[] args) throws IOException
    {
        Scanner scanner = new Scanner(System.in);

        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.readData("EnglishData/");
        
        String[] inStr = scanner.nextLine().toLowerCase().split(" ");

        QuerySearcher qs = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = qs.search(inStr);

        for (Integer d: result)
        {
            System.out.println(d);
        }
    }


    
}