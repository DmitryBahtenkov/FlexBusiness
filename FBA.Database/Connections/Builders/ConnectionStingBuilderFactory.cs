using System;
using FBA.Database.Contract;
using FBA.Database.Contract.Builders;

namespace FBA.Database.Builders
{
    public class ConnectionStingBuilderFactory : IConnectionStingBuilderFactory
    {
        public IConnectionStringBuilder GetBuilder(DbType type)
        {
            return type switch
            {
                DbType.MsSql => new MsConnectionStringBuilder(),
                DbType.Postgres => throw new NotImplementedException("Пока что не реализовано"),
                DbType.MySql => throw new NotImplementedException("Пока что не реализовано"),
                _ => throw new ArgumentException("Указана неизвестная субд", nameof(type))
            };
        }
    }
}