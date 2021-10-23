using System.ComponentModel.DataAnnotations;

namespace FBA.Auth.Contract.Models.Requests
{
    public record ChangePasswordRequest
    {
        [Required(ErrorMessage = "Введите старый пароль")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Введите новый пароль")]
        public string NewPassword { get; set; }
    }
}