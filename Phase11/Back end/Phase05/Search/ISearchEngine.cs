using System;
using System.Collections.Generic;
using System.Text;

namespace Phase05.Search
{
    public interface ISearchEngine
    {
        public List<string> SearchQuery(string query);
    }
}
