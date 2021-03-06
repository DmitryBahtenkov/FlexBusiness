using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FBA.CrossCutting.Contract.Attributes;

namespace FBA.Database.Contract.Connections.Models.Requests
{
    public record CreateConnectionRequest
    {
        [Required(ErrorMessage = "Название для подключения обязательно")]
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Host { get; set; }
        public string Port { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Login { get; set; }
        public string Password { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Database { get; set; }
        [Required(ErrorMessage = "Укажите СУБД")]
        public DbType DbType { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new();
    }
}