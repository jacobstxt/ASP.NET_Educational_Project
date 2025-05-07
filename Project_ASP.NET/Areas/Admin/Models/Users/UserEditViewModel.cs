using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Areas.Admin.Models.Users
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string? Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string? ConfirmPassword { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? Avatar { get; set; }

        public string? Image { get; internal set; }
    }
}
