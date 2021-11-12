using FBA.Database.Contract.Builders;
using FBA.Database.Contract.Connections.Models;

namespace FBA.Database.Builders
{
    public class MsConnectionStringBuilder : IConnectionStringBuilder
    {
        public string Build(ConnectionInfo connectionInfo)
        {
            throw new System.NotImplementedException();
        }

        public bool Check(string connectionString)
        {
            throw new System.NotImplementedException();
        }
    }
}