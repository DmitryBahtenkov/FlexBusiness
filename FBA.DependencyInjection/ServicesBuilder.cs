using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using FBA.Auth.Operations;
using FBA.Auth.Services;
using FBA.CrossCutting.Contract.Logging;
using FBA.CrossCutting.Logging;
using FBA.Database.Builders;
using FBA.Database.Contract.Builders;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Connections.Services;
using FBA.Database.Contract.Dashboards.Operations;
using FBA.Database.Contract.Dashboards.Services;
using FBA.Database.Contract.Diagram.Operations;
using FBA.Database.Contract.Diagram.Services;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Database.Contract.StoredProcedures.Services;
using FBA.Database.Dashboards.Operations;
using FBA.Database.Dashboards.Services;
using FBA.Database.Diagram.Operations;
using FBA.Database.Diagram.Services;
using FBA.Database.Operations;
using FBA.Database.Services;
using FBA.Database.StoredProcedures.Operations;
using FBA.Database.StoredProcedures.Providers;
using FBA.Database.StoredProcedures.Services;
using FBA.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FBA.DependencyInjection
{
    public static class ServicesBuilder
    {
        public static void Build(IServiceCollection collection)
        {
            collection.AddScoped<DbContext>();
            
            collection.AddScoped<IUserWriteOperations, UserWriteOperations>();
            collection.AddScoped<IUserQueryOperations, UserQueryOperations>();
            collection.AddScoped<ILoginService, LoginService>();
            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<UserMapper>();
            collection.AddScoped<ICurrentUserService, CurrentUserService>();

            collection.AddScoped<ISettingsQueryOperations, SettingsQueryOperations>();
            collection.AddScoped<ISettingsWriteOperations, SettingsWriteOperations>();
            collection.AddScoped<IConnectionsService, ConnectionService>();
            
            collection.AddScoped<ILogger, LoggerService>();
            collection.AddScoped<IConnectionStingBuilderFactory, ConnectionStingBuilderFactory>();

            collection.AddScoped<IDiagramService, DiagramService>();
            collection.AddScoped<ITableProviderFactory, TableProviderFactory>();
            collection.AddScoped<IDiagramQueryOperations, DiagramQueryOperations>();
            collection.AddScoped<IDiagramWriteOperations, DiagramWriteOperations>();

            collection.AddScoped<IStoredProcedureQueryOperations, StoredProcedureQueryOperations>();
            collection.AddScoped<IStoredProcedureWriteOperations, StoredProcedureWriteOperations>();
            collection.AddScoped<IStoredProcedureService, StoredProcedureService>();
            collection.AddScoped<IProcedureInfoProviderFactory, ProcedureInfoProviderFactory>();

            collection.AddScoped<IDashboardQueryOperations, DashboardQueryOperations>();
            collection.AddScoped<IDashboardWriteOperations, DashboardWriteOperations>();
            collection.AddScoped<IDashboardService, DashboardService>();
        }
    }
}