using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Models.User;



namespace Project_ASP.NET.Controllers
{
    public class UserController(UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager, IImageService imageService, IMapper mapper) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            var model = new UserProfileViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                AvatarUrl = user.AvatarUrl,
                Email = user.Email
            };

            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = mapper.Map<UserEntity>(model);
            user.AvatarUrl = await imageService.SaveImageAsync(model.Avatar) ?? null;


            var res = await userManager.CreateAsync(user,model.Password);
            if (res.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Redirect("/");
            }
           

           foreach(var error in res.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


           return View(model);
          
        }

    }
}
