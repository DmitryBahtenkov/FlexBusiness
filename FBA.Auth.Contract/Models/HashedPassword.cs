namespace FBA.Auth.Contract.Models
{
    public record HashedPassword
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}