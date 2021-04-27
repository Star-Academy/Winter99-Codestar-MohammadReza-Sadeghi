using System;

namespace Phase05.IO
{
    public class Input
    {
        public static string ReadFromConsole(string inputStr)
        {
            Console.WriteLine(inputStr);
            return Console.ReadLine();
        }
    }
}
