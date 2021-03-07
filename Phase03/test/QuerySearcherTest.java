import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class QuerySearcherTest
{
    String[] createStrArray()
    {
        return new String[]{"get", "help", "+illness", "+disease", "-cough"};
    }

    HashSet<Integer> createHashSet(int addNumber)
    {
        HashSet<Integer> hashSet = new HashSet<>();
        hashSet.add(addNumber);
        return hashSet;
    }

    HashMap<String, HashSet<Integer>> createIndex()
    {
        HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
        index.put("get", createHashSet(0));
        index.put("help", createHashSet(0));
        index.put("illness", createHashSet(0));

        index.put("disease", createHashSet(1));
        index.put("cough", createHashSet(1));

        return index;
    }

    @Test
    void setAndWordsTest()
    {
        ArrayList<String> words = new ArrayList<>();
        words.add("get");
        words.add("help");

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.setAndWords(createStrArray());
        assertEquals(words, querySearcher.getAndOperands().getWords());
    }

    @Test
    void setExcludeWordsTest()
    {
        ArrayList<String> words = new ArrayList<>();
        words.add("cough");

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.setExcludeWords(createStrArray());
        assertEquals(words, querySearcher.getExcludeOperands().getWords());
    }

    @Test
    void setOrWordsTest()
    {
        ArrayList<String> words = new ArrayList<>();
        words.add("illness");
        words.add("disease");

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.setOrWords(createStrArray());
        assertEquals(words, querySearcher.getOrOperands().getWords());
    }

    @Test
    void computeAndDocsTest()
    {
        HashSet<Integer> docs = new HashSet<>();
        docs.add(0);

        HashMap<String, HashSet<Integer>> index = createIndex();

        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);

        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);
        querySearcher.computeAndDocs(createStrArray());
        assertEquals(docs, querySearcher.getAndOperands().getDocs());
    }

    @Test
    void computeAndDocsTest2()
    {
        HashMap<String, HashSet<Integer>> index = createIndex();
        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);


        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);
        querySearcher.computeAndDocs(new String[]{"a"});
        assertEquals(new HashSet<Integer>(), querySearcher.getAndOperands().getDocs());
    }

    @Test
    void computeOrDocsTest()
    {
        HashSet<Integer> docs = new HashSet<>();
        docs.add(0);
        docs.add(1);

        HashMap<String, HashSet<Integer>> index = createIndex();

        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);

        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);
        querySearcher.computeOrDocs(createStrArray());
        assertEquals(docs, querySearcher.getOrOperands().getDocs());
    }

    @Test
    void andResultsTest()
    {
        HashSet<Integer> docs = createHashSet(0);

        HashSet<Integer> or = createHashSet(0);
        or.add(1);

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.getAndOperands().setDocs(createHashSet(0));
        querySearcher.getOrOperands().setDocs(or);

        ArrayList<String> andWords = new ArrayList<>();
        andWords.add("get");
        andWords.add("help");
        querySearcher.getAndOperands().setWords(andWords);

        assertEquals(docs, querySearcher.andResults());
    }

    @Test
    void andResultsTest2()
    {
        HashSet<Integer> docs = createHashSet(0);
        docs.add(1);

        HashSet<Integer> or = createHashSet(0);
        or.add(1);

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.getAndOperands().setDocs(createHashSet(0));
        querySearcher.getOrOperands().setDocs(or);

        querySearcher.getAndOperands().setWords(new ArrayList<String>());
        assertEquals(docs, querySearcher.andResults());
    }

    @Test
    void andResultsTest3()
    {
        HashSet<Integer> docs = createHashSet(0);
        docs.add(1);

        HashSet<Integer> and = createHashSet(0);
        and.add(2);
        and.add(1);

        HashSet<Integer> or = createHashSet(0);
        or.add(1);

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.getAndOperands().setDocs(and);
        querySearcher.getOrOperands().setDocs(or);

        ArrayList<String> orWords = new ArrayList<>();
        orWords.add("disease");
        orWords.add("disease");
        querySearcher.getOrOperands().setWords(orWords);

        assertEquals(docs, querySearcher.andResults());
    }

    @Test
    void andResultsTest4()
    {
        HashSet<Integer> docs = createHashSet(0);
        docs.add(1);
        docs.add(2);

        HashSet<Integer> and = createHashSet(0);
        and.add(2);
        and.add(1);

        HashSet<Integer> or = createHashSet(0);
        or.add(1);

        QuerySearcher querySearcher = new QuerySearcher(new InvertedIndex());
        querySearcher.getAndOperands().setDocs(and);
        querySearcher.getOrOperands().setDocs(or);
        querySearcher.getOrOperands().setWords(new ArrayList<String>());

        assertEquals(docs, querySearcher.andResults());
    }

    @Test
    void removeExcludeDocsTest()
    {
        HashMap<String, HashSet<Integer>> index = createIndex();
        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);

        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = createHashSet(0);
        querySearcher.removeExcludeDocs(createStrArray(), result);
        assertEquals(createHashSet(0), result);
    }

    @Test
    void removeExcludeDocsTest2()
    {
        HashMap<String, HashSet<Integer>> index = createIndex();
        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);

        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = createHashSet(0);

        querySearcher.removeExcludeDocs(new String[]{"a"}, result);
        assertEquals(createHashSet(0), result);
    }

    @Test
    void removeExcludeDocsTest3()
    {
        HashMap<String, HashSet<Integer>> index = createIndex();
        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);

        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);
        HashSet<Integer> result = createHashSet(0);

        index.get("cough").add(0);
        querySearcher.removeExcludeDocs(createStrArray(), result);
        assertEquals(new HashSet<Integer>(), result);
    }

    @Test
    void searchTest()
    {
        HashMap<String, HashSet<Integer>> index = createIndex();
        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getIndex()).thenReturn(index);
        QuerySearcher querySearcher = new QuerySearcher(invertedIndex);

        assertEquals(createHashSet(0), querySearcher.search(createStrArray()));
    }
}
