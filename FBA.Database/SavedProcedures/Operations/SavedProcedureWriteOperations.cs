using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.SavedProcedures.Models;
using FBA.Database.Contract.SavedProcedures.Operations;
using FBA.Repository;
using FBA.Repository.Extensions;
using FBA.Repository.Operations;

namespace FBA.Database.SavedProcedures.Operations
{
    public class SavedProcedureWriteOperations : WriteOperations<SavedProcedureDocument>, ISavedProcedureWriteOperations
    {
        public SavedProcedureWriteOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<SavedProcedureDocument> Archive(string id)
        {
            return await UpdateOne(F.ById(id), U.Set(x => x.IsArchived, true));
        }
    }
}