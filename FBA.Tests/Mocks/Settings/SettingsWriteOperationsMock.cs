using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.Settings
{
    public class SettingsWriteOperationsMock : WriteOperationsMock<ConnectionsDocument>, ISettingsWriteOperations
    {
        public Task<ConnectionsDocument> UpdateConnectionString(string id, string connection, string name)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.ConnectionString = connection;
                document.Name = name;
            }

            return Task.FromResult(document);
        }

        public Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info, string name)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.ConnectionInfo = info;
                document.Name = name;
            }

            return Task.FromResult(document);
        }

        public Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info, string connectionString, string name)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.ConnectionInfo = info;
                document.ConnectionString = connectionString;
                document.Name = name;
            }

            return Task.FromResult(document);
        }
    }
}