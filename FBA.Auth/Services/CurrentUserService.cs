using System;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using Microsoft.AspNetCore.Http;

namespace FBA.Auth.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserQueryOperations _userQueryOperations;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserQueryOperations userQueryOperations)
        {
            _httpContextAccessor = httpContextAccessor;
            _userQueryOperations = userQueryOperations;
        }
        
        public UserDocument Get()
        {
            var login = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            
            return string.IsNullOrEmpty(login) 
                ? default 
                : _userQueryOperations
                    .ByLogin(login)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
        }
    }
}