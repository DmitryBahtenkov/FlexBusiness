namespace FBA.Database.Contract.Connections.Models.Responses
{
    public record ConnectionResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Login { get; set; }
        public string Database { get; set; }
        public DbType Type { get; set; }
    }
}