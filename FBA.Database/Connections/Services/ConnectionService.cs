using System.Linq;
using System.Threading.Tasks;
using FBA.Auth.Contract.Services;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Database.Contract.Builders;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Models.Requests;
using FBA.Database.Contract.Connections.Models.Responses;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Connections.Services;
using FBA.Database.Contract.Diagram.Services;
using FBA.Repository.Helpers;

namespace FBA.Database.Services
{
    public class ConnectionService : IConnectionsService
    {
        private readonly IConnectionStingBuilderFactory _connectionStingBuilderFactory;
        private readonly ISettingsQueryOperations _settingsQueryOperations;
        private readonly ISettingsWriteOperations _settingsWriteOperations;
		private readonly IDiagramService _diagramService;
        private readonly ICurrentUserService _currentUserService; 

        public ConnectionService(IConnectionStingBuilderFactory connectionStingBuilderFactory, 
            ISettingsQueryOperations settingsQueryOperations, 
            ISettingsWriteOperations settingsWriteOperations, 
            ICurrentUserService currentUserService,
			IDiagramService diagramService)
        {
			_diagramService = diagramService;
            _connectionStingBuilderFactory = connectionStingBuilderFactory;
            _settingsQueryOperations = settingsQueryOperations;
            _settingsWriteOperations = settingsWriteOperations;
            _currentUserService = currentUserService;
        }

        public async Task<ConnectionResponse> Create(CreateConnectionRequest request)
        {
            var newDocument = new ConnectionsDocument()
            {
                Id = IdGen.NewId(),
                ConnectionInfo = new()
                {
                    Database = request.Database,
                    Host = request.Host,
                    Password = request.Password,
                    Login = request.Login,
                    Parameters = request.Parameters,
                    Port = request.Port
                },
                DbType = request.DbType,
                ConnectionString = request.ConnectionString,
                Name = request.Name,
                UserId = _currentUserService.Get().Id
            };

            var builder = _connectionStingBuilderFactory.GetBuilder(request.DbType);

            if (string.IsNullOrEmpty(request.ConnectionString))
            {
                newDocument.ConnectionString = builder.Build(newDocument.ConnectionInfo);
            }
            else
            {
                var info = builder.Deconstruct(request.ConnectionString);
                newDocument.ConnectionInfo = info;
            }
            
            newDocument = await _settingsWriteOperations.Create(newDocument);

            await _diagramService.CreateFromConnection(newDocument.Id);
            
            return Map(newDocument);
        }

        private ConnectionResponse Map(ConnectionsDocument document)
        {
            return new ConnectionResponse()
            {
                Id = document.Id,
                ConnectionString = document.ConnectionString,
                Host = document.ConnectionInfo.Host,
                Login = document.ConnectionInfo.Login,
                Name = document.Name,
                Port = document.ConnectionInfo.Port,
                Database = document.ConnectionInfo.Database,
                Type = document.DbType
            };
        }

        public async Task<ConnectionResponse> Update(string id, UpdateConnectionRequest request)
        {
            var document = await _settingsQueryOperations.GetById(id);
            if (document is null)
            {
                throw new NotFoundException();
            }

            if (string.IsNullOrEmpty(request.ConnectionString))
            {
                var connectionInfo = new ConnectionInfo
                {
                    Database = request.Database,
                    Host = request.Host,
                    Login = request.Login,
                    Parameters = request.Parameters,
                    Password = request.Password,
                    Port = request.Port
                };

                var builder = _connectionStingBuilderFactory.GetBuilder(document.DbType);

                document = await _settingsWriteOperations.UpdateConnectionInfo(document.Id, connectionInfo,
                    builder.Build(connectionInfo), request.Name);
            }
            else
            {
                document = await _settingsWriteOperations.UpdateConnectionString(document.Id, request.ConnectionString, request.Name);
            }

            return Map(document);
        }

        public async Task<ConnectionResponse> Delete(string id)
        {
            var document = await _settingsWriteOperations.Delete(id);

            if (document is null)
            {
                throw new NotFoundException();
            }

            return Map(document);
        }

        public async Task<ConnectionResponse> Get(string id)
        {
            var document = await _settingsQueryOperations.GetById(id);

            if (document is null)
            {
                throw new NotFoundException();
            }

            return Map(document);
        }

        public async Task<GetConnectionsResponse> Get()
        {
            var docs = await _settingsQueryOperations.GetAll();

            return new GetConnectionsResponse
            {
                Connections = docs.Select(Map).ToList()
            };
        }
    }
}