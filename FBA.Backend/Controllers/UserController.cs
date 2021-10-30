
using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Contract.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="request">Тело запроса</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UserResponse> CreateUser([FromBody] CreateUserRequest request)
            => await _userService.CreateUser(request);
    }
}