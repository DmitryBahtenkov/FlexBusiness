using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Dashboards.Models;
using FBA.Database.Contract.Dashboards.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.Dashboards.Operations
{
    public class DashboardQueryOperations : QueryOperations<DashboardDocument>, IDashboardQueryOperations
    {
        public DashboardQueryOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<DashboardDocument>> ByConnection(string connectionId, string userId = null)
        {
            var filter = F.Eq(x => x.ConnectionId, connectionId);
            
            if (!string.IsNullOrEmpty(userId))
            {
                filter &= F.Eq(x => x.UserId, userId);
            }

            return await GetMany(filter);
        }

        public async Task<List<DashboardDocument>> ByStoredProcedure(string storedProcedure, string userId = null)
        {
            var filter = F.Eq(x => x.StoredProcedureId, storedProcedure);
            
            if (!string.IsNullOrEmpty(userId))
            {
                filter &= F.Eq(x => x.UserId, userId);
            }

            return await GetMany(filter);
        }
    }
}