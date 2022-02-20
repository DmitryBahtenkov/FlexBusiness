using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using FBA.Auth.Services;
using FBA.Database.Builders;
using FBA.Database.Contract.Builders;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Connections.Services;
using FBA.Database.Contract.Diagram.Services;
using FBA.Database.Contract.StoredProcedures.Operations;
using FBA.Database.Contract.StoredProcedures.Services;
using FBA.Database.Diagram.Services;
using FBA.Database.Operations;
using FBA.Database.Services;
using FBA.Database.StoredProcedures.Services;
using FBA.Tests.Mocks.Diagrams;
using FBA.Tests.Mocks.Procedures;
using FBA.Tests.Mocks.Settings;
using FBA.Tests.Mocks.User;
using Microsoft.Extensions.DependencyInjection;

namespace FBA.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserQueryOperations, UserQueryOperationsMock>();
            services.AddScoped<IUserWriteOperations, UserWriteOperationsMock>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserMapper>();
            services.AddScoped<ICurrentUserService, CurrentUserServiceMock>();
            
            services.AddScoped<ISettingsQueryOperations, SettingsQueryOperationsMock>();
            services.AddScoped<ISettingsWriteOperations, SettingsWriteOperationsMock>();
            services.AddScoped<IConnectionsService, ConnectionService>();
                        
            services.AddScoped<IStoredProcedureQueryOperations, ProcedureQueryOperationsMock>();
            services.AddScoped<IStoredProcedureWriteOperations, ProcedureWriteOperationsMock>();
            services.AddScoped<IStoredProcedureService, StoredProcedureService>();
            services.AddScoped<IProcedureInfoProviderFactory, ProcedureInfoProviderFactoryMock>();


            services.AddScoped<IConnectionStingBuilderFactory, ConnectionStingBuilderFactory>();
            services.AddScoped<IDiagramService, DiagramServiceMock>();
            
            services.AddSingleton<ITableProviderFactory, TableProviderFactory>();
        }
    }
}