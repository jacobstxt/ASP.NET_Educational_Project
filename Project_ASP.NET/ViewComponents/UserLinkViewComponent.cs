using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Models.User;

namespace Project_ASP.NET.ViewComponents
{
    public class UserLinkViewComponent(UserManager<UserEntity> userManager,
        IMapper mapper) : 
        ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = User.Identity?.Name;
            var model = new UserLinkViewModel();

            if (!string.IsNullOrEmpty(userName))
            {
                var user = await userManager.FindByEmailAsync(userName);
                if (user != null)
                {
                    model = mapper.Map<UserLinkViewModel>(user);
                }
            }

            return View(model);
        }

    }
}
