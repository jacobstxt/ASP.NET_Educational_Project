using Microsoft.AspNetCore.Mvc;

namespace Project_ASP.NET.Controllers
{
    public class UserController:Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }

    }
}
