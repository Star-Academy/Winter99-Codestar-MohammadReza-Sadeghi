import org.junit.jupiter.api.*;
import org.mockito.MockedStatic;
import org.mockito.Mockito;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Scanner;

public class TokenizerTest {

    @Test
    public void tokenizeWithSplitTest()
    {
        String[] ans = {"h>subject", "to" ,"a" ,"high-voltag"};
        assertArrayEquals(ans, Tokenizer.tokenizeWithSplit("h>subject to a high-voltag"));
    }

    @Test
    public void extractAndWordsTest()
    {
        String[] inputWords = {"+h>subject", "to" ,"-a" ,"high-voltag"};
        ArrayList<String> ans = new ArrayList<>();
        ans.add("to");
        ans.add("high-voltag");
        assertEquals(ans, Tokenizer.extractAndWords(inputWords));
    }

    @Test
    void extractOrWordsTest()
    {
        String[] inputWords = {"+h>subject", "to" ,"-a" ,"+high-voltag"};
        ArrayList<String> ans = new ArrayList<>();
        ans.add("h>subject");
        ans.add("high-voltag");
        assertEquals(ans, Tokenizer.extractOrWords(inputWords));
    }

    @Test
    void extractExcludeWordsTest()
    {
        String[] inputWords = {"+h>subject", "-to" ,"-a" ,"high-voltag"};
        ArrayList<String> ans = new ArrayList<>();
        ans.add("to");
        ans.add("a");
        assertEquals(ans, Tokenizer.extractExcludeWords(inputWords));
    }

    @Test
    void tokenizeTest()
    {
        assertEquals("ill post the responses        thank yo        abhin singla ms bioe", Tokenizer.tokenize("ill post the responses        Thank Yo        Abhin Singla MS BioE"));
    }

}
