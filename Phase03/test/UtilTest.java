import org.junit.jupiter.api.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import static org.mockito.ArgumentMatchers.isA;
import static org.mockito.Mockito.*;
import org.mockito.plugins.MockMaker;


public class UtilTest
{
    HashSet<Integer> getHashSet1()
    {
        HashSet<Integer> a = new HashSet<>();

        a.add(2);
        a.add(0);
        a.add(-8);
        a.add(-5);
        a.add(-4);

        return a;
    }

    HashSet<Integer> getHashSet2()
    {
        HashSet<Integer> b = new HashSet<>();

        b.add(-5);
        b.add(2);
        b.add(-10);
        b.add(-9);

        return b;
    }

    HashSet<Integer> getHashSet3()
    {
        HashSet<Integer> c = new HashSet<>();

        c.add(-4);
        c.add(-6);
        c.add(-1);
        c.add(1);
        c.add(-5);

        return c;
    }

    HashMap<String, HashSet<Integer>> getIndex1()
    {
        HashMap<String, HashSet<Integer>> index = new HashMap<>();
        HashSet<Integer> a = getHashSet1();
        HashSet<Integer> b = getHashSet2();

        index.put("subject", a);
        index.put("high", b);

        return index;
    }

    @Test
    void andTest()
    {
        HashSet<Integer> a = getHashSet1();
        HashSet<Integer> b = getHashSet2();

        Util.and(a, b);
        HashSet<Integer> res = new HashSet<>();
        res.add(-5);
        res.add(2);
        assertEquals(a, res);
    }

    @Test
    void orTest()
    {
        HashSet<Integer> a = getHashSet1();
        HashSet<Integer> b = getHashSet2();

        HashSet<Integer> res = new HashSet<>();
        res.add(-5);
        res.add(2);
        res.add(0);
        res.add(-8);
        res.add(-4);
        res.add(-10);
        res.add(-9);

        Util.or(a, b);
        assertEquals(a, res);
    }

    @Test
    void findMinWordTest()
    {
        HashMap<String, HashSet<Integer>> index = getIndex1();

        ArrayList<String> words = new ArrayList<>();
        words.add("subject");
        words.add("high");

        InvertedIndex invertedIndex = mock(InvertedIndex.class);

        when(invertedIndex.getIndex()).thenReturn(index);
        String res = Util.findMinWord(words, invertedIndex);
        assertEquals("high", res);
    }

    @Test
    void findMinWordTest2()
    {
        HashMap<String, HashSet<Integer>> index = getIndex1();

        ArrayList<String> words = new ArrayList<>();
        words.add("to");
        words.add("a");

        InvertedIndex invertedIndex = mock(InvertedIndex.class);

        when(invertedIndex.getIndex()).thenReturn(index);
        String res = Util.findMinWord(words, invertedIndex);
        assertEquals("to", res);
    }

    @Test
    void sumOfDocsTest()
    {
        HashMap<String, HashSet<Integer>> index = getIndex1();

        ArrayList<String> words = new ArrayList<>();
        words.add("subject");
        words.add("high");

        InvertedIndex invertedIndex = mock(InvertedIndex.class);

        when(invertedIndex.getIndex()).thenReturn(index);
        int res = Util.sumOfDocs(words, invertedIndex);
        assertEquals(9 , res);
    }

    @Test
    void excludeByBaseSetTest()
    {
        HashMap<String, HashSet<Integer>> index = getIndex1();

        ArrayList<String> words = new ArrayList<>();
        words.add("subject");
        words.add("high");

        HashSet<Integer> baseSet = getHashSet3();

        HashSet<Integer> answer = new HashSet<>();
        answer.add(-6);
        answer.add(1);
        answer.add(-1);

        InvertedIndex invertedIndex = mock(InvertedIndex.class);

        when(invertedIndex.getIndex()).thenReturn(index);
        Util.excludeByBaseSet(baseSet, words, invertedIndex);
        assertEquals(answer , baseSet);
    }

    @Test
    void excludeByExDocsTest()
    {
        HashMap<String, HashSet<Integer>> index = getIndex1();

        ArrayList<String> words = new ArrayList<>();
        words.add("subject");
        words.add("high");

        HashSet<Integer> baseSet = getHashSet3();

        HashSet<Integer> answer = new HashSet<>();
        answer.add(-6);
        answer.add(1);
        answer.add(-1);

        InvertedIndex invertedIndex = mock(InvertedIndex.class);

        when(invertedIndex.getIndex()).thenReturn(index);
        Util.excludeByExDocs(baseSet, words, invertedIndex);
        assertEquals(answer , baseSet);
    }
}
