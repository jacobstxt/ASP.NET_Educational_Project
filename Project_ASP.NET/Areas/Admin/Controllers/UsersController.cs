using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Areas.Admin.Models.Users;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Interfaces;
using Project_ASP.NET.Services;

namespace Project_ASP.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController(UserManager<UserEntity> userManager,SignInManager<UserEntity> signInManager,IMapper mapper, IImageService imageService) :Controller
    {
        public  async Task<IActionResult> Index()
        {
            var model = await userManager.Users
                .ProjectTo<UserItemViewModel>(mapper.ConfigurationProvider).ToListAsync();


            foreach (var item in model)
            {
                var user = await userManager.FindByIdAsync(item.Id);
                var roles = await userManager.GetRolesAsync(user);
                item.Role =  roles.FirstOrDefault(); 
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var model = new UserEditViewModel
            {
                Id = id,
                FirstName = user.Name,
                LastName = user.Surname,
                Email = user.Email,
                Image = user.AvatarUrl,             
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id.ToString());
                if (user == null)
                {
                    return NotFound();
                }

        
                user.Name = model.FirstName;
                user.Surname = model.LastName;
                user.Email = model.Email;


                if (user.Email != model.Email)
                {
                    var res = await userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {

                        await signInManager.SignInAsync(user, isPersistent: false);
                    }
                }



                if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                {
                    var passwordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
                    user.PasswordHash = passwordHash;
                }


                if (model.Avatar != null && model.Avatar.Length > 0)
                {
                    if (!string.IsNullOrEmpty(user.AvatarUrl))
                    {
                        var oldImageName = Path.GetFileName(user.AvatarUrl);
                        await imageService.DeleteImageAsync(oldImageName); 
                    }

                    var imageName = await imageService.SaveImageAsync(model.Avatar);
                    user.AvatarUrl = imageName;

                }

          

           
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model); 
        }





    }
}
