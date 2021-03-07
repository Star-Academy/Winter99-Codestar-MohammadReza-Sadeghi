import java.util.*;

public class Input
{
    public static String[] readAndTokenize()
    {
        String inputStr = readInputFromUser();
        return Tokenizer.tokenizeWithSplit(inputStr);
    }

    static String readInputFromUser()
    {
        final Scanner scanner = new Scanner(System.in);
        final String inputStr = scanner.nextLine();
        scanner.close();
        return inputStr;
    }

}