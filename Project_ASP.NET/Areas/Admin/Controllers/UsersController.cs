using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ASP.NET.Areas.Admin.Models.Users;
using Project_ASP.NET.Data.Entities.Identity;

namespace Project_ASP.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController(UserManager<UserEntity> userManager,IMapper mapper):Controller
    {
        public  async Task<IActionResult> Index()
        {
            var model = await userManager.Users
                .ProjectTo<UserItemViewModel>(mapper.ConfigurationProvider).ToListAsync();

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
                ConfirmPassword = user.PasswordHash,
            };

            return View(model);
        }


    }
}
