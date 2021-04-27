using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phase05.Search
{
    public interface IInvertedIndex
    {
        public void AddDocuments(List<string> documents);
        public List<string> SearchQuery(QueryContainer query);
    }
}
