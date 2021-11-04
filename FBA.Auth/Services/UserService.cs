using System.Linq;
using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Models.Requests;
using FBA.Auth.Contract.Models.Responses;
using FBA.Auth.Contract.Operations;
using FBA.Auth.Contract.Roles;
using FBA.Auth.Contract.Services;
using FBA.Auth.Helpers;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.Repository.Helpers;

namespace FBA.Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserQueryOperations _userQueryOperations;
        private readonly IUserWriteOperations _userWriteOperations;
        private readonly UserMapper _mapper;

        public UserService(
            IUserQueryOperations userQueryOperations, 
            IUserWriteOperations userWriteOperations, 
            UserMapper mapper)
        {
            _userQueryOperations = userQueryOperations;
            _userWriteOperations = userWriteOperations;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest request)
        {
            await ValidateRequest(request);

            var newDocument = _mapper.New(request);

            var user = await _userWriteOperations.Create(newDocument);
            // todo: вынести в отдельный класс
            return _mapper.FromDocument(user);
        }

        private async Task ValidateRequest(CreateUserRequest request)
        {
            if (!RoleTags.GetAll().Contains(request.Role))
            {
                throw new BusinessException("Такой роли не существует");
            }
            
            var user = await _userQueryOperations.ByLogin(request.Login);
            if (user is not null)
            {
                var fio = "{user.SurName} {user.Name} {user.Patronymic}";
                throw new BusinessException($"Такой пользователь уже существует: {fio}");
            }
        }

        public async Task<UserResponse> UpdateUser(string id, UpdateInfoRequest request)
        {
            var document = await _userWriteOperations.UpdateInfo(id,
                request.SurName,
                request.Name,
                request.Patronymic);
            
            if (document is null)
            {
                throw new BusinessException("Пользователь не найден");
            }

            return _mapper.FromDocument(document);
        }

        public async Task<UserResponse> DeleteUser(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}