using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phase05.DataSet
{
    public class Document
    {
        [Key]
        public int DocId { get; set; }
        public string Content { get; set; }
        public ICollection<WordDoc> WordDocs { get; set; }

        public Document(string content)
        {
            this.Content = content;
            //this.WordDocs = new HashSet<WordDoc>();
        }
    }
}
