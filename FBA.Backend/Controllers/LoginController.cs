using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Contract.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<UserResponse> Login([FromBody] LoginRequest request)
            => await _loginService.Login(request);
        
        [HttpPost("logout/{id}")]
        public async Task Logout(string id)
            => await _loginService.Logout(id);
    }
}