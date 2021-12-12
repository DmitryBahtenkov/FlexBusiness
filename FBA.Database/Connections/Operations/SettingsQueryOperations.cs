using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Database.Operations
{
    public class SettingsQueryOperations : QueryOperations<ConnectionsDocument>, ISettingsQueryOperations
    {
        public SettingsQueryOperations(DbContext dbContext) : base(dbContext)
        {
        }
    }
}