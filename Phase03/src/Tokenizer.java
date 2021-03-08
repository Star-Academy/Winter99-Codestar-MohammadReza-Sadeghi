import jdk.nashorn.internal.runtime.regexp.joni.Regex;

import java.util.ArrayList;

public class Tokenizer
{
    final static String whiteSpaceSplit = "\\s+";

    static String[] tokenizeWithSplit(String inputStr)
    {
        return inputStr.toLowerCase().split(whiteSpaceSplit);
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

    static String tokenize(String inputStr)
    {
        return inputStr.toLowerCase();
    }
}
