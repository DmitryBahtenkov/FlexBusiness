using System;
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
        [InlineData(DbType.MsSql, typeof(MsConnectionStringBuilder))]
        public void GetMsSqlBuilderTest(DbType type, Type expectedType)
        {
            var ms = _connectionStingBuilderFactory.GetBuilder(type);
            
            Assert.IsType(expectedType, ms);
        }
    }
}