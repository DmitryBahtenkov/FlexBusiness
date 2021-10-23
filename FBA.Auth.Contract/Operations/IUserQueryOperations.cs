using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Repository.Contract.Operations;

namespace FBA.Auth.Contract.Operations
{
    public interface IUserQueryOperations : IQueryOperations<UserDocument>
    {
        public Task<UserDocument> ByToken(string token);
        public Task<UserDocument> ByLogin(string login);
    }
}