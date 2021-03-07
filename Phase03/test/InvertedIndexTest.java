import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

public class InvertedIndexTest
{

    @Test
    void addDataTest()
    {
        ArrayList<String> docs = new ArrayList<>();
        docs.add("o talk.politics");
        docs.add(" Banks  N3JXP      | \"Skep");

        HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
        index.put("o", Creator.createHashSet(0));
        index.put("talk", Creator.createHashSet(0));
        index.put("politics", Creator.createHashSet(0));

        index.put("Banks", Creator.createHashSet(1));
        index.put("N3JXP", Creator.createHashSet(1));
        index.put("Skep", Creator.createHashSet(1));

        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addData(docs);
        assertEquals(invertedIndex.getIndex(), index);
    }

    @Test
    void addToIndexTest()
    {
        HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
        index.put("N3JXP", Creator.createHashSet(197));
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addToIndex("N3JXP", 197);
        assertEquals(index, invertedIndex.getIndex());
    }
}
