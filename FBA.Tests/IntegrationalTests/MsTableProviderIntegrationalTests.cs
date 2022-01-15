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

        [Fact]
        public async Task GetTablesFromDemEkzamenTest()
        {
            GivenTheConnection();
            await WhenTablesAreReceived();
            ThenAllTablesExists();
        }

        private void ThenAllTablesExists()
        {

        }

        private async Task WhenTablesAreReceived()
        {
            Tables = await _tableProviderFactory.GetProvider(Database.Contract.DbType.MsSql).GetTables(Connection);
        }

        private void GivenTheConnection()
        {
            Connection = new ConnectionsDocument
            {
                ConnectionString = "Server=DESKTOP-SVMQI20;Database=PaperDE;Trusted_Connection=True;"
            };
        }
    }
}
