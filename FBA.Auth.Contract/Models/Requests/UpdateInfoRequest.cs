using System.ComponentModel.DataAnnotations;

namespace FBA.Auth.Contract.Models.Requests
{
    public record UpdateInfoRequest
    {
        [Required(ErrorMessage = "Введите фамилию")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        public string Patronymic { get; set; }
    }
}