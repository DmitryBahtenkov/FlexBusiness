using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.Diagram.Models;
using FBA.Database.Contract.Diagram.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.Diagram.Operations
{
    public class DiagramQueryOperations : QueryOperations<DiagramDocument>, IDiagramQueryOperations
    {
        public DiagramQueryOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<DiagramDocument> ByConnection(string connectionId)
        {
            return await GetOne(F.Eq(x => x.ConnectionId, connectionId));
        }

        public async Task<List<DiagramDocument>> ByUser(string userId)
        {
           return await GetMany(F.Eq(x => x.UserId, userId));
        }
    }
}
