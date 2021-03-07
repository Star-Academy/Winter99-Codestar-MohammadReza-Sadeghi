import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.io.InputStream;

import static org.junit.jupiter.api.Assertions.assertArrayEquals;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class InputTest
{
    @Test
    void readInputFromUserTest()
    {
        String str = "ill post the responses        Thank Yo        Abhin Singla MS BioE";
        InputStream sysInBackup = System.in; // backup System.in to restore it later
        ByteArrayInputStream in = new ByteArrayInputStream(str.getBytes());
        System.setIn(in);

        assertEquals(str, Input.readInputFromUser());

        System.setIn(sysInBackup);
    }

    @Test
    void readAndTokenizeTest()
    {
        String str = "ill post the responses        Thank Yo        Abhin Singla MS BioE";
        String[] strArray = {"ill", "post", "the", "responses", "thank", "yo", "abhin", "singla", "ms", "bioe"};

        InputStream sysInBackup = System.in; // backup System.in to restore it later
        ByteArrayInputStream in = new ByteArrayInputStream(str.getBytes());
        System.setIn(in);

        assertArrayEquals(strArray, Input.readAndTokenize());

        System.setIn(sysInBackup);
    }
}
