import java.util.*;
import java.io.*;

class FileReader
{
    String path;
    public FileReader(String p)
    {
        path = p;
    }

    public ArrayList<String> read() throws IOException
    {
        File folder = new File(path);
        File[] listOfFiles = folder.listFiles();
        ArrayList<String> docs = new ArrayList<>();
        for (File file: listOfFiles)
        {
            Scanner scanner = new Scanner(file);
            if (scanner.hasNext())
            {
                String fileData = scanner.useDelimiter("\\A").next();
                docs.add(fileData.toLowerCase());
            }
            scanner.close();
        }
        return docs;
    }
}

public class InvertedIndex
{
    static HashMap<String, HashSet<Integer>> index = new HashMap<String, HashSet<Integer>>();
    
    public static void main(String[] args) throws IOException
    {
        Scanner scanner = new Scanner(System.in);
        FileReader fileReader = new FileReader("EnglishData/");
        ArrayList<String> docs = fileReader.read();
        for (int i = 0; i < docs.size(); i++)
        {
            String[] words = docs.get(i).split("\\W+");
            for (int j = 0; j <words.length; j++)
            {
                if (!words[j].equals(""))
                    addToIndex(words[j], i);
            }
        }
        String[] inStr = scanner.nextLine().toLowerCase().split(" ");

        ArrayList<String> andWords = getAndWords(inStr);
        HashSet<Integer> andDocs = computeAndDocs(andWords);

        ArrayList<String> orWords = getOrWords(inStr);
        HashSet<Integer> orDocs = computeOrDocs(orWords);
        
        HashSet<Integer> result;
        if (orDocs.size() < andDocs.size())
        {
            if (orWords.size() > 0)
            {
                and(orDocs, andDocs);
                result = orDocs;
            }
            else
            result = andDocs;
        }
        else
        {
            if (andWords.size() > 0)
            {
                and(andDocs, orDocs);
                result = andDocs;
            }
            else
            result = orDocs;
        }

        ArrayList<String> exWords = getExcludeWords(inStr);
        removeExcludeDocs(exWords, result);

        for (Integer d: result)
        {
            System.out.println(d);
        }
    }

    static void addToIndex(String word, int doc)
    {
        HashSet<Integer> docList = index.get(word);
        if (docList == null)
        {
            docList = new HashSet<Integer>();
            docList.add(doc);
            index.put(word, docList);
        }
        else
            docList.add(doc);
    }

    static ArrayList<String> getAndWords(String[] inputWords)
    {
        ArrayList<String> andWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) != '+' && w.charAt(0) != '-')
                andWords.add(w);
        return andWords;
    }

    static ArrayList<String> getOrWords(String[] inputWords)
    {
        ArrayList<String> orWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) == '+')
                orWords.add(w.substring(1));
        return orWords;
    }

    static ArrayList<String> getExcludeWords(String[] inputWords)
    {
        ArrayList<String> exWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) == '-')
                exWords.add(w.substring(1));
        return exWords;
    }

    // a = and(a, b)
    // have better performance if size(a) < size(b)
    static void and(HashSet<Integer> a, HashSet<Integer> b)
    {
        for (Iterator<Integer> i = a.iterator(); i.hasNext(); )
        {
            Integer doc = i.next();
            if (!b.contains(doc))
                i.remove();
        }
    }

    static HashSet<Integer> computeAndDocs(ArrayList<String> words)
    {
        HashSet<Integer> result = new HashSet<>();
        if (words.size() == 0)
            return result;

        // This part can be another function
        int minimumDocSize = index.size();
        String minimumWord = "";
        for (String w: words)
        {
            if (index.get(w) == null)
                return result;
            if (index.get(w).size() < minimumDocSize)
            {
                minimumDocSize = index.get(w).size();
                minimumWord = w;
            }
        }


        result.addAll(index.get(minimumWord));
        for (String w: words)
            and(result, index.get(w));
        return result;
    }

    static HashSet<Integer> computeOrDocs(ArrayList<String> words)
    {
        HashSet<Integer> result = new HashSet<>();
        for (String w: words)
            if (index.get(w) != null)
            result.addAll(index.get(w));
        return result;
    }

    // this method should be divided to more methods (2 or 3 at least)
    static void removeExcludeDocs(ArrayList<String> exWords, HashSet<Integer> result)
    {
        int exDocSize = 0;
        for (String ew: exWords)
            if (index.get(ew) != null)
                exDocSize += index.get(ew).size();
        int exWordSize = exWords.size();
        int resSize = result.size();
        
        if (exWordSize == 0 || exDocSize == 0 || resSize == 0)
            return;

        if (resSize * exWordSize < exDocSize)
        {
            for (Iterator<Integer> i = result.iterator(); i.hasNext(); )
            {
                Integer doc = i.next();
                for (String ew: exWords)
                    if (index.get(ew).contains(doc))
                        i.remove();
            }
        }

        else
        {
            for (String ew: exWords)
            {
                if (index.get(ew) != null)
                    result.removeAll(index.get(ew));
            }
        }
    }
}