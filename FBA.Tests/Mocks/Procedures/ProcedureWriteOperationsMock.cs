using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.Procedures
{
    public class ProcedureWriteOperationsMock : WriteOperationsMock<StoredProcedureDocument>, IStoredProcedureWriteOperations
    {
        public Task<StoredProcedureDocument> UpdateInfo(string id, string name, string title)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.Name = name;
                document.Title = title;
            }

            return Task.FromResult(document);
        }

        public Task<StoredProcedureDocument> UpdateParameters(string id, ParameterInfoEmbeddedDocument[] parameters)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.Parameters = parameters;
            }

            return Task.FromResult(document);
        }
    }
}