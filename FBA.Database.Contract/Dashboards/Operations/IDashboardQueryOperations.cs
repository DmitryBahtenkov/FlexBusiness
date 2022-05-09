using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.Dashboards.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.Dashboards.Operations
{
    public interface IDashboardQueryOperations : IQueryOperations<DashboardDocument>
    {
        public Task<List<DashboardDocument>> ByConnection(string connectionId, string userId = null);
        public Task<List<DashboardDocument>> ByStoredProcedure(string storedProcedure, string userId = null);
    }
}