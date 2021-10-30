using FBA.Repository.Contract.Documents;
using MongoDB.Driver;

namespace FBA.Repository.Extensions
{
    public static class MongoDefinitionExtensions
    {
        public static FilterDefinition<TDocument> ById<TDocument>(
            this FilterDefinitionBuilder<TDocument> builder,
            string id) where TDocument : IDocument
        {
            return builder.Eq(x => x.Id, id);
        }
    }
}