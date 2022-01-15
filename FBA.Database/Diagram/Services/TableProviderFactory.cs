using System;
using FBA.Database.Contract;
using FBA.Database.Contract.Diagram.Services;

namespace FBA.Database.Diagram.Services
{
    public class TableProviderFactory : ITableProviderFactory
    {
        public ITableProvider GetProvider(DbType type)
        {
            return type switch
            {
                DbType.MsSql => new MsSqlTableProvider(),
                DbType.MySql => throw new NotImplementedException(),
                DbType.Postgres => throw new NotImplementedException(),
                _ => throw new ArgumentException(nameof(type))
            };
        }
    }
}