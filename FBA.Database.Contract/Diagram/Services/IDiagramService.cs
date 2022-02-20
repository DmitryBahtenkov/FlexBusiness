using System.Threading.Tasks;
using FBA.Database.Contract.Diagram.Models;

namespace FBA.Database.Contract.Diagram.Services
{
    public interface IDiagramService
    {
        public Task<DiagramDocument> Get(string id);
        public Task<DiagramDocument> GetByConnection(string connectionId);
        public Task<DiagramDocument> CreateFromConnection(string connectionId);
    }
}