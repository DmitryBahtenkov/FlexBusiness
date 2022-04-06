using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Backend.Attributes;
using FBA.Database.Contract.Dashboards.Models.Request;
using FBA.Database.Contract.Dashboards.Models.Response;
using FBA.Database.Contract.Dashboards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [ApiController]
    [Route("api/dashboards")]
    [Authorize]
    [ValidateModel]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("{id}")]
        public async Task<DashboardResponse> Get(string id)
            => await _dashboardService.ById(id);

        [HttpPost]
        public async Task<DashboardResponse> Create([FromBody] DashboardRequest request)
            => await _dashboardService.Create(request);

        [HttpPut("{id}")]
        public async Task<DashboardResponse> Update(string id, [FromBody] DashboardRequest request)
            => await _dashboardService.Update(id, request);

        [HttpGet("by-connection/{connectionId}")]
        public async Task<List<DashboardResponse>> ByConnection(string connectionId)
            => await _dashboardService.ByConnection(connectionId);

        [HttpGet("by-procedure/{procedureId}")]
        public async Task<List<DashboardResponse>> ByProcedure(string procedureId)
            => await _dashboardService.ByStoredProcedure(procedureId);
    }
}