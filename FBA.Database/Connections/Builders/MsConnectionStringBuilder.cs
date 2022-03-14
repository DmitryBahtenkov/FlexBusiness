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

        public ConnectionInfo Deconstruct(string connectionString)
        {
            var splitted = connectionString.Split(";");
            var pairs = splitted.Select(x => 
                {
                    var pair = x.Trim().Split("=");
                    return (pair[0], pair[1]);
                })
                .ToDictionary(x => x.Item1, x => x.Item2);

            var connectionInfo = new ConnectionInfo();

            if(pairs.ContainsKey(Database))
            {
                connectionInfo.Database = pairs[Database];
            }

            if(pairs.ContainsKey(Server))
            {
                connectionInfo.Host = pairs[Server];
            }
            if(pairs.ContainsKey(UserId))
            {
                connectionInfo.Login = pairs[UserId];
            }

            if(pairs.ContainsKey(Password))
            {
                connectionInfo.Password = pairs[Password];
            }

            connectionInfo.Parameters = pairs.Where(x => !MainParameters.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);

            return connectionInfo;
        }

        private const string Database = nameof(Database);
        private const string Server = nameof(Server);
        private const string Password = nameof(Password);
        private const string UserId = "User Id";

        private readonly string[] MainParameters = new [] { Database, Server, Password, UserId };
    }
}