
using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Contract.Roles;
using FBA.Auth.Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    //[Authorize(Roles = RoleTags.Admin)]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<UserResponse> CreateUser([FromBody] CreateUserRequest request)
            => await _userService.CreateUser(request);

        [HttpPut("{id}")]
        public async Task<UserResponse> UpdateUser(string id, [FromBody] UpdateInfoRequest request)
            => await _userService.UpdateUser(id, request);
        
        [HttpDelete("{id}")]
        public async Task<UserResponse> DeleteUser(string id)
            => await _userService.DeleteUser(id);
    }
}