using FBA.Database.Contract;
using FBA.Database.Contract.Diagram.Services;

namespace FBA.Database.Diagram.Services
{
    public class TableProviderFactory : ITableProviderFactory
    {
        public ITableProvider GetProvider(DbType type)
        {
            throw new System.NotImplementedException();
        }
    }
}