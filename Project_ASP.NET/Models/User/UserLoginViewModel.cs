using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Models.User
{
    public class UserLoginViewModel
    {
        [Display(Name = "Електронна пошта")]
        [Required(ErrorMessage = "Вкажіть електронну пошту")]
        [EmailAddress(ErrorMessage = "Пошту вказано не вірно")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Вкажіть пароль")]
        public string Password { get; set; } = string.Empty;
    }
}
