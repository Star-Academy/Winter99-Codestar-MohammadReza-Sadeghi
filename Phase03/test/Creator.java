import java.util.HashMap;
import java.util.HashSet;

public class Creator
{
    static String[] createStrArray()
    {
        return new String[]{"get", "help", "+illness", "+disease", "-cough"};
    }

    static HashSet<Integer> createHashSet(int addNumber)
    {
        HashSet<Integer> hashSet = new HashSet<>();
        hashSet.add(addNumber);
        return hashSet;
    }

    static HashSet<Integer> getHashSet1()
    {
        HashSet<Integer> a = new HashSet<>();

        a.add(2);
        a.add(0);
        a.add(-8);
        a.add(-5);
        a.add(-4);

        return a;
    }

    static HashSet<Integer> getHashSet2()
    {
        HashSet<Integer> b = new HashSet<>();

        b.add(-5);
        b.add(2);
        b.add(-10);
        b.add(-9);

        return b;
    }

    static HashSet<Integer> getHashSet3()
    {
        HashSet<Integer> c = new HashSet<>();

        c.add(-4);
        c.add(-6);
        c.add(-1);
        c.add(1);
        c.add(-5);

        return c;
    }

    static HashMap<String, HashSet<Integer>> getIndex1()
    {
        HashMap<String, HashSet<Integer>> index = new HashMap<>();
        HashSet<Integer> a = getHashSet1();
        HashSet<Integer> b = getHashSet2();

        index.put("subject", a);
        index.put("high", b);

        return index;
    }
}
