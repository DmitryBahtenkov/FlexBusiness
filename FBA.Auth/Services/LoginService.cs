using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Roles;
using FBA.Auth.Contract.Services;
using FBA.Auth.Helpers;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.CrossCutting.Contract.Global;
using Microsoft.IdentityModel.Tokens;

namespace FBA.Auth.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserWriteOperations _userWriteOperations;
        private readonly IUserQueryOperations _userQueryOperations;

        public LoginService(IUserWriteOperations userWriteOperations, IUserQueryOperations userQueryOperations)
        {
            _userWriteOperations = userWriteOperations;
            _userQueryOperations = userQueryOperations;
        }

        #region Login

        public async Task<UserResponse> Login(LoginRequest request)
        {
            var user = await ValidateLogin(request);

            return await SetToken(user);
        }
        
        private async Task<UserResponse> SetToken(UserDocument document)
        {
            var identity = GetIdentity(document);
            
            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            var documentWithToken = await _userWriteOperations.UpdateToken(document.Id, encodedJwt);
            return MapCurrentUser(documentWithToken);
        }
        
        private ClaimsIdentity GetIdentity(UserDocument userDocument)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, userDocument.Login),
                new (ClaimsIdentity.DefaultRoleClaimType, userDocument.Role)
            };
            
            ClaimsIdentity claimsIdentity =
                new (
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType
                    );
            
            return claimsIdentity;
        }

        private async Task<UserDocument> ValidateLogin(LoginRequest request)
        {
            var user = await _userQueryOperations.ByLogin(request.Login);
            if (user is null)
            {
                throw new BusinessException("Не найден пользователь");
            }

            if (PasswordHelper.ComparePassword(user, request.Password))
            {
                throw new BusinessException("Некорректный логин или пароль");
            }

            return user;
        }
        
        private UserResponse MapCurrentUser(UserDocument document)
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

        #endregion

        #region Logout

        public async Task Logout(string id)
        {
            var user = await _userQueryOperations.GetById(id);
            
            if (user is not null)
            {
                await _userWriteOperations.ClearToken(id);
            }
            else
            {
                throw new BusinessException("Пользователь не существует");
            }
        }

        #endregion
    }
}