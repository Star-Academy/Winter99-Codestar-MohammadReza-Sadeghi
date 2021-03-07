import java.util.*;
import java.io.*;

public class InvertedIndex
{
    private HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
    final static String documentSplit = "\\W+";

    HashMap<String, HashSet<Integer>> getIndex()
    {
        return this.index;
    }

    public void addData(ArrayList<String> docs)
    {
        for (int i = 0; i < docs.size(); i++)
        {
            String[] words = docs.get(i).split(documentSplit);
            for (String word : words)
                if (!word.equals(""))
                    addToIndex(word, i);
        }
    }


    void addToIndex(String word, int doc)
    {
        HashSet<Integer> docList = index.get(word);
        if (!index.containsKey(word))
            docList = new HashSet<Integer>();
        docList.add(doc);
        index.put(word, docList);
    }
}