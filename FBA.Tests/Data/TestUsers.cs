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
    }
}