using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Services;

namespace FBA.Tests.Mocks.User
{
    public class CurrentUserServiceMock : ICurrentUserService
    {
        public UserDocument Get()
        {
            return new UserDocument
            {
                Id = "TestUserId"
            };
        }
    }
}