import java.util.*;

public class QuerySearcher
{
    private Operand andOperands = new Operand();
    private Operand orOperands = new Operand();
    private Operand excludeOperands = new Operand();
    private InvertedIndex invertedIndex;
    
    public QuerySearcher(InvertedIndex invertedIndex)
    {
        this.invertedIndex = invertedIndex;
    }

    public HashSet<Integer> search(String[] inputWords)
    {
        computeAndDocs(inputWords);
        computeOrDocs(inputWords);
     
        // This method computes and of 2 previous results
        HashSet<Integer> result = andResults();
        
        removeExcludeDocs(inputWords, result);
        return result;
    }

    void getAndWords(String[] inputWords)
    {
        ArrayList<String> andWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) != '+' && w.charAt(0) != '-')
                andWords.add(w);
        andOperands.setWords(andWords);
    }

    void getOrWords(String[] inputWords)
    {
        ArrayList<String> orWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) == '+')
                orWords.add(w.substring(1));
        orOperands.setWords(orWords);
    }

    void getExcludeWords(String[] inputWords)
    {
        ArrayList<String> exWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) == '-')
                exWords.add(w.substring(1));
        excludeOperands.setWords(exWords);
    }

    /**
     * a = and(a, b)
     * 
     * @param two hashSets
     * @return nothing(and(a, b) is stored in a)
     */
    void and(HashSet<Integer> a, HashSet<Integer> b)
    {
        for (Iterator<Integer> i = a.iterator(); i.hasNext(); )
        {
            Integer doc = i.next();
            if (!b.contains(doc))
                i.remove();
        }
    }

    /**
     * 
     * @param a list of words
     * @return the word which has fewest docs in the index
     */
    String findMinWord(ArrayList<String> words)
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

    void computeAndDocs(String[] inputWords)
    {
        getAndWords(inputWords);
        ArrayList<String> words = andOperands.getWords();
        HashSet<Integer> result = new HashSet<>();

        String minimumWord = findMinWord(words);
        if (invertedIndex.getIndex().get(minimumWord) == null)
        {
            andOperands.setDocs(result);
            return;
        } 

        result.addAll(invertedIndex.getIndex().get(minimumWord));
        for (String w: words)
            and(result, invertedIndex.getIndex().get(w));
        andOperands.setDocs(result);
    }

    void computeOrDocs(String[] inputWords)
    {
        getOrWords(inputWords);
        ArrayList<String> words = orOperands.getWords();
        HashSet<Integer> result = new HashSet<>();
        for (String w: words)
            if (invertedIndex.getIndex().get(w) != null)
            result.addAll(invertedIndex.getIndex().get(w));
        orOperands.setDocs(result);
    }

    HashSet<Integer> andResults()
    {
        HashSet<Integer> result;
        HashSet<Integer> orDocs = orOperands.getDocs();
        HashSet<Integer> andDocs = andOperands.getDocs();
        if (orDocs.size() < andDocs.size())
        {
            if (orOperands.getWords().size() > 0)
            {
                and(orDocs, andDocs);
                result = orDocs;
            }
            else
            result = andDocs;
        }
        else
        {
            if (andOperands.getWords().size() > 0)
            {
                and(andDocs, orDocs);
                result = andDocs;
            }
            else
            result = orDocs;
        }
        return result;
    }

    /**
     * 
     * @return the maximum size of documents which mush be excluded
     */
    int getExcludeDocSize()
    {
        ArrayList<String> exWords = excludeOperands.getWords();
        int exDocSize = 0;
        for (String ew: exWords)
            if (invertedIndex.getIndex().get(ew) != null)
                exDocSize += invertedIndex.getIndex().get(ew).size();
        return exDocSize;
    }

    void removeExcludeDocs(String[] inputWords, HashSet<Integer> result)
    {
        getExcludeWords(inputWords);
        ArrayList<String> exWords = excludeOperands.getWords();
        
        int exDocSize = getExcludeDocSize();
        int exWordSize = exWords.size();
        int resSize = result.size();
        
        if (exWordSize == 0 || exDocSize == 0 || resSize == 0)
            return;

        // Here we decide for removing exclusion ducuments,
        // iterating over result has less cost or iterating over exclusion docs
        if (resSize * exWordSize < exDocSize)
            excludingByResult(result);            
        else
            excludingByExDocs(result);
    }

    /**
     * This method removes exclusion documents by iterating over result
     *
     * @param result of and & or documents calculated before
     */
    void excludingByResult(HashSet<Integer> result)
    {
        ArrayList<String> exWords = excludeOperands.getWords();
        for (Iterator<Integer> i = result.iterator(); i.hasNext(); )
        {
            Integer doc = i.next();
            for (String ew: exWords)
                if (invertedIndex.getIndex().get(ew).contains(doc))
                    i.remove();
        }
    }

    /**
     * This method removes exclusion documents by iterating over exclusion words
     *
     * @param result of and & or documents calculated before
     */
    void excludingByExDocs(HashSet<Integer> result)
    {
        ArrayList<String> exWords = excludeOperands.getWords();
        for (String ew: exWords)
        {
            if (invertedIndex.getIndex().get(ew) != null)
                result.removeAll(invertedIndex.getIndex().get(ew));
        }
    }
}