using Nest;
using Phase05.Models;

namespace Phase05.Elastic
{
    internal static class FieldsDefiner
    {
        public static PropertiesDescriptor<Document> AddAboutFieldMapping(this PropertiesDescriptor<Document> prpertiesDescriptor)
        {
            return prpertiesDescriptor
                .Text(t => t
                    .Name(n => n.Content)
                    .Fields(f => f
                        .Text(ng => ng
                            .Name("ngram")
                            .Analyzer(Analyzers.NgramAnalyzer))));
        }
    }
}
