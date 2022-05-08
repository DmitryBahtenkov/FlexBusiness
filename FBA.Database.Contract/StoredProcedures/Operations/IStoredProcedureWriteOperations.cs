using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Database.Contract.StoredProcedures.Operations
{
    public interface IStoredProcedureWriteOperations : IWriteOperations<StoredProcedureDocument>
    {
        public Task<StoredProcedureDocument> UpdateParameters(string id, ParameterInfoEmbeddedDocument[] parameters);
        public Task<StoredProcedureDocument> UpdateInfo(string id, string name, string title, string direction);
    }
}