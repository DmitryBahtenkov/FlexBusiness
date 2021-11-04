using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;
using FBA.Repository.Contract.Operations;

namespace FBA.Tests.Mocks.Base
{
    public class QueryOperationsMock<TDocument> : IQueryOperations<TDocument> where TDocument : IDocument
    {
        protected readonly List<TDocument> Storage = Storages.GetStorage<TDocument>();
        
        public Task<IEnumerable<TDocument>> GetAll(bool archive = false)
        {
            return Task.FromResult(GetMany(_ => true));
        }

        public Task<TDocument> GetById(string id)
        {
            return Task.FromResult(GetOne(x=>x.Id == id));
        }

        public Task<IEnumerable<TDocument>> GetByIds(params string[] ids)
        {
            return Task.FromResult(GetMany(x=>ids.Contains(x.Id)));
        }

        public Task<bool> ExistById(string id)
        {
            return Task.FromResult(Storage.Count(x=>x.Id == id) > 0);
        }

        public Task<TDocument> GetArchived(string id)
        {
            return Task.FromResult(GetOne(x=>x.Id == id, true));
        }

        protected TDocument GetOne(Func<TDocument, bool> predicate, bool archive = false)
        {
            return Storage.FirstOrDefault(x=>predicate(x) && x.IsArchived == archive);
        }

        protected IEnumerable<TDocument> GetMany(Func<TDocument, bool> predicate, bool archive = false)
        {
            return Storage.Where(x=>predicate(x) && x.IsArchived == archive);
        }
    }
}