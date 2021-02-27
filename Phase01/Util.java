import java.util.*;

public class Util
{
    
    /**
     * a = and(a, b)
     * has better performance if size(a) < size(b)
     * 
     * @param a hashSet
     * @param b hashset
     */
    static void and(HashSet<Integer> a, HashSet<Integer> b)
    {
        // for (Iterator<Integer> i = a.iterator(); i.hasNext(); )
        // {
        //     Integer doc = i.next();
        //     if (!b.contains(doc))
        //         i.remove();
        // }
        a.removeIf(doc -> !b.contains(doc));
    }

    /**
     * a = or(a, b)
     * 
     * @param a hashSet
     * @param b hashset
     */
    static void or(HashSet<Integer> a, HashSet<Integer> b)
    {
        a.addAll(b);
    }

    /**
     * 
     * @param words a list of words
     * @return the word which has fewest docs in the index
     */
    static String findMinWord(ArrayList<String> words, InvertedIndex invertedIndex)
    {
        int minimumDocSize = invertedIndex.getIndex().size();
        String minimumWord = "";
        for (String w: words)
        {
            if (!invertedIndex.getIndex().containsKey(w))
                return w;
            if (invertedIndex.getIndex().get(w).size() < minimumDocSize)
            {
                minimumDocSize = invertedIndex.getIndex().get(w).size();
                minimumWord = w;
            }
        }
        return minimumWord;
    }

    /**
     * @param words a list of words
     * @return the sum of number of documents each word has in the invertedIndex
     */
    static int sumOfDocs(ArrayList<String> words, InvertedIndex invertedIndex)
    {
        // ArrayList<String> exWords = excludeOperands.getWords();
        int sum = 0;
        for (String w: words)
            if (invertedIndex.getIndex().containsKey(w))
                sum += invertedIndex.getIndex().get(w).size();
        return sum;
    }

    /**
     * This method removes documents in excludeWords from baseSet by iterating over baseSet
     *
     * @param baseSet the set we want to remove excludeWords from
     * @param excludeWords the list that must be excluded
     */
    static void excludeByBaseSet(HashSet<Integer> baseSet, ArrayList<String> excludeWords, InvertedIndex invertedIndex)
    {
        // for (Iterator<Integer> i = baseSet.iterator(); i.hasNext(); )
        // {
        //     Integer doc = i.next();
            for (String ew: excludeWords)
        //         if (invertedIndex.getIndex().get(ew).contains(doc))
        //             i.remove();
        // }
        baseSet.removeIf(doc -> invertedIndex.getIndex().get(ew).contains(doc));
    }

    /**
     * This method removes edocuments in excludeWords from baseSet by iterating over exclusion words
     *
     * @param baseSet the set we want to remove excludeWords from
     * @param excludeWords the list that must be excluded
     */
    static void excludeByExDocs(HashSet<Integer> baseSet, ArrayList<String> excludeWords, InvertedIndex invertedIndex)
    {
        for (String ew: excludeWords)
            if (invertedIndex.getIndex().containsKey(ew))
                baseSet.removeAll(invertedIndex.getIndex().get(ew));
    }
}