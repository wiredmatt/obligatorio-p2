using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Estancia.Web.Models;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

public class EmpleadosController : Controller
{
    private readonly ILogger<EmpleadosController> _logger;

    public EmpleadosController(ILogger<EmpleadosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Empleado> empleados = Sistema.Instancia.Empleados;

        ViewBag.Empleados = empleados;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
