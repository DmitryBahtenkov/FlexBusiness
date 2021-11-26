using System.Reflection;
using System.Threading.Tasks;
using FBA.Database.Contract;
using FBA.Database.Contract.Connections.Models.Requests;
using FBA.Database.Contract.Connections.Services;
using FBA.Tests.Data;
using Xunit;

namespace FBA.Tests.ConnectionsTests
{
    public class ConnectionServiceTests
    {
        private readonly IConnectionsService _connectionsService;

        public ConnectionServiceTests(IConnectionsService connectionsService)
        {
            _connectionsService = connectionsService;
        }

        [Fact(DisplayName = "Проверка корректного создания подключения без строки подключения")]
        public async Task CreateCorrectConnection()
        {
            var request = new CreateConnectionRequest()
            {
                DbType = DbType.MsSql,
                Database = "db",
                Host = "localhost",
                Name = "My super connection",
                Login = "User1",
                Password = "1234"
            };

            var response = await _connectionsService.Create(request);
            
            Assert.NotNull(response);
            Assert.Equal(request.Name, response.Name);
            Assert.NotEmpty(response.ConnectionString);
        }
        
        [Fact(DisplayName = "Проверка корректного обновления подключения")]
        public async Task UpdateCorrectConnection()
        {
            var request = new UpdateConnectionRequest()
            {
                Database = "newdb",
                Host = "localhost",
                Name = "My super connection",
                Login = "User2",
                Password = "12345"
            };

            var response = await _connectionsService.Update(TestConnections.ConnectionForUpdate.Id, request);
            
            Assert.NotNull(response);
            Assert.Equal(request.Name, response.Name);
            Assert.NotEmpty(response.ConnectionString);
            Assert.Equal(request.Database, response.Database);
        }
    }
}