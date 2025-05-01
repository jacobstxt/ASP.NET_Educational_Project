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
    }
}
