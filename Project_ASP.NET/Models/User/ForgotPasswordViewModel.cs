using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Models.User
{
    public class ForgotPasswordViewModel
    {
        [Display(Name ="Електронна пошта")]
        [Required(ErrorMessage = "Вкажіть електронну пошту")]
        [EmailAddress(ErrorMessage = "Пошту вказано неправильно")]
        public string Email { get; set; } = String.Empty;
    }
}
