using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Models.User;
using Project_ASP.NET.SMTP;



namespace Project_ASP.NET.Controllers
{
    public class UserController(UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager, IImageService imageService, IMapper mapper,
        ISMTPService SMTPService) : Controller
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

        public IActionResult Index()
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
                    return Redirect("Categories/Index");
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

        [HttpPost]
        public  async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Користувача по даній пошті не існує");
                return View(model);
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);


            var resetUrl = Url.Action(
              "ResetPassword",                  // Назва дії
              "User",                        // Назва контролера
               new { email = user.Email, token },// Параметри GET
               protocol: Request.Scheme);       // http або https



            MessageModel msgEmail = new MessageModel
                {
                    Body = $"Для скидання паролю перейдіть за посиланням: <a href='{resetUrl}'>Скинути пароль</a>",
                    Subject = $"Скидання паролю",
                    To = model.Email
              };

            var result = await SMTPService.SendEmailAsync(msgEmail);

            if (!result)
            {
                ModelState.AddModelError("", "Помилка надсилання листа. Зверніться у підтримку");
                return View(model);
            }
            return RedirectToAction(nameof(ForgotPasswordSend));
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //view з повідомленням що лист прийшов
        [HttpGet]
        public IActionResult ForgotPasswordSend()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email,string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            return View();
        }
    }
}
