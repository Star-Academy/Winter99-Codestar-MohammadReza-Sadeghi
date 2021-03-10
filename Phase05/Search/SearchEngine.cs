using Phase05;
using System;
using System.Collections.Generic;
using System.Text;

namespace Search
{
    public class SearchEngine
    {
        private InvertedIndex Index { get; set; }

        public SearchEngine(InvertedIndex Index)
        {
            this.Index = Index;
        }
        public HashSet<int> SearchQuery(string Query)
        {
            
        }
    }
}
