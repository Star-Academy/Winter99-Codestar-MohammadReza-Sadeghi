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
}