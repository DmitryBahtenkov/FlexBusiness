using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.StoredProcedures.Models;

namespace FBA.Database.Contract.StoredProcedures.Services
{
    public interface IProcedureInfoProvider
    {
        public Task<List<string>> GetNames(ConnectionsDocument document);
        public Task<object> ExecuteStoredProcedure(ConnectionsDocument document, string procedureName, Dictionary<string, string> parameters);

        public Task<ParameterInfoEmbeddedDocument[]> GetParameters(ConnectionsDocument document, string procedureName);
    }
}