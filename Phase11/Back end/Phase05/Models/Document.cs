using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Phase05.Models
{
    public class Document
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        public Document(string content)
        {
            this.Content = content;
        }

        public static List<Document> GetDocumentList(List<string> strDocs)
        {
            return strDocs.Select(s => new Document(s)).ToList();
        }
    }
}
