using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;

namespace FBA.Auth.Contract.Services
{
    public interface IUserService
    {
        public Task<UserResponse> CreateUser(CreateUserRequest request);
        public Task<UserResponse> UpdateUser(string id, UpdateInfoRequest request);
        public Task<UserResponse> DeleteUser(string userId);
    }
}