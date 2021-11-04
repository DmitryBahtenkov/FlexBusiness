using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;
using FBA.Repository.Contract.Operations;
using FBA.Repository.Extensions;
using MongoDB.Driver;

namespace FBA.Repository.Operations
{
    public abstract class QueryOperations<TDocument> : BaseOperations<TDocument>, IQueryOperations<TDocument> where TDocument : IDocument
    {
        public async Task<IEnumerable<TDocument>> GetAll(bool archive = false)
        {
            return await GetMany(FilterDefinition<TDocument>.Empty, archive);
        }

        public async Task<TDocument> GetById(string id)
        {
            return await GetOne(F.Eq(x => x.Id, id));
        }

        public async Task<IEnumerable<TDocument>> GetByIds(params string[] ids)
        {
            return await GetMany(F.In(x => x.Id, ids));
        }

        public async Task<bool> ExistById(string id)
        {
            return await Collection.CountDocumentsAsync(F.Eq(x => x.Id, id)) > 0;
        }

        public async Task<TDocument> GetArchived(string id)
        {
            return await GetOne(F.ById(id), true);
        }

        /// <summary>
        /// Поиск одного объекта из коллекции
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="archive"></param>
        /// <returns></returns>
        protected async Task<TDocument> GetOne(FilterDefinition<TDocument> filter, bool archive = false)
        {
            filter &= F.Eq(x => x.IsArchived, archive);
            
            return await (await Collection.FindAsync(filter)).FirstOrDefaultAsync();
        }
        
        /// <summary>
        /// Поиск нескольких объектов из коллекции
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="archive"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<TDocument>> GetMany(FilterDefinition<TDocument> filter, bool archive = false)
        {
            filter &= F.Eq(x => x.IsArchived, archive);
            
            return (await Collection.FindAsync(filter)).ToEnumerable();
        }

        public QueryOperations(DbContext dbContext) : base(dbContext)
        {
        }
    }
}