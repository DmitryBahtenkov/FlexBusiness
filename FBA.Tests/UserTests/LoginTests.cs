using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Roles;
using FBA.Auth.Contract.Services;
using FBA.Tests.Data;
using Xunit;

namespace FBA.Tests.UserTests
{
    public class LoginTests
    {
        private readonly ILoginService _loginService;
        private readonly IUserQueryOperations _userQueryOperations;

        public LoginTests(ILoginService loginService, IUserQueryOperations userQueryOperations)
        {
            _loginService = loginService;
            _userQueryOperations = userQueryOperations;
        }

        [Fact(DisplayName = "Проверка корректной авторизации пользователя")]
        public async Task ValidLoginTest()
        {
            //arrange
            var request = new LoginRequest
            {
                Login = TestUsers.ValidUser.Login,
                Password = "string"
            };
            
            //act
            var response = await _loginService.Login(request);
            
            //assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.Token);
            Assert.Equal(request.Login, response.Login);
            Assert.Equal(RoleTags.Default, response.Role);
        }

        [Fact(DisplayName = "Проверка уничтожения токена после выхода из аккаунта")]
        public async Task LogoutTest()
        {
            var id = TestUsers.UserForLogout.Id;

            await _loginService.Logout(id);
            
            var user = await _userQueryOperations.GetById(id);
            
            Assert.NotNull(user);
            Assert.Null(user.Token);
        }
    }
}