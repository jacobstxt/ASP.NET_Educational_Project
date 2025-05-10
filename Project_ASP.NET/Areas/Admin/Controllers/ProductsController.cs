using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Areas.Admin.Models.Products;
using Project_ASP.NET.Data;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Models.Category;



namespace Project_ASP.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController(ProjectDbContext context, IMapper mapper, IImageService imageService):Controller
    {

        public IActionResult Index()
        {
            var model = mapper.ProjectTo<ProductItemViewModel>(context.Products).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync() 
        {
            var categories = await context.Categories.ToListAsync();

            var model = new ProductCreateViewModel{ };

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model) //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {

            if (ModelState.IsValid)
            {

                var exists = await context.Products.AnyAsync(x => x.Name == model.Name);
                if (exists)
                {
                    ModelState.AddModelError("Name", "Такий продукт уже існує!");
                    return View(model);
                }

                var entity = mapper.Map<ProductEntity>(model);

                if (model.Images != null && model.Images.Any())
                {
                    entity.ProductImages = await imageService.SaveImagesAsync(model.Images);
                }


                await context.Products.AddAsync(entity);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var categories = await context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(model);
        }


        [HttpPost]
        [Route("product/images")]
        public async Task<IActionResult> UploadImage(IFormFile file, IImageService imageService)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var fileName = await imageService.SaveImageAsync(file);
            var url = $"{Request.Scheme}://{Request.Host}/images/800_{fileName}"; 

            return Json(new { location = url }); // location — це те, що очікує TinyMCE
        }


    }
}
