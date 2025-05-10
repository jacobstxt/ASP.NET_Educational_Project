using System.ComponentModel.DataAnnotations;
using Project_ASP.NET.Models.Helpers;

namespace Project_ASP.NET.Models.Products
{
    public class ProductSearchViewModel
    {
        [Display(Name = "Назва")]
        public string Name { get; set; }= String.Empty;
        [Display(Name = "Опис")]
        public string Description { get; set; } = String.Empty;

        [Display(Name = "Категорія")]
        public  int CategoryId { get; set; }

        public List<SelectItemViewModel> Categories { get; set; } = new();

    }
}
