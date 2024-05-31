using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Estancia.Web.Models;
using Estancia.Dominio;

namespace Estancia.Web.Controllers;

public class PotrerosController : Controller
{
    private readonly ILogger<PotrerosController> _logger;

    public PotrerosController(ILogger<PotrerosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        IEnumerable<Potrero> Potreros = Sistema.Instancia.GetPotreros();

        ViewBag.Potreros = Potreros;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
