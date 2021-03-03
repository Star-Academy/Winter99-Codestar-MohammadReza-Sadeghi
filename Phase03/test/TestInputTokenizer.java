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

public class TestInputTokenizer {

    @Test
    public void testTokenizeWithSplit()
    {
        String[] ans = {"h>subject", "to" ,"a" ,"high-voltag"};
        assertArrayEquals(ans, InputTokenizer.tokenizeWithSplit("h>subject to a high-voltag"));
    }

    @Test
    public void testExtractAndWords()
    {
        String[] inputWords = {"+h>subject", "to" ,"-a" ,"high-voltag"};
        ArrayList<String> ans = new ArrayList<>();
        ans.add("to");
        ans.add("high-voltag");
        assertEquals(ans, InputTokenizer.extractAndWords(inputWords));
    }

    @Test
    void testExtractOrWords()
    {
        String[] inputWords = {"+h>subject", "to" ,"-a" ,"+high-voltag"};
        ArrayList<String> ans = new ArrayList<>();
        ans.add("h>subject");
        ans.add("high-voltag");
        assertEquals(ans, InputTokenizer.extractOrWords(inputWords));
    }

    @Test
    void testExtractExcludeWords()
    {
        String[] inputWords = {"+h>subject", "-to" ,"-a" ,"high-voltag"};
        ArrayList<String> ans = new ArrayList<>();
        ans.add("to");
        ans.add("a");
        assertEquals(ans, InputTokenizer.extractExcludeWords(inputWords));
    }

    @Test
    void testTokenize()
    {
        assertEquals("ill post the responses        thank yo        abhin singla ms bioe", InputTokenizer.tokenize("ill post the responses        Thank Yo        Abhin Singla MS BioE"));
    }

    @Test
    void testReadInputFromUser()
    {
        String str = "ill post the responses        Thank Yo        Abhin Singla MS BioE";
        InputStream sysInBackup = System.in; // backup System.in to restore it later
        ByteArrayInputStream in = new ByteArrayInputStream(str.getBytes());
        System.setIn(in);

        assertEquals(str, InputTokenizer.readInputFromUser());

        System.setIn(sysInBackup);
    }

    @Test
    void testReadAndTokenize()
    {
        String str = "ill post the responses        Thank Yo        Abhin Singla MS BioE";
        String[] strArray = {"ill", "post", "the", "responses", "thank", "yo", "abhin", "singla", "ms", "bioe"};

        InputStream sysInBackup = System.in; // backup System.in to restore it later
        ByteArrayInputStream in = new ByteArrayInputStream(str.getBytes());
        System.setIn(in);

            assertArrayEquals(strArray, InputTokenizer.readAndTokenize());

        System.setIn(sysInBackup);
    }
}
