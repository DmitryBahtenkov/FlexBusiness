using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.User
{
    public class UserQueryOperationsMock : QueryOperationsMock<UserDocument>, IUserQueryOperations
    {
        public Task<UserDocument> ByToken(string token)
        {
            return Task.FromResult(GetOne(x=>x.Token == token));
        }

        public Task<UserDocument> ByLogin(string login)
        {
            return Task.FromResult(GetOne(x=>x.Login == login));
        }
    }
}