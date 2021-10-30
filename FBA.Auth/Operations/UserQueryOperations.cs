using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Operations;
using FBA.Repository;
using FBA.Repository.Operations;

namespace FBA.Auth.Operations
{
    public sealed class UserQueryOperations : QueryOperations<UserDocument>, IUserQueryOperations
    {
        public UserQueryOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserDocument> ByToken(string token)
        {
            return await GetOne(F.Eq(x => x.Token, token));
        }

        public async Task<UserDocument> ByLogin(string login)
        {
            return await GetOne(F.Eq(x => x.Login, login));
        }
    }
}