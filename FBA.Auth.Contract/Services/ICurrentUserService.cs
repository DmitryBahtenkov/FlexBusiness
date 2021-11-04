using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FBA.Auth.Contract.Models;

namespace FBA.Auth.Contract.Services
{
    public interface ICurrentUserService
    {

        public UserDocument Get();
    }
}