namespace FBA.Database.Contract.Diagram.Services
{
    public interface ITableProviderFactory
    {
        public ITableProvider GetProvider(DbType type);
    }
}