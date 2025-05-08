using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Models.User
{
    public class ForgotPasswordViewModel
    {
        [Display(Name ="Електронна пошта")]
        [EmailAddress(ErrorMessage = "Пошту вказано неправильно")]
        public string Email { get; set; } = String.Empty;
    }
}
