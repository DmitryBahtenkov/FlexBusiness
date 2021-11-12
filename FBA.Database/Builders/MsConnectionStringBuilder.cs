using System.Linq;
using System.Text;
using FBA.Database.Contract.Builders;
using FBA.Database.Contract.Connections.Models;

namespace FBA.Database.Builders
{
    public class MsConnectionStringBuilder : IConnectionStringBuilder
    {
        public string Build(ConnectionInfo connectionInfo)
        {
            var builder = new StringBuilder();

            var server = $"Server={connectionInfo.Host}";
            if (!string.IsNullOrEmpty(connectionInfo.Port))
            {
                server += $", {connectionInfo.Port}";
            }

            var db = $"Database={connectionInfo.Database}";
            
            var user = $"User Id={connectionInfo.Login}";

            var password = $"Password={connectionInfo.Password}";

            builder.AppendJoin(";", server, db, user, password);

            foreach (var (key, value) in connectionInfo.Parameters)
            {
                builder.Append($";{key}:{value}");
            }

            return builder.ToString();
        }

        public bool Check(string connectionString)
        {
            throw new System.NotImplementedException();
        }
    }
}