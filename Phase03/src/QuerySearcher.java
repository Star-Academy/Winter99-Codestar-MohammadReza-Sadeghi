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

    public Operand getAndOperands() {
        return andOperands;
    }

    public Operand getOrOperands() {
        return orOperands;
    }

    public Operand getExcludeOperands() {
        return excludeOperands;
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

    void setAndWords(String[] inputWords)
    {
        getAndOperands().setWords(Tokenizer.extractAndWords(inputWords));
    }

    void setOrWords(String[] inputWords)
    {
        getOrOperands().setWords(Tokenizer.extractOrWords(inputWords));
    }

    void setExcludeWords(String[] inputWords)
    {
        getExcludeOperands().setWords(Tokenizer.extractExcludeWords(inputWords));
    }

    void computeAndDocs(String[] inputWords)
    {
        setAndWords(inputWords);
        ArrayList<String> words = getAndOperands().getWords();
        HashSet<Integer> result = new HashSet<>();

        String minimumWord = Util.findMinWord(words, this.invertedIndex);
        if (!invertedIndex.getIndex().containsKey(minimumWord))
        {
            getAndOperands().setDocs(result);
            return;
        }

        result.addAll(invertedIndex.getIndex().get(minimumWord));
        for (String w: words)
            Util.and(result, invertedIndex.getIndex().get(w));
        getAndOperands().setDocs(result);
    }

    void computeOrDocs(String[] inputWords)
    {
        setOrWords(inputWords);
        ArrayList<String> words = getOrOperands().getWords();
        HashSet<Integer> result = new HashSet<>();
        for (String w: words)
            if (invertedIndex.getIndex().containsKey(w))
                Util.or(result, invertedIndex.getIndex().get(w));
        getOrOperands().setDocs(result);
    }

    HashSet<Integer> andResults()
    {
        HashSet<Integer> result;
        HashSet<Integer> orDocs = getOrOperands().getDocs();
        HashSet<Integer> andDocs = getAndOperands().getDocs();
        if (orDocs.size() < andDocs.size())
        {
            if (getOrOperands().getWords().size() > 0)
            {
                Util.and(orDocs, andDocs);
                result = orDocs;
            }
            else
                result = andDocs;
        }
        else
        {
            if (getAndOperands().getWords().size() > 0)
            {
                Util.and(andDocs, orDocs);
                result = andDocs;
            }
            else
                result = orDocs;
        }
        return result;
    }

    void removeExcludeDocs(String[] inputWords, HashSet<Integer> result)
    {
        setExcludeWords(inputWords);
        ArrayList<String> exWords = getExcludeOperands().getWords();

        int exDocSize = Util.sumOfDocs(exWords, this.invertedIndex);
        int exWordSize = exWords.size();
        int resSize = result.size();

        if (exWordSize == 0 || exDocSize == 0 || resSize == 0)
            return;

        // Here we decide for removing exclusion ducuments,
        // iterating over result has less cost or iterating over exclusion docs
        if (resSize * exWordSize < exDocSize)
            Util.excludeByBaseSet(result, exWords, this.invertedIndex);
        else
            Util.excludeByExDocs(result, exWords, this.invertedIndex);
    }

}