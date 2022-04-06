using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Dashboards.Models.Request;
using FBA.Database.Contract.Dashboards.Models.Response;

namespace FBA.Database.Contract.Dashboards.Services
{
    public interface IDashboardService
    {
        public Task<DashboardResponse> Create(DashboardRequest request);
        public Task<DashboardResponse> Update(string id, DashboardRequest request);
        public Task<DashboardResponse> ById(string id);
        public Task<List<DashboardResponse>> ByConnection(string connectionId);
        public Task<List<DashboardResponse>> ByStoredProcedure(string connectionId);
    }
}