using System.Linq;
using System.Threading.Tasks;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Operations;
using FBA.Tests.Mocks.Base;

namespace FBA.Tests.Mocks.User
{
    public class UserWriteOperationsMock : WriteOperationsMock<UserDocument>, IUserWriteOperations
    {
        public Task<UserDocument> UpdateToken(string id, string newToken)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.Token = newToken;
            }

            return Task.FromResult(document);
        }

        public Task<UserDocument> ClearToken(string id)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.Token = null;
            }

            return Task.FromResult(document);
        }

        public Task<UserDocument> UpdateInfo(string id, string surName, string name, string patronymic)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.SurName = surName;
                document.Name = name;
                document.Patronymic = patronymic;
            }

            return Task.FromResult(document);
        }

        public Task<UserDocument> UpdatePassword(string id, HashedPassword newPassword)
        {
            var document = Storage.FirstOrDefault(x => x.Id == id);

            if (document is not null)
            {
                document.Password = newPassword;
            }

            return Task.FromResult(document);
        }
    }
}