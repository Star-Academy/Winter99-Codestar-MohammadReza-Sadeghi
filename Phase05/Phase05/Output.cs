using System;
using System.Collections.Generic;
using System.Text;

namespace Phase05
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
