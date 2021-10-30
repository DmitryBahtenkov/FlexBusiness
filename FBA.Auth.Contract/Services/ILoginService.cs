using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;

namespace FBA.Auth.Contract.Services
{
    public interface ILoginService
    {
        public Task<UserResponse> Login(LoginRequest request);
        public Task Logout(string id);
    }
}