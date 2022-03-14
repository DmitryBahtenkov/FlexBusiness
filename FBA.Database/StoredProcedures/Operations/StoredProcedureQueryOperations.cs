using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.StoredProcedures.Operations
{
    public class StoredProcedureQueryOperations : QueryOperations<StoredProcedureDocument>, IStoredProcedureQueryOperations
    {
        public StoredProcedureQueryOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<StoredProcedureDocument>> ByConnection(string connectionId)
        {
            return await GetMany(F.Eq(x => x.ConnectionId, connectionId));
        }
    }
}