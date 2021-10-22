using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FBA.CrossCutting.Contract.Global
{
    public static class AuthOptions
    {
        public const string Issuer = "FBA"; 
        public const string Audience = "USER";
        const string Key = "lol kek cheburek";   
        public const int Lifetime = 24 * 60; 
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}