using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Estancia.Web.Models;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

public class PeonesController : Controller
{
    private readonly ILogger<PeonesController> _logger;

    public PeonesController(ILogger<PeonesController> logger)
    {
        _logger = logger;
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

        Tarea t = p.GetTarea(id, false);

        if (t == null) return Redirect("/Peones/MisTareas");

        return View(t);
    }

    [HttpPost]
    public IActionResult CompletarTarea(Tarea t)
    {
        int? IDUsuario = HttpContext.Session.GetInt32("IDUsuario");
        if (IDUsuario == null) return Redirect("/");

        Peon? p = Sistema.Instancia.GetPeonPorID((int)IDUsuario);
        if (p == null) return Redirect("/");

        Tarea? tarea = p.GetTarea(t.ID, false);
        if (tarea == null) return Redirect("/");

        try
        {
            tarea.Completar(t.Comentario);
            Console.WriteLine(tarea);
            return Redirect("/Peones/MisTareas");
        }
        catch (Exception e)
        {
            ViewBag.msg = e.Message;
            return View(tarea);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
