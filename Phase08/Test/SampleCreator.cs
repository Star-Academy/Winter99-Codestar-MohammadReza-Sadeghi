using System.Collections.Generic;

namespace Phase05
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

        public static Dictionary<string, HashSet<int>> CreateIndex()
        {
            var index = new Dictionary<string, HashSet<int>>();
            index.Add("their", new HashSet<int> { 0, 2 });
            index.Add("woultake", new HashSet<int> { 0 });
            index.Add("issue", new HashSet<int> { 0 });
            index.Add("i", new HashSet<int> { 0, 1 });
            index.Add("anyone", new HashSet<int> { 0 });
            index.Add("overstating", new HashSet<int> { 0 });
            index.Add("conclusion", new HashSet<int> { 1, 2 });
            return index;
        }

        public static string CreateStr()
        {
            return "tus in  some people8. There is a nati";
        }

        public static string CreateTekenizedStr()
        {
            return "tus in  some people8. there is a nati";
        }

        public static string[] CreateSplitdedDocument()
        {
            return new[] { "tus", "in", "some", "people8", "There", "is", "a", "nati" };
        }

        public static string[] CreateSplittedInput()
        {
            return new[] { "tus", "in", "some", "people8.", "There", "is", "a", "nati" };
        }

        public static string CreateQueryString()
        {
            return "-issue +conclusion +woultake i";
        }

        public static string[] CreateQueryArray()
        {
            return new[] { "-people8", "+is", "+som", "tus" };
        }
    }
}
