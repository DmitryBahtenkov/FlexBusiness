using System.ComponentModel.DataAnnotations;
using FBA.CrossCutting.Contract.Attributes;

namespace FBA.Auth.Contract.Models.Requests
{
    public record CreateUserRequest
    {
        [Required(ErrorMessage = "Введите фамилию")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [RequiredWhen(nameof(IsNewUser), false)]
        public string Password { get; set; }
        public bool IsNewUser { get; set; }
    }
}