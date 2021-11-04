using System.Collections.Generic;

namespace FBA.Database.Contract.Connections.Models.Responses
{
    public class GetConnectionsResponse
    {
        public List<ConnectionResponse> Connections { get; set; }
    }
}