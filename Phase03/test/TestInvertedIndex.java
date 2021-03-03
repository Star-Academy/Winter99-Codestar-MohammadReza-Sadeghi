import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

public class TestInvertedIndex
{
    HashSet<Integer> createHashSet(int addNumber)
    {
        HashSet<Integer> hashSet = new HashSet<>();
        hashSet.add(addNumber);
        return hashSet;
    }

    @Test
    void testAddData()
    {
        ArrayList<String> docs = new ArrayList<>();
        docs.add("o talk.politics");
        docs.add(" Banks  N3JXP      | \"Skep");

        HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
        index.put("o", createHashSet(0));
        index.put("talk", createHashSet(0));
        index.put("politics", createHashSet(0));

        index.put("Banks", createHashSet(1));
        index.put("N3JXP", createHashSet(1));
        index.put("Skep", createHashSet(1));

        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addData(docs);
        assertEquals(invertedIndex.getIndex(), index);
    }

    @Test
    void testAddToIndex()
    {
        HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
        index.put("N3JXP", createHashSet(197));
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addToIndex("N3JXP", 197);
        assertEquals(index, invertedIndex.getIndex());
    }
}
