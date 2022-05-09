using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Models.Requests;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Database.Contract.StoredProcedures.Services;
using FBA.Repository.Helpers;

namespace FBA.Database.StoredProcedures.Services
{
    public class StoredProcedureService : IStoredProcedureService
    {
		private readonly IStoredProcedureQueryOperations _storedProcedureQueryOperations;
		private readonly IProcedureInfoProviderFactory _procedureInfoProviderFactory;
		private readonly IStoredProcedureWriteOperations _storedProcedureWriteOperations;
		private readonly ISettingsQueryOperations _settingsQueryOperations;

		public StoredProcedureService(
			IStoredProcedureQueryOperations storedProcedureQueryOperations,
			IStoredProcedureWriteOperations storedProcedureWriteOperations,
			IProcedureInfoProviderFactory procedureInfoProviderFactory,
			ISettingsQueryOperations settingsQueryOperations)
		{
			_settingsQueryOperations = settingsQueryOperations;
			_procedureInfoProviderFactory = procedureInfoProviderFactory;
			_storedProcedureWriteOperations = storedProcedureWriteOperations;
			_storedProcedureQueryOperations = storedProcedureQueryOperations;
		}

        public async Task<StoredProcedureDocument> Create(string connectionId, ProcedureRequest request)
        {
            var connection = await GetConnection(connectionId);

            var document = new StoredProcedureDocument
            {
                Id = IdGen.NewId(),
                Name = request.Name,
                Title = request.Title,
                ConnectionId = connectionId,
                Direction = request.Direction
            };

            var provider = _procedureInfoProviderFactory.GetProvider(connection.DbType);

            document.Parameters = await provider.GetParameters(connection, request.Name);

            return await _storedProcedureWriteOperations.Create(document);
        }

        public async Task<ExecuteResult> Execute(string id, Dictionary<string, string> parameters)
        {
            var procedure = await Get(id);
            if(procedure is null)
            {
                throw new NotFoundException();
            }

            var errors = new StringBuilder();
            foreach(var param in procedure.Parameters)
            {
                if (!parameters.ContainsKey(param.Name))
                {
                    errors.Append($"Отсутствует параметр {param.Name} - {param.Title}; ");
                }
            }

            var errorText = errors.ToString();
            if (!string.IsNullOrEmpty(errorText))
            {
                throw new BusinessException(errorText);
            }

            var connection = await GetConnection(procedure.ConnectionId);

            var provider = _procedureInfoProviderFactory.GetProvider(connection.DbType);
            return await provider.ExecuteStoredProcedure(connection, procedure.Name, parameters);
        }

        public async Task<StoredProcedureDocument> Get(string id)
        {
            return await _storedProcedureQueryOperations.GetById(id);
        }

        public async Task<List<StoredProcedureDocument>> GetAll()
        {
            return (await _storedProcedureQueryOperations.GetAll()).ToList();
        }

        public async Task<List<StoredProcedureDocument>> GetByConnection(string connectionId)
        {
            return await _storedProcedureQueryOperations.ByConnection(connectionId);
        }

        public async Task<List<string>> GetDirections()
        {
            var procedures = await _storedProcedureQueryOperations.GetAll();
            
            return procedures.Select(x => x.Direction).Distinct().ToList();
        }

        public async Task<List<string>> GetNamesFromDatabase(string connectionId)
        {
            var connection = await GetConnection(connectionId);
            var provider = _procedureInfoProviderFactory.GetProvider(connection.DbType);

            return await provider.GetNames(connection);
        }

        public async Task<StoredProcedureDocument> Update(string id, ProcedureRequest request)
        {
            var document = await Get(id);
            if(document is null)
            {
                throw new NotFoundException();
            }

            return await _storedProcedureWriteOperations.UpdateInfo(id, request.Name, request.Title, request.Direction);
        }

        private async Task<ConnectionsDocument> GetConnection(string connectionId)
        {
            var connection = await _settingsQueryOperations.GetById(connectionId);

            if(connection is null)
            {
                throw new BusinessException("Настройка для БД не найдена");
            }

            return connection;
        }
    }
}