using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phase05.DataSet
{
    public class Word
    {
        [Key]
        public string Value { get; set; }
        public ICollection<WordDoc> WordDocs { get; set; }

        public Word(string value)
        {
            this.Value = value;
        }
    }
}
