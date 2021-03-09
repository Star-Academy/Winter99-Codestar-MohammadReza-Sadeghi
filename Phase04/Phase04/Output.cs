using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    class Output
    {
        public static void PrintList(IEnumerable<Object> printList)
        {
            foreach (var obj in printList)
                Console.WriteLine(obj.ToString());
        }
    }
}
