using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Helpers;
using FBA.Repository.Helpers;

namespace FBA.Auth.Services
{
    public class UserMapper
    {
        public UserDocument New(CreateUserRequest createUserRequest)
        {
            var password = createUserRequest.IsNewUser
                ? null
                : PasswordHelper.GeneratePassword(createUserRequest.Password);
            
            return new ()
            {
                Id = IdGen.NewId(),
                IsNewUser = createUserRequest.IsNewUser,
                Login = createUserRequest.Login,
                Name = createUserRequest.Name,
                SurName = createUserRequest.SurName,
                Patronymic = createUserRequest.Patronymic,
                Role = createUserRequest.Role,
                Password = password
            };
        }

        public UserResponse FromDocument(UserDocument document)
        {
            return new()
            {
                Id = document.Id,
                Login = document.Login,
                Name = document.Name,
                SurName = document.SurName,
                Patronymic = document.Patronymic,
                Token = document.Token,
                Role = document.Role
            };
        }
    }
}