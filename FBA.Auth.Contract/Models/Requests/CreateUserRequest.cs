namespace FBA.Auth.Contract.Models.Requests
{
    public record CreateUserRequest
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool ShouldGeneratePassword { get; set; }
    }
}