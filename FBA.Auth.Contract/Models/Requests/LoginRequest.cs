using System.ComponentModel.DataAnnotations;

namespace FBA.Auth.Contract.Models.Requests
{
    public record LoginRequest
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        //todo: доработать сервис для обработки новых пользователей
        public bool IsNew { get; set; }
    }
}