using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Auth.Contract.Services;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Database.Contract.SavedProcedures.Models;
using FBA.Database.Contract.SavedProcedures.Models.Requests;
using FBA.Database.Contract.SavedProcedures.Operations;
using FBA.Database.Contract.SavedProcedures.Services;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Database.Contract.StoredProcedures.Services;
using FBA.Repository.Helpers;

namespace FBA.Database.SavedProcedures.Services
{
    public class SavedProcedureService : ISavedProcedureService
    {
        
		private readonly ISavedProcedureQueryOperations _savedProcedureQueryOperations;
		private readonly IStoredProcedureService _storedProcedureService;
		private readonly ISavedProcedureWriteOperations _savedProcedureWriteOperations;
		private readonly ICurrentUserService _currentUserService;
		private readonly IStoredProcedureQueryOperations _storedProcedureQueryOperations;

		public SavedProcedureService(
			ISavedProcedureQueryOperations savedProcedureQueryOperations,
			ISavedProcedureWriteOperations savedProcedureWriteOperations,
			IStoredProcedureService storedProcedureService,
			IStoredProcedureQueryOperations storedProcedureQueryOperations,
			ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
			_storedProcedureQueryOperations = storedProcedureQueryOperations;
			_storedProcedureService = storedProcedureService;
			_savedProcedureWriteOperations = savedProcedureWriteOperations;
			_savedProcedureQueryOperations = savedProcedureQueryOperations;
		}

        public async Task<SavedProcedureDocument> Create(SavedProcedureRequest request)
        {
            var procedure = await _storedProcedureService.Get(request.StoredProcedureId);
            if (procedure is null)
            {
                throw new BusinessException("Хранимая процедура не найдена");
            }

            if(procedure.Parameters.Length != request.Parameters.Count)
            {
                throw new BusinessException("Некорректно введены параметры");
            }

            var document = new SavedProcedureDocument()
            {
                Id = IdGen.NewId(),
                Name = request.Name,
                StoredProcedureId = request.StoredProcedureId,
                Parameters = request.Parameters,
                UserId = _currentUserService.Get().Id
            };

            return await _savedProcedureWriteOperations.Create(document);
        }

        public async Task<ExecuteResult> Execute(string id)
        {
            var savedProcedure = await _savedProcedureQueryOperations.GetById(id);
            if (savedProcedure is null)
            {
                throw new NotFoundException();
            }

            return await _storedProcedureService.Execute(savedProcedure.StoredProcedureId, savedProcedure.Parameters);
        }

        public async Task<List<SavedProcedureDocument>> GetAll()
        {
            return await _savedProcedureQueryOperations.ByUser(_currentUserService.Get().Id);
        }

        public async Task<SavedProcedureDocument> SetArchived(string id)
        {
            return await _savedProcedureWriteOperations.Archive(id);
        }
    }
}