import java.util.*;
import java.io.*;

public class InvertedIndex
{
    private HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();

    HashMap<String, HashSet<Integer>> getIndex()
    {
        return this.index;
    }

    public void readAndAddData(String dataFolder)
    {
        final FileReader fileReader = new FileReader(dataFolder);
        ArrayList<String> docs = fileReader.read();
        for (int i = 0; i < docs.size(); i++)
        {
            String[] words = docs.get(i).split("\\W+");
            for (int j = 0; j <words.length; j++)
            {
                if (!words[j].equals(""))
                    addToIndex(words[j], i);
            }
        }
    }

    
    void addToIndex(String word, int doc)
    {
        HashSet<Integer> docList = index.get(word);
        if (!index.containsKey(word))
        {
            docList = new HashSet<Integer>();
            docList.add(doc);
            index.put(word, docList);
        }
        else
            docList.add(doc);
    }
}