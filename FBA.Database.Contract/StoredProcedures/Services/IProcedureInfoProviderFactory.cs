using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBA.Database.Contract.StoredProcedures.Services
{
    public interface IProcedureInfoProviderFactory
    {
        public IProcedureInfoProvider GetProvider(DbType type);
    }
}