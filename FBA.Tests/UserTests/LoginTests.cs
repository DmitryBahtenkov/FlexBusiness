using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Contract.Roles;
using FBA.Auth.Contract.Services;
using FBA.Tests.Data;
using Xunit;

namespace FBA.Tests.UserTests
{
    public class LoginTests
    {
        private readonly ILoginService _loginService;

        public LoginTests(ILoginService loginService)
        {
            _loginService = loginService;
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
    }
}