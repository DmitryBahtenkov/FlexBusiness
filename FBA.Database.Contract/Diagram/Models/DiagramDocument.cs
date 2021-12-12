using System.Collections.Generic;
using FBA.Repository.Contract.Documents;

namespace FBA.Database.Contract.Diagram.Models
{
    public record DiagramDocument : IDocumentWithUserId
    {
        public string Id { get; set; }
        public bool IsArchived { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public List<TableEmbeddedDocument> Tables { get; set; }
        public string UserId { get; set; }
    }
}