using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;

namespace FBA.Repository.Contract.Operations
{
    public interface IQueryOperations<TDocument> where TDocument : IDocument
    {
        public Task<IEnumerable<TDocument>> GetAll(bool archive = false);
        public Task<TDocument> GetById(string id);
        public Task<IEnumerable<TDocument>> GetByIds(params string[] ids);
        public Task<bool> ExistById(string id);

        public Task<TDocument> GetArchived(string id);
    }
}