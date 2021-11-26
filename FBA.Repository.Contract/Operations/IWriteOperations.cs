using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;

namespace FBA.Repository.Contract.Operations
{
    public interface IWriteOperations<TDocument> where TDocument : IDocument
    {
        public Task<TDocument?> Create(TDocument document);
        public Task<TDocument?> Update(TDocument document);
        public Task<TDocument?> Delete(string id, bool safe = true);
    }
}