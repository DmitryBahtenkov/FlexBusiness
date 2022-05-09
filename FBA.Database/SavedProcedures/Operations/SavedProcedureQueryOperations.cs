using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.SavedProcedures.Models;
using FBA.Database.Contract.SavedProcedures.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.SavedProcedures.Operations
{
    public class SavedProcedureQueryOperations : QueryOperations<SavedProcedureDocument>, ISavedProcedureQueryOperations
    {
        public SavedProcedureQueryOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<SavedProcedureDocument>> ByUser(string userId)
        {
            return await GetMany(F.Eq(x => x.UserId, userId));
        }
    }
}