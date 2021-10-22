using FBA.Repository.Contract.Documents;
using MongoDB.Driver;

namespace FBA.Repository.Operations
{
    public abstract class BaseOperations<TDocument> where TDocument : IDocument
    {
        protected FilterDefinitionBuilder<TDocument> F => Builders<TDocument>.Filter;
        protected UpdateDefinitionBuilder<TDocument> U => Builders<TDocument>.Update;

        private readonly DbContext _dbContext;
        protected readonly IMongoCollection<TDocument> Collection;

        protected BaseOperations(DbContext dbContext)
        {
            _dbContext = dbContext;

            Collection = _dbContext.Database.GetCollection<TDocument>(typeof(TDocument).Name.Replace("Document", ""));
        }
    }
}