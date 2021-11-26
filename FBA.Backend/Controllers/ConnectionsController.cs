using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models.Requests;
using FBA.Database.Contract.Connections.Models.Responses;
using FBA.Database.Contract.Connections.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBA.Backend.Controllers
{
    [Route("api/v1/connections")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly IConnectionsService _connectionsService;

        public ConnectionsController(IConnectionsService connectionsService)
        {
            _connectionsService = connectionsService;
        }

        [HttpGet]
        public async Task<GetConnectionsResponse> Get()
            => await _connectionsService.Get();
        
        [HttpGet("{id}")]
        public async Task<ConnectionResponse> Get(string id)
            => await _connectionsService.Get(id);

        [HttpPost]
        public async Task<ConnectionResponse> Create([FromBody] CreateConnectionRequest request)
            => await _connectionsService.Create(request);

        [HttpPut("{id}")]
        public async Task<ConnectionResponse> Update(string id, [FromBody] UpdateConnectionRequest request)
            => await _connectionsService.Update(id, request);
        
        [HttpDelete("{id}")]
        public async Task<ConnectionResponse> Update(string id)
            => await _connectionsService.Delete(id);
    }
}