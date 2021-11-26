using System.Threading.Tasks;
using FBA.Database.Contract;
using FBA.Database.Contract.Connections.Models.Requests;
using FBA.Database.Contract.Connections.Services;
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
    }
}