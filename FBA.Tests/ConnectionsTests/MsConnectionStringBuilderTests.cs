using System.Collections.Generic;
using FBA.Database.Builders;
using FBA.Database.Contract.Connections.Models;
using Xunit;

namespace FBA.Tests.ConnectionsTests
{
    public class MsConnectionStringBuilderTests
    {
        private readonly MsConnectionStringBuilder _msConnectionStringBuilder;

        public MsConnectionStringBuilderTests()
        {
            _msConnectionStringBuilder = new MsConnectionStringBuilder();
        }

        [Fact(DisplayName = "Проверка полноценного создания строки подключения MS SQL")]
        public void CreateCorrectConnectionStringTest()
        {
            var info = new ConnectionInfo
            {
                Host = "127.0.0.1",
                Port = "1433",
                Database = "Penises",
                Login = "user6",
                Password = "qwerty124"
            };

            var connectionString = _msConnectionStringBuilder.Build(info);

            AssertDefaultConnectionString(connectionString, info);
        }
        
        [Fact(DisplayName = "Проверка полноценного создания строки подключения MS SQL с параметром")]
        public void CreateCorrectConnectionStringWithParameterTest()
        {
            var info = new ConnectionInfo
            {
                Host = "127.0.0.1",
                Port = "1433",
                Database = "Penises",
                Login = "user6",
                Password = "qwerty124"
            };
            info.Parameters["Trusted_Connection"] = "True";

            var connectionString = _msConnectionStringBuilder.Build(info);
            var elements = AssertDefaultConnectionString(connectionString, info);
            
            var param = elements[4];
            Assert.Contains("Trusted_Connection", param);
            Assert.Contains("True", param);
        }

        public string[] AssertDefaultConnectionString(string connectionString, ConnectionInfo info)
        {
            Assert.NotEmpty(connectionString);
            var elements = connectionString.Split(";");
            Assert.Equal(5, elements.Length);
            
            var server = elements[0];
            Assert.Contains(info.Host, server);
            Assert.Contains(info.Port, server);
            
            var db = elements[1];
            Assert.Contains(info.Database, db);

            var user = elements[2];
            Assert.Contains(info.Login, user);
            
            var pass = elements[3];
            Assert.Contains(info.Password, pass);

            return elements;
        }
    }
}