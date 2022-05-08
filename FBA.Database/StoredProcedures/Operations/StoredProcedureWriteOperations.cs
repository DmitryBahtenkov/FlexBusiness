using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Repository;
using FBA.Repository.Extensions;
using FBA.Repository.Operations;
using MongoDB.Driver;

namespace FBA.Database.StoredProcedures.Operations
{
    public class StoredProcedureWriteOperations : WriteOperations<StoredProcedureDocument>, IStoredProcedureWriteOperations
    {
        public StoredProcedureWriteOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<StoredProcedureDocument> UpdateInfo(string id, string name, string title, string direction)
        {
            var filter = F.ById(id);
            var update = U
                .Set(x => x.Name, name)
                .Set(x => x.Title, title)
                .Set(x => x.Direction, direction);

            return await UpdateOne(filter, update);
        }

        public async Task<StoredProcedureDocument> UpdateParameters(string id, ParameterInfoEmbeddedDocument[] parameters)
        {
            return await UpdateOne(F.ById(id), U.Set(x => x.Parameters, parameters));
        }
    }
}