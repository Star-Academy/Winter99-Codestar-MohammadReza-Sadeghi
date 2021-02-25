import java.util.*;

public class Operand
{
    private ArrayList<String> words;
    private HashSet<Integer> docs;

    public ArrayList<String> getWords()
    {
        return this.words;
    }

    public HashSet<Integer> getDocs()
    {
        return this.docs;
    }

    public void setWords(ArrayList<String> words)
    {
        this.words = words;
    }

    public void setDocs(HashSet<Integer> docs)
    {
        this.docs = docs;
    }
}