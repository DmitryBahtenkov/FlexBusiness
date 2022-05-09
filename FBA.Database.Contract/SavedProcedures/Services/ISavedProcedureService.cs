using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.SavedProcedures.Models;
using FBA.Database.Contract.SavedProcedures.Models.Requests;
using FBA.Database.Contract.StoredProcedures.Models;

namespace FBA.Database.Contract.SavedProcedures.Services
{
    public interface ISavedProcedureService
    {
        public Task<SavedProcedureDocument> Create(SavedProcedureRequest request);
        public Task<ExecuteResult> Execute(string id);
        public Task<SavedProcedureDocument> SetArchived(string id);
        public Task<List<SavedProcedureDocument>> GetAll();
    }
}