using System.Threading.Tasks;
using FBA.Database.Builders;
using FBA.Database.Contract;
using FBA.Database.Contract.Builders;
using Xunit;

namespace FBA.Tests.ConnectionsTests
{
    public class BuilderFactoryTests
    {
        private readonly IConnectionStingBuilderFactory _connectionStingBuilderFactory;

        public BuilderFactoryTests(IConnectionStingBuilderFactory connectionStingBuilderFactory)
        {
            _connectionStingBuilderFactory = connectionStingBuilderFactory;
        }

        [Theory(DisplayName = "Проверка на корректный тип строителя строк для MS SQL")]
        [InlineData(DbType.MsSql)]
        public void GetMsSqlBuilderTest(DbType type)
        {
            var ms = _connectionStingBuilderFactory.GetBuilder(type);
            
            Assert.IsType<MsConnectionStringBuilder>(ms);
        }
    }
}