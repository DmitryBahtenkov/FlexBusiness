using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using FBA.Auth.Services;
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
        }
    }
}