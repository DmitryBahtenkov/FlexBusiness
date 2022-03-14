using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Models.Requests;
using FBA.Database.Contract.StoredProcedures.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/procedures")]
    public class StoredProcedureController : ControllerBase
    {
		private readonly IStoredProcedureService _storedProcedureService;

		public StoredProcedureController(IStoredProcedureService storedProcedureService)
		{
			_storedProcedureService = storedProcedureService;
		}

        [HttpPost("{connectionId}")]
        public async Task<StoredProcedureDocument> Create(string connectionId, ProcedureRequest request)
            => await _storedProcedureService.Create(connectionId, request);

        [HttpPut("{id}")]
        public async Task<StoredProcedureDocument> Update(string id, ProcedureRequest request)
            => await _storedProcedureService.Update(id, request);

        [HttpGet("{id}")]
        public async Task<StoredProcedureDocument> Get(string id)
            => await _storedProcedureService.Get(id);

        [HttpGet]
        public async Task<List<StoredProcedureDocument>> GetAll()
            => await _storedProcedureService.GetAll();

        [HttpGet("by-connection/{connectionId}")]
        public async Task<List<StoredProcedureDocument>> GetByConnection(string connectionId)
            => await _storedProcedureService.GetByConnection(connectionId);
    }
}