using FBA.Repository.Contract.Documents;

namespace FBA.Database.Contract.Connections.Models
{
    public class ConnectionsDocument : IDocumentWithUserId
    {
        public string Id { get; set; }
        public bool IsArchived { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Строка подключения к бд
        /// </summary>
        public string ConnectionString { get; set; }
        public ConnectionInfo ConnectionInfo { get; set; }
        public DbType DbType { get; set; }
    }
}