using FBA.Repository.Contract.Documents;

namespace FBA.Database.Contract.StoredProcedures.Models
{
    public class StoredProcedureDocument : IDocument
    {
        public string Id { get; set; }
        public bool IsArchived { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ConnectionId { get; set; }
        public string Direction { get; set; }

        public ParameterInfoEmbeddedDocument[] Parameters { get; set; }
    }
}