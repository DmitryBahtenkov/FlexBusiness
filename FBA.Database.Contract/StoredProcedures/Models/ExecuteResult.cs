using System.Collections.Generic;

namespace FBA.Database.Contract.StoredProcedures.Models
{
    public record ExecuteResult(IEnumerable<string> Headers, IEnumerable<object[]> Values);
}