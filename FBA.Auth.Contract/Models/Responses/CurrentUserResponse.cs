namespace FBA.Auth.Contract.Models.Responses
{
    public class CurrentUserResponse
    {
        public string Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
    }
}