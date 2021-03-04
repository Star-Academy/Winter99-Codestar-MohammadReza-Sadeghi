import org.junit.jupiter.api.Test;
import org.mockito.MockedStatic;
import org.mockito.Mockito;
import static org.junit.jupiter.api.Assertions.*;

import java.io.FileNotFoundException;
import java.util.ArrayList;

public class TestFileReader
{
    @Test
    void testRead() throws FileNotFoundException {
        try (MockedStatic<InputTokenizer> mockedStatic = Mockito.mockStatic(InputTokenizer.class)) {
            mockedStatic.when(() ->
                    InputTokenizer.tokenize((Mockito.eq("is what Got me")))).thenReturn("is what got me");
            mockedStatic.when(() ->
                    InputTokenizer.tokenize((Mockito.eq("h>subJecT to a high-Voltag")))).thenReturn("h>subject to a high-voltag");
        }
            ArrayList<String> docs = new ArrayList<>();
            docs.add("is what got me");
            docs.add("h>subject to a high-voltag");

            assertEquals(docs, FileReader.read("SampleData/"));
    }

    @Test
    void testRead2() throws FileNotFoundException {
            assertEquals(new ArrayList<>(), FileReader.read("avc"));
    }

}
