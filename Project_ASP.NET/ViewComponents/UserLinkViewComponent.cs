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
       public IViewComponentResult Invoke()
        {
            var userName = User.Identity?.Name;
            var model = new UserLinkViewModel();
            if(userName != null)
            {
                var user =  userManager.FindByEmailAsync(userName).Result;
                model = mapper.Map<UserLinkViewModel>(user)
                    ;
            }

            return View(model);
        }

    }
}
