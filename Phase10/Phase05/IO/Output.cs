using System;
using System.Collections.Generic;

namespace Phase05.IO
{
    public class Output
    {
        public static void PrintSet(HashSet<int> result)
        {
            foreach (int s in result)
            {
                Console.WriteLine(s);
            }
        }

        public static void PrintStringList(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("-------Document" + i + "\n:" + list[i]);
            }
        }

        public static void PrintString(string str)
        {
            Console.WriteLine(str);
        }
    }
}
