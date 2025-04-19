using System.ComponentModel.DataAnnotations;

namespace Project_ASP.NET.Models.Category
{
    public class CategoryEditViewModel
    {
        public int Id {  get; set; }
        [Required(ErrorMessage="Некоректна назва")]
        [Display(Name = "Category name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Description")]
        public string? Description { get; set; } = string.Empty;

        public string? ViewImage { get; set; } = string.Empty;
        [Display(Name = "Choose image")]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImageFile { get; set; } = null!;
    }
}
