using Microsoft.AspNetCore.Mvc;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

public class CapatazController : Controller
{
    private readonly ILogger<CapatazController> _logger;

    public CapatazController(ILogger<CapatazController> logger)
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

        ViewBag.Potreros = Sistema.Instancia.GetPotreros();

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

    public IActionResult AltaBovino()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        return View();
    }

    [HttpPost]
    public IActionResult AltaBovino(string id, string raza, int sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, string esHibrido, int alimentacion)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        ESexo sexoParsed = sexo == 0 ? ESexo.Macho : ESexo.Hembra;
        EAlimentacion alimentacionParsed = alimentacion == 0 ? EAlimentacion.Pastura : EAlimentacion.Grano;
        bool esHibridoParsed = esHibrido == "on";

        Bovino b = new Bovino(id, raza, sexoParsed, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibridoParsed, alimentacionParsed);

        try
        {
            Sistema.Instancia.AltaAnimal(b);
            ViewBag.msg = $"Bovino {b.ID} dado de alta exitosamente.";
        }
        catch (Exception e)
        {
            ViewBag.Bovino = b;
            ViewBag.msg = e.Message;
        }

        return View();
    }

    public IActionResult BuscarAnimales(string? tipo, double? peso)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Capataz? c = Sistema.Instancia.GetCapatazPorID((int)IDUsuario);
        if (c == null) return Redirect("/");

        ViewBag.Tipo = tipo;
        ViewBag.Peso = peso;

        if (tipo != null && peso != null)
        {
            ViewBag.Animales = Sistema.Instancia.GetAnimalesPorTipoYPeso(tipo, (double)peso);
        }
        else
        {
            ViewBag.Animales = new List<Animal>();
        }

        return View();
    }
}