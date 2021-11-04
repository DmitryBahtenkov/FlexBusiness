using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.Connections.Operations
{
    public interface ISettingsWriteOperations : IWriteOperations<ConnectionsDocument>
    {
        public Task<ConnectionsDocument> UpdateConnectionString(string id, string connection);
        public Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info);
    }
}