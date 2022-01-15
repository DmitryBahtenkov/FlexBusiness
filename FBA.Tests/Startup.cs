using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using FBA.Auth.Services;
using FBA.Database.Builders;
using FBA.Database.Contract.Builders;
using FBA.Database.Contract.Connections.Operations;
using FBA.Database.Contract.Connections.Services;
using FBA.Database.Contract.Diagram.Services;
using FBA.Database.Diagram.Services;
using FBA.Database.Operations;
using FBA.Database.Services;
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

            services.AddScoped<IConnectionStingBuilderFactory, ConnectionStingBuilderFactory>();
            
            services.AddSingleton<ITableProviderFactory, TableProviderFactory>();
        }
    }
}