using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract;
using FBA.Database.Contract.StoredProcedures.Services;

namespace FBA.Tests.Mocks.Procedures
{
    public class ProcedureInfoProviderFactoryMock : IProcedureInfoProviderFactory
    {
        public IProcedureInfoProvider GetProvider(DbType type)
        {
            return new ProcedureInfoProviderMock();
        }
    }
}