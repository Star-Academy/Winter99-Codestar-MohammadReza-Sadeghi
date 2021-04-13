using System.Collections.Generic;
using System.Linq;
using Nest;
using Phase05.Elastic;
using Phase05.Models;

namespace Phase05.Search
{
    public class InvertedIndex
    {
        private ElasticIndex index;

        public InvertedIndex(ElasticIndex index, string indexName)
        {
            this.index = index;
            this.index.CreateIndex(indexName);
        }

        public InvertedIndex() { }

        public void AddDocuments(List<string> documents)
        {
            var docs = Document.GetDocumentList(documents);
            index.AddDocuments(docs);
        }

        public List<string> SearchQuery(QueryContainer query)
        {
            var response = (ISearchResponse<Document>) index.Search(query);
            return response.Documents.Select(s => s.Content).ToList();
        }

    }
}
