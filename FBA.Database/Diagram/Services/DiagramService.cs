using System;
using System.Threading.Tasks;
using FBA.Auth.Contract.Services;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Connections.Services;
using FBA.Database.Contract.Diagram.Models;
using FBA.Database.Contract.Diagram.Operations;
using FBA.Database.Contract.Diagram.Services;
using FBA.Repository.Helpers;

namespace FBA.Database.Diagram.Services
{
    public class DiagramService : IDiagramService
    {
        private readonly ITableProviderFactory _tableProviderFactory;
		private readonly IDiagramWriteOperations _diagramWriteOperations;
		private readonly IDiagramQueryOperations _diagramQueryOperations;
		private readonly ICurrentUserService _currentUserService;
		private readonly ISettingsQueryOperations _settingsQueryOperations;

		public DiagramService(
			ITableProviderFactory tableProviderFactory,
			IDiagramWriteOperations diagramWriteOperations,
			IDiagramQueryOperations diagramQueryOperations,
			ISettingsQueryOperations settingsQueryOperations,
			ICurrentUserService currentUserService)
        {
			_currentUserService = currentUserService;
			_settingsQueryOperations = settingsQueryOperations;
			_diagramQueryOperations = diagramQueryOperations;
			_diagramWriteOperations = diagramWriteOperations;
            _tableProviderFactory = tableProviderFactory;
        }

        public async Task<DiagramDocument> CreateFromConnection(string connectionId)
        {
            var connection = await _settingsQueryOperations.GetById(connectionId);
            var provider = _tableProviderFactory.GetProvider(connection.DbType);
            var diagram = await provider.GetTables(connection);

            var document = new DiagramDocument()
            {
                Id = IdGen.NewId(),
                Tables = diagram,
                Name = connection.Name,
                ConnectionId = connectionId,
                UserId = _currentUserService.Get().Id
            };

            document = await _diagramWriteOperations.Create(document);

            return document;
        }

        public async Task<DiagramDocument> Get(string id)
        {
            return await _diagramQueryOperations.GetById(id);
        }

        public async Task<DiagramDocument> GetByConnection(string connectionId)
        {
            return await _diagramQueryOperations.ByConnection(connectionId);
        }
    }
}
