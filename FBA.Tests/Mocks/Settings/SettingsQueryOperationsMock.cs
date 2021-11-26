using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Connections.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.Settings
{
    public class SettingsQueryOperationsMock : QueryOperationsMock<ConnectionsDocument>, ISettingsQueryOperations
    {
        
    }
}