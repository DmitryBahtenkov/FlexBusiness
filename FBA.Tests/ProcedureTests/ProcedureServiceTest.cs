using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;
using FBA.Database.Contract.StoredProcedures.Models.Requests;
using FBA.Database.Contract.StoredProcedures.Services;
using FBA.Tests.Data;
using Xunit;

namespace FBA.Tests.ProcedureTests
{
    public class ProcedureServiceTest
    {
		private readonly IStoredProcedureService _storedProcedureService;

		public ProcedureServiceTest(IStoredProcedureService storedProcedureService)
		{
			_storedProcedureService = storedProcedureService;
		}

        [Fact]
        public async Task CreateValidProcedureTest()
        {
            var request = new ProcedureRequest
            {
                Name = "Name",
                Title = "Title"
            };

            var doc = await _storedProcedureService.Create(TestConnections.ConnectionForUpdate.Id, request);

            Assert.NotNull(doc);
            Assert.Equal(request.Name, doc.Name);
            Assert.Equal(request.Title, doc.Title);
        }
    }
}