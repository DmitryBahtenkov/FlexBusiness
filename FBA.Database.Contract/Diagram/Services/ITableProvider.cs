using System.Collections.Generic;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Diagram.Models;

namespace FBA.Database.Contract.Diagram.Services
{
    public interface ITableProvider
    {
        public Task<List<TableEmbeddedDocument>> GetTables(ConnectionsDocument connection);
    }
}