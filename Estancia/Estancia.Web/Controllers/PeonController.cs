using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Estancia.Web.Models;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

public class PeonController : Controller
{
    private readonly ILogger<PeonController> _logger;

    public PeonController(ILogger<PeonController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        return View();
    }

    public IActionResult MiPerfil()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        ViewBag.Peon = p;

        return View();
    }

    public IActionResult MisTareas()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        ViewBag.IDPeon = p.ID;
        ViewBag.NombrePeon = p.Nombre;
        ViewBag.TareasPendientes = p.GetTareasPendientes();

        return View();
    }

    public IActionResult CompletarTarea(int id)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");
        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        Tarea t = p.GetTareaPorID(id, false);

        if (t == null) return RedirectToAction("MisTareas");

        return View(t);
    }

    [HttpPost]
    public IActionResult CompletarTarea(Tarea t)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        Tarea? tarea = p.GetTareaPorID(t.ID, false);
        if (tarea == null) return Redirect("/");

        try
        {
            tarea.Completar(t.Comentario);
            return RedirectToAction("MisTareas");
        }
        catch (Exception e)
        {
            ViewBag.msg = e.Message;
            return View(tarea);
        }
    }

    public IActionResult VacunarAnimales()
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        ViewBag.Vacunas = Sistema.Instancia.GetVacunas();
        ViewBag.Animales = Sistema.Instancia.GetAnimales();
        return View();
    }

    [HttpPost]
    public IActionResult VacunarAnimales(string id, string NombreVacuna, DateTime FechaVacuna)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        Animal? a = Sistema.Instancia.GetAnimalPorID(id);
        if (a == null) return Redirect("/");

        Vacuna? v = Sistema.Instancia.GetVacunaPorNombre(NombreVacuna);
        if (v == null) return Redirect("/");

        try
        {
            a.Vacunar(v, FechaVacuna);
            ViewBag.msg = $"Animal #{a.ID} Vacunado con la vacuna '{v.Nombre}' exitosamente.";
        }
        catch (Exception e)
        {
            ViewBag.msg = e.Message;
        }

        ViewBag.Vacunas = Sistema.Instancia.GetVacunas();
        ViewBag.Animales = Sistema.Instancia.GetAnimales();

        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
