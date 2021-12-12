using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Operations;
using FBA.Repository;
using FBA.Repository.Extensions;
using FBA.Repository.Operations;
using MongoDB.Driver;

namespace FBA.Database.Operations
{
    public class SettingsWriteOperations : WriteOperations<ConnectionsDocument>, ISettingsWriteOperations
    {
        public SettingsWriteOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ConnectionsDocument> UpdateConnectionString(string id, string connection, string name)
        {
            return await UpdateOne(F.ById(id), U
                .Set(x => x.ConnectionString, connection)
                .Set(x=>x.Name, name));
        }

        public async Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info, string name)
        {
            return await UpdateOne(F.ById(id), U
                .Set(x => x.ConnectionInfo, info)
                .Set(x=>x.Name, name));
        }

        public async Task<ConnectionsDocument> UpdateConnectionInfo(string id, ConnectionInfo info, string connectionString, string name)
        {
            return await UpdateOne(F.ById(id), U
                .Set(x => x.ConnectionInfo, info)
                .Set(x => x.ConnectionString, connectionString)
                .Set(x => x.Name, name));

        }
    }
}