using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.NET.Data;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Models.Product;

namespace Project_ASP.NET.Controllers
{
    public class ProductsController(ProjectDbContext context,IMapper mapper,IImageService imageService) : Controller
    {
        public IActionResult Index() //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            var model = mapper.ProjectTo<ProductItemViewModel>(context.Products).ToList();
            return View(model);
        }

       


    }
}
