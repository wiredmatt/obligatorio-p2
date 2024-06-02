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

    [HttpPost]
    [ActionName("AsignarTareaAPeon")]
    public IActionResult AsignarTareaAPeon(int id, string descripcion, DateTime fechaInicio, DateTime fechaLimite)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID(id);
        if (p == null) return RedirectToAction("Peones");

        Tarea t = new Tarea(descripcion, fechaInicio, fechaLimite, c);

        try
        {
            p.AltaTarea(t);
            ViewBag.msg = $"Tarea #{t.ID} asignada exitosamente a Peon {p.Nombre} (#{p.ID})";
        }
        catch (Exception e)
        {
            ViewBag.msg = e.Message;
        }

        ViewBag.IDPeon = p.ID;
        ViewBag.NombrePeon = p.Nombre;

        return View();
    }

    public IActionResult Potreros()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        IEnumerable<Potrero> Potreros = Sistema.Instancia.GetPotreros();
        ViewBag.Potreros = Potreros;

        return View();
    }

    public IActionResult AsignarAnimales()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        ViewBag.Animales = Sistema.Instancia.GetAnimalesLibres();
        ViewBag.Potreros = Sistema.Instancia.GetPotreros();

        return View();
    }

    [HttpPost]
    public IActionResult AsignarAnimales(string idAnimal, int idPotrero)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        Animal? a = Sistema.Instancia.GetAnimalPorID(idAnimal);
        if (a == null) return RedirectToAction("AsignarAnimales");
        Potrero? p = Sistema.Instancia.GetPotreroPorID(idPotrero);
        if (p == null) return RedirectToAction("AsignarAnimales");

        try
        {
            p.AgregarAnimal(a);
            ViewBag.msg = $"Animal #{a.ID} Asignado a Potrero #{p.ID} exitosamente.";
        }
        catch (Exception e)
        {
            ViewBag.msg = e.Message;
        }

        ViewBag.Animales = Sistema.Instancia.GetAnimalesLibres();
        ViewBag.Potreros = Sistema.Instancia.GetPotreros();
        return View();
    }
}