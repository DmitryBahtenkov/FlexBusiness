using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FBA.Auth.Contract.Models;

namespace FBA.Auth.Contract.Services
{
    public interface ICurrentUserService
    {
        public UserDocument CurrentUser { get; }

        [assembly:InternalsVisibleTo("FBA.Tests")]
        internal void Init(UserDocument document);
    }
}