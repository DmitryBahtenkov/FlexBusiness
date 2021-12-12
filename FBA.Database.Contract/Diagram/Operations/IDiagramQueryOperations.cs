using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.Diagram.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.Diagram.Operations
{
    public interface IDiagramQueryOperations : IQueryOperations<DiagramDocument>
    {
        public Task<DiagramDocument> ByConnection(string connectionId);
        public Task<List<DiagramDocument>> ByUser(string userId);
    }
}