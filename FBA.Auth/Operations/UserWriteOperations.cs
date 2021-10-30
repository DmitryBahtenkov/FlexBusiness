using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Operations;
using FBA.Repository;
using FBA.Repository.Extensions;
using FBA.Repository.Operations;
using MongoDB.Driver;

namespace FBA.Auth.Operations
{
    public class UserWriteOperations : WriteOperations<UserDocument>, IUserWriteOperations
    {
        public UserWriteOperations(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserDocument> UpdateToken(string id, string newToken)
        {
            return await UpdateOne(F.ById(id), 
                U.Set(x => x.Token, newToken));
        }

        public async Task<UserDocument> ClearToken(string id)
        {
            return await UpdateOne(F.ById(id), 
                U.Set(x => x.Token, null));
        }

        public async Task<UserDocument> UpdateInfo(string id, string surName, string name, string patronymic)
        {
            var update = U
                .Set(x => x.SurName, surName)
                .Set(x=>x.Name, name)
                .Set(x=>x.Patronymic, patronymic);
            
            return await UpdateOne(F.ById(id), update);
        }

        public async Task<UserDocument> UpdatePassword(string id, HashedPassword newPassword)
        {
            return await UpdateOne(F.ById(id), 
                U.Set(x => x.Password, newPassword));
        }
    }
}