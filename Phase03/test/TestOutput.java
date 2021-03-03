import org.junit.jupiter.api.Test;
import org.mockito.MockedStatic;
import org.mockito.Mockito;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.util.HashSet;

import static com.sun.javaws.JnlpxArgs.verify;
import static org.junit.jupiter.api.Assertions.*;

public class TestOutput
{
    @Test
    void testPrintSet()
    {
        HashSet<Integer> hashSet = new HashSet<>();
        hashSet.add(2);

        String answer = "2";
        ByteArrayOutputStream sink = new ByteArrayOutputStream();
        System.setOut(new PrintStream(sink, true));
        Output.printSet(hashSet);
        assertEquals(answer, (new String(sink.toByteArray())).replaceAll("\n", "").replaceAll("\r", ""));
    }
}
