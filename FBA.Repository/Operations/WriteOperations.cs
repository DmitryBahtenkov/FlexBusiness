using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;
using FBA.Repository.Contract.Operations;
using MongoDB.Driver;

namespace FBA.Repository.Operations
{
    public class WriteOperations<TDocument>: BaseOperations<TDocument>, IWriteOperations<TDocument> where TDocument : IDocument
    {
        public WriteOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TDocument> Create(TDocument document)
        {
            await Collection.InsertOneAsync(document);
            return document;
        }

        public async Task<TDocument> Update(TDocument document)
        {
            var replaceResult  = await Collection.ReplaceOneAsync(F.Eq(x => x.Id, document.Id), document);
            return replaceResult.IsAcknowledged ? document : default;
        }

        public async Task<TDocument> Delete(string id, bool safe = true)
        {
            if (safe)
            {
                return await Collection.FindOneAndUpdateAsync<TDocument>(F.Eq(x => x.Id, id),
                    U.Set(x => x.IsArchived, true));
            }
            
            return await Collection.FindOneAndDeleteAsync<TDocument>(F.Eq(x => x.Id, id));
        }

        protected async Task<TDocument> UpdateOne(FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update)
        {
            return await Collection.FindOneAndUpdateAsync<TDocument>(filter, update);
        }
        
        protected async Task UpdateMany(FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update)
        {
            await Collection.UpdateManyAsync(filter, update);
        }
    }
}