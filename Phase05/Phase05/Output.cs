using System;
using System.Collections.Generic;
using System.Text;

namespace Phase05
{
    public class Output
    {
        public static void PrintSet(HashSet<int> Result)
        {
            foreach (int s in Result)
            {
                Console.WriteLine(s);
            }
        }
    }
}
