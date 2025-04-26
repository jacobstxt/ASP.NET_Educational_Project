using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Data;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Models.Category;
using Project_ASP.NET.Models.User;


namespace Project_ASP.NET.Controllers
{
    public class UserController(Data.ProjectDbContext context,IMapper mapper,IPasswordHasher<UserEntity> passwordHasher,IImageService imageService):Controller
    {
        public IActionResult Index() 
        {
            var model = mapper.ProjectTo<CategoryItemViewModel>(context.Categories).ToList();
            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel model) //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        {
            var existingUser = await context.User
              .AnyAsync(x => x.Email == model.Email || x.Phone == model.Phone);

            if (existingUser)
            {
                ModelState.AddModelError("", "Користувач з таким Email або Phone вже існує!");
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Паролі не співпадають!");
                return View(model);
            }

            var hashedPassword = passwordHasher.HashPassword(null, model.Password);
            var userEntity = mapper.Map<UserEntity>(model);
            userEntity.Password = hashedPassword;


            if (model.Avatar != null)
            {
                userEntity.AvatarUrl = await imageService.SaveImageAsync(model.Avatar);
            }

            await context.User.AddAsync(userEntity);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Categories");
        }

    }
}
