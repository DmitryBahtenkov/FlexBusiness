namespace FBA.Database.Contract.Diagram.Models
{
    public record FieldEmbeddedDocument
    {
        public string Title { get; set; }
        public string DataType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public int Position { get; set; }
    }
}