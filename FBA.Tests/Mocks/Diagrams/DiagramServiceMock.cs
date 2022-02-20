using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.Diagram.Models;
using FBA.Database.Contract.Diagram.Services;

namespace FBA.Tests.Mocks.Diagrams
{
    public class DiagramServiceMock : IDiagramService
    {
        public Task<DiagramDocument> CreateFromConnection(string connectionId)
        {
            return Task.FromResult(new DiagramDocument());
        }

        public Task<DiagramDocument> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DiagramDocument> GetByConnection(string connectionId)
        {
            throw new NotImplementedException();
        }
    }
}