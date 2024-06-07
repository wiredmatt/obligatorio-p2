using Microsoft.AspNetCore.Mvc;

namespace Estancia.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string? RolUsuario = HttpContext.Session.GetString("RolUsuario");
        if (RolUsuario == null) return View();

        if (RolUsuario == "Capataz") return Redirect("/Capataz/Index");
        else return Redirect("/Peon/Index");
    }

    public IActionResult EasterEgg()
    {
        return Redirect("https://www.youtube.com/watch?v=-HB5XU18IsU");
    }
}