using System.Threading.Tasks;

namespace FBA.Database.Contract.Builders
{
    public interface IConnectionStingBuilderFactory
    {
        public IConnectionStringBuilder GetBuilder(DbType type);
    }
}