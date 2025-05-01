
using Microsoft.AspNetCore.Mvc;


namespace Project_ASP.NET.Areas.Admin.Controllers;

public class FormsController : Controller
{
  public IActionResult BasicInputs() => View();
  public IActionResult InputGroups() => View();
}
