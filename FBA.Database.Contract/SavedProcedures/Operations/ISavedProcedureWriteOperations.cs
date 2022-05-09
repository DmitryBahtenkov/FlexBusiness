using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.SavedProcedures.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.SavedProcedures.Operations
{
    public interface ISavedProcedureWriteOperations : IWriteOperations<SavedProcedureDocument>
    {
        
    }
}