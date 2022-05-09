using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.Tables;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [Route("api/v1/table")]
    [ApiController]
    public class TableController : ControllerBase
    {
		private readonly ITableService _tableService;

		public TableController(ITableService tableService)
		{
			_tableService = tableService;
		}

        [HttpPost("{connectionId}/select/{table}")]
        public async Task<ExecuteResult> ExecuteSelect(string connectionId, string table, [FromBody] SelectQuery query = null)
        {
            return await _tableService.ExecuteSelect(connectionId, table, query);
        }
        
        [HttpGet("{connectionId}/tables")]
        public async Task<List<TableInfo>> GetTables(string connectionId)
        {
            return await _tableService.GetTables(connectionId);
        }
    }
}