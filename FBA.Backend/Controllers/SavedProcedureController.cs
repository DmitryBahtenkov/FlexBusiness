using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Backend.Attributes;
using FBA.Database.Contract.SavedProcedures.Models;
using FBA.Database.Contract.SavedProcedures.Models.Requests;
using FBA.Database.Contract.SavedProcedures.Services;
using FBA.Database.Contract.StoredProcedures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [Route("api/v1/saved")]
    [ApiController]
    [ValidateModel]
    [Authorize]
    public class SavedProcedureController : ControllerBase
    {
		private readonly ISavedProcedureService _savedProcedureService;

		public SavedProcedureController(ISavedProcedureService savedProcedureService)
		{
			_savedProcedureService = savedProcedureService;
		}

        [HttpGet]
        public async Task<List<SavedProcedureDocument>> All()
            => await _savedProcedureService.GetAll();

        [HttpPost]
        public async Task<SavedProcedureDocument> Create(SavedProcedureRequest request)
            => await _savedProcedureService.Create(request);

        [HttpDelete]
        public async Task<SavedProcedureDocument> Delete(string id)
            => await _savedProcedureService.SetArchived(id);

        [HttpGet("{id}/execute")]
        public async Task<ExecuteResult> Execute(string id)
            => await _savedProcedureService.Execute(id);
    }
}