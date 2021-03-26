using System;

namespace Phase05.IO
{
    public class Input
    {
        public static string ReadFromConsole()
        {
            Console.WriteLine("Please enter your query:");
            return Console.ReadLine();
        }
    }
}
