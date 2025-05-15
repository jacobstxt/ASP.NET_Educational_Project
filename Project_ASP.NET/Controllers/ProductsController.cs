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


            searchModel.Categories = await mapper.ProjectTo<SelectItemViewModel>(context.Categories)
                .ToListAsync();

            searchModel.Categories.Insert(0,
            new SelectItemViewModel
            {
                Id = 0,
                Name = "Оберіть категорію"
            });

            var query = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                string textSearch = searchModel.Name.Trim();
                query = query.Where(x=> x.Name.ToLower().Contains(textSearch.ToLower()));
            }

            if(searchModel.CategoryId != 0)
            {
                query = query.Where(p => p.CategoryId == searchModel.CategoryId);
            }

            int totalItems = await query.CountAsync();

            var items = await mapper.ProjectTo<ProductItemViewModel>(query
                .Skip((searchModel.Page - 1) * searchModel.PageSize)
                .Take(searchModel.PageSize))
                .ToListAsync();

            //var model = new ProductListViewModel();
            //model.Count = query.Count();

            ////Відбір тих елементів , які відображаються на сторінці
            //model.Products = mapper.ProjectTo<ProductItemViewModel>(query).ToList();
            //model.Search = searchModel;

            var model = new ProductListViewModel
            {
                Search = searchModel,
                Page = new PaginationViewModel<ProductItemViewModel>
                {
                    Items = items,
                    TotalItems = totalItems,
                    Page = searchModel.Page,
                    PageSize = searchModel.PageSize
                }
            };


            return View(model);
        }


    }
}
