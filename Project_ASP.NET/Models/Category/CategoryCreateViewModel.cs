using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.Metadata;

namespace Project_ASP.NET.Models.Category
{
    public class CategoryCreateViewModel
    {
        [Display(Name = "Category name")]
        public string Name {  get; set; } = string.Empty;
        [Display(Name = "Description")]
        public string? Description { get; set; } = string.Empty;
        [Display(Name = "Choose image")]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImageFile { get; set; } = null!;
    }
}
