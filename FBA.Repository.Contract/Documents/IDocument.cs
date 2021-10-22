namespace FBA.Repository.Contract.Documents
{
    public interface IDocument
    {
        public string Id { get; set; }
        public bool IsArchived { get; set; }
    }
}