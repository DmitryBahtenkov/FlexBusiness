using System.Collections.Generic;

namespace FBA.Database.Contract.StoredProcedures.Models
{
    public record ExecuteResult(List<string> Headers, IEnumerable<object[]> Values);
}