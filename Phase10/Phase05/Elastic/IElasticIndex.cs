using Nest;
using Phase05.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phase05.Elastic
{
    interface IElasticIndex
    {
        void CreateIndex();
        IResponse AddDocuments(List<Document> documents);
        IResponse Search(QueryContainer query);
    }
}
