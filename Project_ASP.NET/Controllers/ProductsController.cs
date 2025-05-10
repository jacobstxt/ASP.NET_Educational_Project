using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data;
using Project_ASP.NET.Models.Helpers;
using Project_ASP.NET.Models.Products;

namespace Project_ASP.NET.Controllers
{
    public class ProductsController(ProjectDbContext context,
    IMapper mapper) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index(ProductSearchViewModel searchModel) //Це будь-який web результат - View - сторінка, Файл, PDF, Excel
        {
            ViewBag.Title = "Продукти";

            searchModel = new ProductSearchViewModel();
            searchModel.Categories = await mapper.ProjectTo<SelectItemViewModel>(context.Categories)
                .ToListAsync();

            searchModel.Categories.Insert(0,
            new SelectItemViewModel
            {
                Id = 0,
                Name = "Оберіть категорію"
            });

            var query = context.Products.AsQueryable();

            var model = new ProductListViewModel();
            model.Count = query.Count();

            //Відбір тих елементів , які відображаються на сторінці
            model.Products = mapper.ProjectTo<ProductItemViewModel>(context.Products).ToList();
            model.Search = searchModel;

           
            return View(model);
        }
    }
}
