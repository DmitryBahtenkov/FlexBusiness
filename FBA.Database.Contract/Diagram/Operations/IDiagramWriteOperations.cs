using System.Threading.Tasks;
using FBA.Database.Contract.Diagram.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.Diagram.Operations
{
    public interface IDiagramWriteOperations : IWriteOperations<DiagramDocument>
    { }
}