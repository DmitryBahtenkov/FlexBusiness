using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Models.Requests;

namespace FBA.Database.Contract.StoredProcedures.Services
{
    public interface IStoredProcedureService
    {
        public Task<StoredProcedureDocument> Create(string connectionId, ProcedureRequest request);
        public Task<StoredProcedureDocument> Update(string id, ProcedureRequest request);
        public Task<List<string>> GetDirections();
        public Task<List<string>> GetNamesFromDatabase(string connectionId);
        public Task<List<StoredProcedureDocument>> GetAll();
        public Task<StoredProcedureDocument> Get(string id);
        public Task<List<StoredProcedureDocument>> GetByConnection(string connectionId);
        public Task<object> Execute(string id, Dictionary<string, string> parameters);
    }
}