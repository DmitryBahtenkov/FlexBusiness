using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract;
using FBA.Database.Contract.StoredProcedures.Services;

namespace FBA.Database.StoredProcedures.Providers
{
    public class ProcedureInfoProviderFactory : IProcedureInfoProviderFactory
    {
        public IProcedureInfoProvider GetProvider(DbType type)
        {
            return type switch
            {
                DbType.MsSql => new MsProcedureInfoProvider(),
                _ => throw new NotImplementedException()
            };
        }
    }
}