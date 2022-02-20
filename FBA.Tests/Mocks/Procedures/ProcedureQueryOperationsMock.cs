using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.Procedures
{
    public class ProcedureQueryOperationsMock : QueryOperationsMock<StoredProcedureDocument>, IStoredProcedureQueryOperations
    {
        public Task<List<StoredProcedureDocument>> ByConnection(string connectionId)
        {
            return Task.FromResult(GetMany(x => x.ConnectionId == connectionId).ToList());
        }
    }
}