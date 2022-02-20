using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.StoredProcedures.Operations
{
    public interface IStoredProcedureQueryOperations : IQueryOperations<StoredProcedureDocument>
    {
        public Task<List<StoredProcedureDocument>> ByConnection(string connectionId);
    }
}