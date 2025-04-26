using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Models.User;



namespace Project_ASP.NET.Controllers
{
    public class UserController(UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var res = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (res.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/");
                }

            }
            ModelState.AddModelError("", "Дані вказано не вірно!");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }

        //[HttpPost]
        //public async Task<IActionResult> SignUp(UserSignUpViewModel model) //Це будь-який web результат - View - сторінка,файл, PDF, Excel
        //{
        //    var existingUser = await context.User
        //      .AnyAsync(x => x.Email == model.Email || x.Phone == model.Phone);

        //    if (existingUser)
        //    {
        //        ModelState.AddModelError("", "Користувач з таким Email або Phone вже існує!");
        //        return View(model);
        //    }

        //    if (model.Password != model.ConfirmPassword)
        //    {
        //        ModelState.AddModelError("ConfirmPassword", "Паролі не співпадають!");
        //        return View(model);
        //    }

        //    var hashedPassword = passwordHasher.HashPassword(null, model.Password);
        //    var userEntity = mapper.Map<UserEntity>(model);
        //    userEntity.Password = hashedPassword;


        //    if (model.Avatar != null)
        //    {
        //        userEntity.AvatarUrl = await imageService.SaveImageAsync(model.Avatar);
        //    }

        //    await context.User.AddAsync(userEntity);
        //    await context.SaveChangesAsync();

        //    return RedirectToAction("Index", "Categories");
        //}

    }
}
