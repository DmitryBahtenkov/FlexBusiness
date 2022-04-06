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

        public async Task<List<DashboardDocument>> ByConnection(string connectionId)
        {
            return await GetMany(F.Eq(x => x.ConnectionId, connectionId));
        }

        public async Task<List<DashboardDocument>> ByStoredProcedure(string storedProcedure)
        {
            return await GetMany(F.Eq(x => x.StoredProcedureId, storedProcedure));
        }
    }
}