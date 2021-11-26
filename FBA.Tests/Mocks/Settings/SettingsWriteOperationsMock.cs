using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.Settings
{
    public class SettingsWriteOperationsMock : WriteOperationsMock<ConnectionsDocument>, ISettingsWriteOperations
    {
        public Task<ConnectionsDocument> UpdateConnectionString(string id, string connection)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.ConnectionString = connection;
            }

            return Task.FromResult(document);
        }

        public Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.ConnectionInfo = info;
            }

            return Task.FromResult(document);
        }
    }
}