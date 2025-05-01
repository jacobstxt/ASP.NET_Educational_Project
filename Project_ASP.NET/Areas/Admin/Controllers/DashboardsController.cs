
using Microsoft.AspNetCore.Mvc;


namespace Project_ASP.NET.Areas.Admin.Controllers;

public class DashboardsController : Controller
{
  public IActionResult Index() => View();
}
