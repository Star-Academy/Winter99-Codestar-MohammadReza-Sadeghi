import java.util.*;

public class QuerySearcher
{
    Operand andOperands = new Operand();
    Operand orOperands = new Operand();
    Operand excludeOperands = new Operand();
    InvertedIndex invertedIndex;
    
    public QuerySearcher(InvertedIndex ii)
    {
        this.invertedIndex = ii;
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

    // a = and(a, b)
    // has better performance if size(a) < size(b)
    void and(HashSet<Integer> a, HashSet<Integer> b)
    {
        for (Iterator<Integer> i = a.iterator(); i.hasNext(); )
        {
            Integer doc = i.next();
            if (!b.contains(doc))
                i.remove();
        }
    }

    // This function gets a list of words and
    // returns the word which has fewest docs in the index
    String findMinWord(ArrayList<String> words)
    {
        int minimumDocSize = invertedIndex.index.size();
        String minimumWord = "";
        for (String w: words)
        {
            if (invertedIndex.index.get(w) == null)
                return w;
            if (invertedIndex.index.get(w).size() < minimumDocSize)
            {
                minimumDocSize = invertedIndex.index.get(w).size();
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
        if (invertedIndex.index.get(minimumWord) == null)
        {
            andOperands.setDocs(result);
            return;
        } 

        result.addAll(invertedIndex.index.get(minimumWord));
        for (String w: words)
            and(result, invertedIndex.index.get(w));
        andOperands.setDocs(result);
    }

    void computeOrDocs(String[] inputWords)
    {
        getOrWords(inputWords);
        ArrayList<String> words = orOperands.getWords();
        HashSet<Integer> result = new HashSet<>();
        for (String w: words)
            if (invertedIndex.index.get(w) != null)
            result.addAll(invertedIndex.index.get(w));
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

    // This method returns the maximum size of
    // documents which mush be excluded
    int getExcludeDocSize()
    {
        ArrayList<String> exWords = excludeOperands.getWords();
        int exDocSize = 0;
        for (String ew: exWords)
            if (invertedIndex.index.get(ew) != null)
                exDocSize += invertedIndex.index.get(ew).size();
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

    // This method removes exclusion documents
    // by iterating over result
    void excludingByResult(HashSet<Integer> result)
    {
        ArrayList<String> exWords = excludeOperands.getWords();
        for (Iterator<Integer> i = result.iterator(); i.hasNext(); )
        {
            Integer doc = i.next();
            for (String ew: exWords)
                if (invertedIndex.index.get(ew).contains(doc))
                    i.remove();
        }
    }

    // This method removes exclusion documents
    // by iterating over exclusion words
    void excludingByExDocs(HashSet<Integer> result)
    {
        ArrayList<String> exWords = excludeOperands.getWords();
        for (String ew: exWords)
        {
            if (invertedIndex.index.get(ew) != null)
                result.removeAll(invertedIndex.index.get(ew));
        }
    }
}