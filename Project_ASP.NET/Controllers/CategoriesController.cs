using Microsoft.AspNetCore.Mvc;
using Project_ASP.NET.Models.Category;

namespace Project_ASP.NET.Controllers
{
    public class CategoriesController : Controller
    {
        List<CategoryItemViewModel> categories = new List<CategoryItemViewModel>
        {
            new CategoryItemViewModel
            {
                Id = 1,
                Name = "Пригодницькі",
                Description = "Мультфільми, сповнені захопливих пригод та подорожей.",
                Image = "https://image.shutterstock.com/image-vector/cartoon-pirate-adventure-260nw-123456789.jpg"
            },
            new CategoryItemViewModel
            {
                Id = 2,
                Name = "Комедійні",
                Description = "Мультфільми, що піднімуть настрій та розсмішать.",
                Image = "https://image.shutterstock.com/image-vector/funny-cartoon-character-260nw-987654321.jpg"
            },
            new CategoryItemViewModel
            {
                Id = 3,
                Name = "Фентезі",
                Description = "Магічні світи та чарівні істоти чекають на вас.",
                Image = "https://image.shutterstock.com/image-vector/fantasy-world-castle-260nw-192837465.jpg"
            },
            new CategoryItemViewModel
            {
                Id = 4,
                Name = "Наукова фантастика",
                Description = "Подорожі в космос та футуристичні технології.",
                Image = "https://image.shutterstock.com/image-vector/space-adventure-cartoon-260nw-564738291.jpg"
            },
            new CategoryItemViewModel
            {
                Id = 5,
                Name = "Класика Disney",
                Description = "Найкращі класичні мультфільми від студії Disney.",
                Image = "https://image.shutterstock.com/image-vector/disney-classic-cartoon-260nw-374829374.jpg"
            }
        };


        public IActionResult Index() //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create() //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel model) //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
           
            return View(model);
        }



    }
}
