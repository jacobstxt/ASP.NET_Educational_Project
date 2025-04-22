using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Models.User
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Прізвище обов'язкове")]
        public string Surname { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Некоректний формат телефону")]
        public string Phone { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль має містити щонайменше 6 символів")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = null!;

        public IFormFile? Avatar { get; set; }

    }
}
