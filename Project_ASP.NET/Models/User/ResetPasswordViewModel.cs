using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Models.User
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; } = string.Empty;

        [Display(Name ="Новий пароль")]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль має містити щонайменше 6 символів")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Підтвердіть пароль")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
