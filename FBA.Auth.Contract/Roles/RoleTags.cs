using System.Linq;

namespace FBA.Auth.Contract.Roles
{
    public static class RoleTags
    {
        public const string Admin = nameof(Admin);
        public const string Default = nameof(Default);

        public static string[] GetAll()
        {
            return typeof(RoleTags)
                .GetFields()
                .Select(x => x.Name)
                .ToArray();
        }
    }
}