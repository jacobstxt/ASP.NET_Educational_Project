using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data;
using Project_ASP.NET.DataBase.Entities;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Models.Category;

namespace Project_ASP.NET.Controllers
{
    public class CategoriesController(ProjectDbContext context,IMapper mapper,IImageService imageService) : Controller
    {

        public IActionResult Index() //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            var model = mapper.ProjectTo<CategoryItemViewModel>(context.Categories).ToList();
            return View(model);
        }


        [HttpGet]
        public IActionResult Create() //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model) //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            var entity = await context.Categories.SingleOrDefaultAsync(x => x.Name == model.Name);

            if (entity != null)
            {
                ModelState.AddModelError("Name", "Така категорія уже є!!!");
                return View(model);
            }

       
            entity = mapper.Map<CategoryEntity>(model);
            entity.ImageUrl = await imageService.SaveImageAsync(model.ImageFile);
            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(category.ImageUrl))
            {
                await imageService.DeleteImageAsync(category.ImageUrl);
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
