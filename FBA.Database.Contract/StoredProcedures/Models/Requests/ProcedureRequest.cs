namespace FBA.Database.Contract.StoredProcedures.Models.Requests
{
    public record ProcedureRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Direction { get; set; }
    }
}