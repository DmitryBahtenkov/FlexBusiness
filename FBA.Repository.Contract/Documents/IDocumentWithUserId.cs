namespace FBA.Repository.Contract.Documents
{
    public interface IDocumentWithUserId : IDocument
    {
        public string UserId { get; set; }
    }
}