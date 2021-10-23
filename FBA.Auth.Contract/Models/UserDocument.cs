using FBA.Repository.Contract.Documents;

namespace FBA.Auth.Contract.Models
{
    public record UserDocument : IDocument
    {
        public string Id { get; set; }
        public bool IsArchived { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public HashedPassword Password { get; set; }
        public string Token { get; set; }
        /// <summary>
        /// При входе установить пароль
        /// </summary>
        public bool IsNewUser { get; set; }
    }
}