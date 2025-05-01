
using Microsoft.AspNetCore.Mvc;


namespace Project_ASP.NET.Areas.Admin.Controllers;

public class FormLayoutsController : Controller
{
public IActionResult Horizontal() => View();
public IActionResult Vertical() => View();
}
