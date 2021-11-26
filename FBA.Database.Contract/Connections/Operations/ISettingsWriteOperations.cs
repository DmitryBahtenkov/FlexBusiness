using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.Connections.Operations
{
    public interface ISettingsWriteOperations : IWriteOperations<ConnectionsDocument>
    {
        public Task<ConnectionsDocument> UpdateConnectionString(string id, string connection, string name);
        public Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info, string name);
        public Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info, string connectionString, string name);
    }
}