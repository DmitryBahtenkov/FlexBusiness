using System.Data;
using System.Threading.Tasks;
using System;
using FBA.Database.Contract.Diagram.Services;
using Xunit;
using FBA.Database.Contract;
using FBA.Database.Diagram.Services;

namespace FBA.Tests.DatabaseTests
{
    public class TableProviderFactoryTests
    {
		private readonly ITableProviderFactory _tableProviderFactory;

		public TableProviderFactoryTests(ITableProviderFactory tableProviderFactory)
		{
			_tableProviderFactory = tableProviderFactory;
		}

        [InlineData(Database.Contract.DbType.MsSql, typeof(MsSqlTableProvider))]
        [Theory(DisplayName = "Проверка на корректный тип провайдера таблиц MS SQL")]
        public void FactoryTest(Database.Contract.DbType type, Type expectedType)
        {
            var implementation = _tableProviderFactory.GetProvider(type);
            Assert.IsType(expectedType, implementation);
        }
    }
}
