import java.util.ArrayList;
import java.util.HashSet;

public class Output
{
    public static void printSet(HashSet<Integer> set, ArrayList<String> docs)
    {
        for (Integer s: set)
        {
            System.out.println(s);
            System.out.println("------------------This is document:");
            System.out.println(docs.get(s));
            System.out.println();
            System.out.println();
            System.out.println();
            System.out.println();
            System.out.println();
        }
    }
}