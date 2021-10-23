using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Repository.Contract.Documents;
using FBA.Repository.Contract.Operations;

namespace FBA.Tests.Mocks.Base
{
    public class WriteOperationsMock<TDocument> : IWriteOperations<TDocument> where TDocument : IDocument
    {
        protected readonly List<TDocument> Storage = Storages.GetStorage<TDocument>();
        
        public Task<TDocument> Create(TDocument document)
        {
            Storage.Add(document);
            
            return Task.FromResult(document);
        }

        public Task<TDocument> Update(TDocument document)
        {
            Storage.RemoveAll(x=>x.Id == document.Id);
            Storage.Add(document);
            
            return Task.FromResult(document);
        }

        public Task<TDocument> Delete(string id, bool safe = true)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);
            
            if (safe)
            {
                if (document is not null)
                {
                    document.IsArchived = true;
                }
            }
            else
            {
                Storage.RemoveAll(x => x.Id == id);
            }
            
            return Task.FromResult(document);
        }
    }
}