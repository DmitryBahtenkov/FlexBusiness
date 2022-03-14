using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Services;

namespace FBA.Database.StoredProcedures.Providers
{
    public class MsProcedureInfoProvider : IProcedureInfoProvider
    {
        public Task<List<string>> GetNames(ConnectionsDocument document)
        {
            throw new NotImplementedException();
        }

        public Task<ParameterInfoEmbeddedDocument[]> GetParameters(ConnectionsDocument document, string procedureName)
        {
            throw new NotImplementedException();
        }

        
    }
}