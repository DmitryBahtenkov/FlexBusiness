using System.Reflection;
using System.Threading.Tasks;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Database.Contract;
using FBA.Database.Contract.Connections.Models.Requests;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Connections.Services;
using FBA.Tests.Data;
using Xunit;

namespace FBA.Tests.ConnectionsTests
{
    public class ConnectionServiceTests
    {
        private readonly IConnectionsService _connectionsService;
        private readonly ISettingsQueryOperations _settingsQueryOperations;

        public ConnectionServiceTests(IConnectionsService connectionsService, ISettingsQueryOperations settingsQueryOperations)
        {
            _connectionsService = connectionsService;
            _settingsQueryOperations = settingsQueryOperations;
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

        [Fact(DisplayName = "Проверка удаления существующего документа настроек")]
        public async Task DeleteExistingConnection()
        {
            var id = TestConnections.ConnectionForDelete.Id;
            await _connectionsService.Delete(id);

            var connection = await _settingsQueryOperations.GetById(id);
            Assert.Null(connection);
        }
        
        
        [Fact(DisplayName = "Проверка удаления несуществующего документа настроек")]
        public async Task DeleteNonExistingConnection()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _connectionsService.Delete("id"));
        }
    }
}