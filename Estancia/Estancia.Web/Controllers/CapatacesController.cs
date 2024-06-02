using Microsoft.AspNetCore.Mvc;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

public class CapatacesController : Controller
{
    private readonly ILogger<CapatacesController> _logger;

    public CapatacesController(ILogger<CapatacesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        return View();
    }

    public IActionResult Peones()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        ViewBag.Peones = Sistema.Instancia.GetPeones();

        return View();
    }

    public IActionResult TareasDePeon(int id)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID(id);

        if (p == null) return RedirectToAction("Peones");

        ViewBag.IDPeon = p.ID;
        ViewBag.NombrePeon = p.Nombre;
        ViewBag.Tareas = p.GetTareas();

        return View();
    }

    [HttpGet]
    public IActionResult AsignarTareaAPeon(int id)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID(id);
        if (p == null) return RedirectToAction("Peones");

        ViewBag.IDPeon = p.ID;
        ViewBag.NombrePeon = p.Nombre;

        return View();
    }

    // [HttpPost]
    // [ActionName("AsignarTareaAPeon")]
    // public IActionResult AsignarTareaAPeonPost(int id)
    // {
    //     int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
    //     if (IDUsuario == null) return Redirect("/");
    //     Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
    //     if (c == null) return Redirect("/");

    //     Peon? p = Sistema.Instancia.GetPeonPorID(id);
    //     if (p == null) return RedirectToAction("Peones");

    //     // ... codigo para asignar tarea
    // }
}