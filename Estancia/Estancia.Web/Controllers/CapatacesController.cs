using Microsoft.AspNetCore.Mvc;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

// TODO: Implementar RBAC: a la ruta /peones/detalle/1 solo puede acceder el peon que tenga ID 1
public class CapatacesController : Controller
{
    private readonly ILogger<CapatacesController> _logger;

    public CapatacesController(ILogger<CapatacesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Peones()
    {
        IEnumerable<Peon> peones = Sistema.Instancia.GetPeones();

        ViewBag.Peones = peones;

        return View();
    }

    [Route("/Capataces/Peones/{id}/Tareas")]
    public IActionResult PeonesTareas(int id)
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
            Tareas = p.GetTareas()
        };

        return View();
    }
}