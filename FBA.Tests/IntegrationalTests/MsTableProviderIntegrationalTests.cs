using System.Collections.Generic;
using System;
using FBA.Database.Contract.Connections.Models;
using FBA.Database.Contract.Diagram.Services;
using FBA.Database.Contract.Diagram.Models;
using System.Threading.Tasks;
using Xunit;
using System.Net;

namespace FBA.Tests.IntegrationalTests
{
    public class MsTableProviderIntegrationalTests
    {
		private readonly ITableProviderFactory _tableProviderFactory;

        private ConnectionsDocument Connection;
        private List<TableEmbeddedDocument> Tables;


		public MsTableProviderIntegrationalTests(ITableProviderFactory tableProviderFactory)
		{
			_tableProviderFactory = tableProviderFactory;
		}

        [Fact(Skip = "Необходима бд ms sql с демэкзамена, развёрнутая локально со стандартной конфигурацией")]
        //[Fact]
        public async Task GetTablesFromDemEkzamenTest()
        {
            GivenTheConnection();
            await WhenTablesAreReceived();
            ThenAllTablesExists();
        }

        private void ThenAllTablesExists()
        {
            Assert.Equal(5, Tables.Count);
            
            Assert.Contains(Tables, x => x.Title is "History");
            Assert.Contains(Tables, x => x.Title is "Material");
            Assert.Contains(Tables, x => x.Title is "Supplier");
            Assert.Contains(Tables, x => x.Title is "MaterialSupplier");
        }

        private async Task WhenTablesAreReceived()
        {
            Tables = await _tableProviderFactory.GetProvider(Database.Contract.DbType.MsSql).GetTables(Connection);
        }

        private void GivenTheConnection()
        {
            Connection = new ConnectionsDocument
            {
                ConnectionString = "Server=localhost; Database=PaperDE; User Id=Userok; Password=Userok; Trusted_Connection=Yes; TrustServerCertificate=True"
            };
        }
    }
}
