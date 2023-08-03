using ServiceExtender.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Administration.Host.Models.Account
{
    public class LoginViewModel
    {
        [QSEmailValidation(EmptyErrorMessage = "Укажите email", Required = true, ErrorMessage = "Некорректный email адрес")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Укажите пароль")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
