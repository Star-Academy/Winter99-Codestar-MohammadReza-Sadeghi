using System.Collections.Generic;

namespace Test
{
    class SampleCreator
    {
        public static List<string> CreateStringList()
        {
            var al = new List<string>();
            al.Add("tHeiR woultake issue i anyone overstating");
            al.Add("cOnCLUsion I cOncLusion");
            al.Add("their conclusion");
            return al;
        }

        public static string CreateStr()
        {
            return "tus in  some people8. There is a nati";
        }

        public static string CreateTekenizedStr()
        {
            return "tus in  some people8. there is a nati";
        }

        public static string[] CreateSplittedInput()
        {
            return new[] { "tus", "in", "some", "people8.", "There", "is", "a", "nati" };
        }

        public static string CreateQueryString()
        {
            return "-issue +conclusion +woultake i";
        }
    }
}
