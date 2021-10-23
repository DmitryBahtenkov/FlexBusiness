using FBA.Auth.Contract.Models;

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
            Password = new HashedPassword()
            {
                // string
                Salt = "X8RZbNBvfuGbPfHMvZBJvg==",
                Hash = "TRwZMA3N+qlX1ogdi8sgDg=="
            }
        };
    }
}