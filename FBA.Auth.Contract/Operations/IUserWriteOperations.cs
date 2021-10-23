using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Auth.Contract.Operations
{
    public interface IUserWriteOperations : IWriteOperations<UserDocument>
    {
        public Task<UserDocument> UpdateToken(string id, string newToken);
        public Task<UserDocument> ClearToken(string id);
        public Task<UserDocument> UpdateInfo(string id, string surName, string name, string patronymic);
        public Task<UserDocument> UpdatePassword(string id, HashedPassword newPassword);
    }
}