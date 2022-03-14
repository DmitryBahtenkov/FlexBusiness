using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBA.Database.Contract.StoredProcedures.Models;

namespace FBA.Tests.ProcedureTests
{
    public static class TestProcedures
    {
        public static StoredProcedureDocument Procedure => new StoredProcedureDocument()
        {
            Id = "testprocedure",
            Name = "test",
            Title = "тест",
            Parameters = new [] 
            {
                new ParameterInfoEmbeddedDocument()
                {
                    Name = "@test",
                    Title = "test"
                }
            }
        };
    }
}