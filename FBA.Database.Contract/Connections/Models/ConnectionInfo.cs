namespace FBA.Database.Contract.Connections.Models
{
    public record ConnectionInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}