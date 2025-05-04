using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ASP.NET.Areas.Admin.Models.Products
{
    public class ProductCreateViewModel
    {
        [Display(Name = "Product name")]
        public string Name { get; set; } = string.Empty;


        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Choose Category")]
        public int CategoryId { get; set; }


        [Display(Name = "Choose Priority")]
        public int Priority { get; set; }


        [Display(Name = "Choose image")]
        [DataType(DataType.ImageUrl)]
        public List<IFormFile> Images { get; set; } = new();
    }
}
