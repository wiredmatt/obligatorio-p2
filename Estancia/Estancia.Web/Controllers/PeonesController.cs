using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Estancia.Web.Models;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

// TODO: Implementar RBAC: a la ruta /peones/detalle/1 solo puede acceder el peon que tenga ID 1
public class PeonesController : Controller
{
    private readonly ILogger<PeonesController> _logger;

    public PeonesController(ILogger<PeonesController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {
    //     List<Peon> peones = Sistema.Instancia.GetPeones();

    //     ViewBag.Peones = peones;

    //     return View();
    // }

    public IActionResult Detalle(int id)
    {
        Peon? p = Sistema.Instancia.GetPeonPorID(id);

        if (p == null)
        {
            return Redirect("/");
        }

        ViewBag.Peon = p;

        return View();
    }

    public IActionResult Tareas(int id)
    {
        Peon? p = Sistema.Instancia.GetPeonPorID(id);

        if (p == null)
        {
            return Redirect("/");
        }

        ViewBag.PeonTareas = new
        {
            IDPeon = p.ID,
            NombrePeon = p.Nombre,
            Tareas = p.GetTareasPendientes()
        };

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
