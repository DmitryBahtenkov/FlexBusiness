using System.Threading.Tasks;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Services;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Tests.Data;
using Xunit;

namespace FBA.Tests.UserTests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly IUserQueryOperations _userQueryOperations;

        public UserServiceTests(IUserService userService, IUserQueryOperations userQueryOperations)
        {
            _userService = userService;
            _userQueryOperations = userQueryOperations;
        }

        #region Create

        [Fact(DisplayName = "Проверка создания пользователя с паролем")]
        public async Task CreateUserWithPasswordTest()
        {
            var request = new CreateUserRequest
            {
                Name = "Ирина",
                SurName = "Цикозина",
                Password = "qwerty123",
                Patronymic = "Евгеньевна",
                Login = "dinozavrik35",
                IsNewUser = false
            };

            var response = await _userService.CreateUser(request);
            
            Assert.NotNull(response);
            Assert.NotEmpty(response.Id);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.SurName, response.SurName);

            var user = await _userQueryOperations.GetById(response.Id);
            Assert.NotNull(user);
            Assert.NotNull(user.Password);
        }
        
        [Fact(DisplayName = "Проверка создания пользователя без пароля")]
        public async Task CreateUserWithoutPasswordTest()
        {
            var request = new CreateUserRequest
            {
                Name = "---",
                SurName = "---",
                Patronymic = "---",
                Login = "---",
                IsNewUser = true
            };

            var response = await _userService.CreateUser(request);
            
            Assert.NotNull(response);
            Assert.NotEmpty(response.Id);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.SurName, response.SurName);
            
            var user = await _userQueryOperations.GetById(response.Id);
            Assert.NotNull(user);
            Assert.Null(user.Password);
        }
        
        [Fact(DisplayName = "Проверка создания пользователя с некорректной ролью")]
        public async Task CreateUserWithInvalidRoleTest()
        {
            var request = new CreateUserRequest
            {
                Name = "aaa",
                SurName = "aaa",
                Patronymic = "aaaa",
                Login = "aaaaa",
                IsNewUser = true,
                Role = "aaaa"
            };

            await Assert.ThrowsAsync<BusinessException>(async () => await _userService.CreateUser(request));
        }

        #endregion

        #region Update

        [Fact(DisplayName = "Корректное обновление пользователя")]
        public async Task UpdateValidUser()
        {
            var updateRequest = new UpdateInfoRequest()
            {
                SurName = "Analniy",
                Name = "Anal",
                Patronymic = "Alekseevich"
            };

            var result = await _userService.UpdateUser(TestUsers.UserForUpdate.Id, updateRequest);
            
            Assert.NotNull(result);
            Assert.Equal(updateRequest.Name, result.Name);
            Assert.Equal(updateRequest.SurName, result.SurName);
            Assert.Equal(updateRequest.Patronymic, result.Patronymic);
        }
        
        [Fact(DisplayName = "Обновление несуществующего пользователя")]
        public async Task UpdateNotExistedUser()
        {
            var updateRequest = new UpdateInfoRequest()
            {
                SurName = "Analniy",
                Name = "Anal",
                Patronymic = "Alekseevich"
            };
            
            await Assert.ThrowsAsync<BusinessException>(
                async () => await _userService.UpdateUser("", updateRequest)
                );
        }

        #endregion
    }
}