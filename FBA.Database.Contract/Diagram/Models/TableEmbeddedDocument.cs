using System.Collections.Generic;

namespace FBA.Database.Contract.Diagram.Models
{
    public record TableEmbeddedDocument
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public List<ReferenceEmbeddedDocument> References { get; set; }
        public List<FieldEmbeddedDocument> Fields { get; set; }
    }
}