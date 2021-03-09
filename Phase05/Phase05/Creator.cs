using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Phase05
{
    class Creator
    {
        public static ArrayList CreateStringList()
        {
            ArrayList al = new ArrayList();
            al.Add("tHeiR woultake issue i anyone overstating");
            al.Add("cOnCLUsion I cOncLusion");
            al.Add(" their conclusion");
            return al;
        }

        public static Dictionary<string, ArrayList> CreateIndex()
        {
            var index = new Dictionary<string, ArrayList>();
            index.Add("their", new ArrayList { 0, 2});
            index.Add("woultake", new ArrayList { 0});
            index.Add("issue", new ArrayList { 0});
            index.Add("i", new ArrayList { 0, 1});
            index.Add("anyone", new ArrayList { 0});
            index.Add("overstating", new ArrayList { 0});
            index.Add("conclusion", new ArrayList { 1, 2});
            return index;
        }
    }
}
