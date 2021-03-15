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

        public static void PrintString(string str)
        {
            Console.WriteLine(str);
        }
    }
}
