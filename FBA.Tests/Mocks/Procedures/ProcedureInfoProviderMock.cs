using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Services;

namespace FBA.Tests.Mocks.Procedures
{
    public class ProcedureInfoProviderMock : IProcedureInfoProvider
    {
        public  Task<List<string>> GetNames(ConnectionsDocument document)
        {
            return Task.FromResult(new List<string>() { "@param" });
        }

        public Task<ParameterInfoEmbeddedDocument[]> GetParameters(ConnectionsDocument document, string procedureName)
        {
            return Task.FromResult(Array.Empty<ParameterInfoEmbeddedDocument>());
        }
    }
}