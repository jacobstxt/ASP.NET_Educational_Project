
using Microsoft.AspNetCore.Mvc;


namespace Project_ASP.NET.Areas.Admin.Controllers;

public class ExtendedUiController : Controller
{
  public IActionResult PerfectScrollbar() => View();
  public IActionResult TextDivider() => View();
}
