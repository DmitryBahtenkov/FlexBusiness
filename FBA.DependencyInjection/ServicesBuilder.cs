using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using FBA.Auth.Operations;
using FBA.Auth.Services;
using FBA.CrossCutting.Contract.Logging;
using FBA.CrossCutting.Logging;
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

            collection.AddScoped<ILogger, LoggerService>();

        }
    }
}