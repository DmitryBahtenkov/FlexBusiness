using System.ComponentModel.DataAnnotations;
using FBA.CrossCutting.Contract.Attributes;

namespace FBA.Database.Contract.Connections.Models.Requests
{
    public record UpdateConnectionRequest
    {
        [Required(ErrorMessage = "Название для подключения обязательно")]
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Host { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Port { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Login { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Password { get; set; }
        [RequiredWhen(nameof(ConnectionString), null)]
        public string Database { get; set; }
    }
}