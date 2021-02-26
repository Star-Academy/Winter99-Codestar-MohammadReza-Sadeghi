import java.util.*;

public class Tokenizer
{
    public static String[] readAndTokenize()
    {
        String inputStr = readInputFromUser();
        return tokenize(inputStr);
    }

    static String readInputFromUser()
    {
        Scanner scanner = new Scanner(System.in);
        String inputStr = scanner.nextLine();
        scanner.close();
        return inputStr;
    }

    static String[] tokenize(String inputStr)
    {
        return inputStr.toLowerCase().split(" ");
    }

    static ArrayList<String> extractAndWords(String[] inputWords)
    {
        ArrayList<String> andWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) != '+' && w.charAt(0) != '-')
                andWords.add(w);
        return andWords;
    }

    static ArrayList<String> extractOrWords(String[] inputWords)
    {
        ArrayList<String> orWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) == '+')
                orWords.add(w.substring(1));
        return orWords;
    }

    static ArrayList<String> extractExcludeWords(String[] inputWords)
    {
        ArrayList<String> exWords = new ArrayList<>();
        for (String w: inputWords)
            if (w.charAt(0) == '-')
                exWords.add(w.substring(1));
        return exWords;
    }
}