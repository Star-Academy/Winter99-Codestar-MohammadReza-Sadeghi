using Nest;
using Phase05.Models;
using System;
using System.Collections.Generic;
using Phase05.Elastic;
using System.Linq;

namespace Phase05.Elastic
{
    public class ElasticIndex : IElasticIndex
    {
        private readonly IElasticClient client;
        private readonly string indexName;

        public ElasticIndex(string serverUrl, int port, string indexName)
        {
            var uri = new Uri(serverUrl + ":" + port);
            var connectionSettings = new ConnectionSettings(uri);
            connectionSettings.EnableDebugMode();
            client = new ElasticClient(connectionSettings);
            this.indexName = indexName;
        }

        public void CreateIndex()
        {
            var response = client.Indices.Create(indexName, s => s
                .Settings(CreateSettings)
                .Map<Document>(CreateMapping));
        }

        private IPromise<IIndexSettings> CreateSettings(IndexSettingsDescriptor settingsDescriptor)
        {
            return settingsDescriptor
                .Setting("max_ngram_diff", 7)
                .Analysis(CreateAnalysis);
        }

        private ITypeMapping CreateMapping(TypeMappingDescriptor<Document> mappingDescriptor)
        {
            return mappingDescriptor
                .Properties(pr => pr.AddAboutFieldMapping());
        }

        private IAnalysis CreateAnalysis(AnalysisDescriptor analysisDescriptor)
        {
            return analysisDescriptor
                .TokenFilters(CreateTokenFilters)
                .Analyzers(CreateAnalyzers);
        }

        private static IPromise<IAnalyzers> CreateAnalyzers(AnalyzersDescriptor analyzersDescriptor)
        {
            return analyzersDescriptor
                .Custom(Analyzers.NgramAnalyzer, custom => custom
                    .Tokenizer("standard")
                    .Filters("lowercase", TokenFilters.NgramFilter));
        }

        private static IPromise<ITokenFilters> CreateTokenFilters(TokenFiltersDescriptor tokenFiltersDescriptor)
        {
            return tokenFiltersDescriptor
                .NGram(TokenFilters.NgramFilter, ng => ng
                    .MinGram(3)
                    .MaxGram(10));
        }

        public IResponse AddDocuments(List<Document> documents)
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var doc in documents)
            {
                bulkDescriptor.Index<Document>(x => x
                    .Index(indexName)
                    .Document(doc));
            }
            return client.Bulk(bulkDescriptor);
        }

        public IResponse Search(QueryContainer query)
        {
            var response = client.Search<Document>(s => s
                .Index(indexName)
                .Query(q => query));
            response.Validate();
            return response;
        }

    }
}
