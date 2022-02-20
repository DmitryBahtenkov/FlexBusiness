using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;

namespace FBA.Database.Contract.StoredProcedures.Services
{
    public interface IProcedureInfoProvider
    {
        public Task<List<string>> GetNames(ConnectionsDocument document);
    }
}