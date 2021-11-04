using FBA.Database.Contract.Connections.Models;

namespace FBA.Database.Contract.Builders
{
    public interface IConnectionStringBuilder
    {
        public string Build(ConnectionInfo connectionInfo);
        public bool Check(string connectionString);
    }
}