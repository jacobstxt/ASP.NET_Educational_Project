using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Project_ASP.NET.Models.Category
{
    public class CategoryCreateViewModel
    {
        [Display(Name = "Назва категорії")]
        public string Name {  get; set; } = string.Empty;
        [Display(Name = "Опис")]
        public string? Description { get; set; } = string.Empty;
        [Display(Name = "URL зображення")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
