using System.Threading.Tasks;
using FBA.Database.Contract.Connections.Models.Requests;
using FBA.Database.Contract.Connections.Models.Responses;

namespace FBA.Database.Contract.Connections.Services
{
    public interface IConnectionsService
    {
        public Task<ConnectionResponse> Create(CreateConnectionRequest request);
        public Task<ConnectionResponse> Update(UpdateConnectionRequest request);
        public Task<ConnectionResponse> Delete(string id);
        public Task<ConnectionResponse> Get(string id);
        public Task<GetConnectionsResponse> Get();
    }
}