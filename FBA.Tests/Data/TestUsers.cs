using System.Collections.Generic;
using System.Linq;
using FBA.Auth.Contract.Models;
using FBA.Auth.Contract.Roles;
using FBA.Auth.Helpers;

namespace FBA.Tests.Data
{
    public class TestUsers
    {
        public static UserDocument ValidUser => new()
        {
            Id = "validuserid",
            Login = "Michael",
            Name = "Michael",
            SurName = "Sapogov",
            Password = PasswordHelper.GeneratePassword("string"),
            Role = RoleTags.Default
        };
        
        public static UserDocument UserForLogout => new()
        {
            Id = "validuserid2",
            Login = "Michaelisch'e",
            Name = "Gay",
            SurName = "Sapogov",
            Password = PasswordHelper.GeneratePassword("string"),
            Role = RoleTags.Default,
            Token = "gsyhdfjisdhgiushdluigjhodfisughouih4e78yf4984otgiuhiuhgoui"
        };
        
        public static UserDocument UserForUpdate => new()
        {
            Id = "validuserid3",
            Login = "Michaelisch'e",
            Name = "Pidor",
            SurName = "Sapogov",
            Password = PasswordHelper.GeneratePassword("string"),
            Role = RoleTags.Default,
            Token = "gsyhdfjisdhgiushdluigjhodfisughouih4e78yf4984otgiuhiuhgoui"
        };
        
        public static UserDocument UserForDelete => new()
        {
            Id = "validuserid4",
            Login = "Michaelisch'e",
            Name = "Pidor",
            SurName = "Sapogov",
            Password = PasswordHelper.GeneratePassword("string"),
            Role = RoleTags.Default,
            Token = "gsyhdfjisdhgiushdluigjhodfisughouih4e78yf4984otgiuhiuhgoui"
        };

        public static IEnumerable<UserDocument> GetAllDocuments()
        {
            return typeof(TestUsers)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(UserDocument))
                .Select(x => x.GetValue(null, null) as UserDocument);
        }
    }
}