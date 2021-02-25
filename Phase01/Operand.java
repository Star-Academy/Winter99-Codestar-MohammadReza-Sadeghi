import java.util.*;

public class Operand
{
    ArrayList<String> words;
    HashSet<Integer> docs;

    public ArrayList<String> getWords()
    {
        return this.words;
    }

    public HashSet<Integer> getDocs()
    {
        return this.docs;
    }

    public void setWords(ArrayList<String> w)
    {
        this.words = w;
    }

    public void setDocs(HashSet<Integer> d)
    {
        this.docs = d;
    }
}