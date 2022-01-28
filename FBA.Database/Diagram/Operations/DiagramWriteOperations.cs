using System;
using FBA.Database.Contract.Diagram.Models;
using FBA.Database.Contract.Diagram.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.Diagram.Operations
{
    public class DiagramWriteOperations : WriteOperations<DiagramDocument>, IDiagramWriteOperations
    {
        public DiagramWriteOperations(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
