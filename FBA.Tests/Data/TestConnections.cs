using System.Collections.Generic;
using System.Linq;
using FBA.Database.Contract;
using FBA.Database.Contract.Connections.Models;

namespace FBA.Tests.Data
{
    public static class TestConnections
    {
        public static ConnectionsDocument ConnectionForUpdate => new()
        {
            Id = "connforupdate",
            DbType = DbType.MsSql,
            Name = "name",
            ConnectionInfo = new()
            {
                Database = "a",
                Host = "a"
            }
        };
        
        public static IEnumerable<ConnectionsDocument> GetAllDocuments()
        {
            return typeof(TestConnections)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(ConnectionsDocument))
                .Select(x => x.GetValue(null, null) as ConnectionsDocument);
        }
    }
}