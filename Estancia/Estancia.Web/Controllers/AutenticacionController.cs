using Microsoft.AspNetCore.Mvc;
using Estancia.Dominio;
using Microsoft.AspNetCore.Authorization;

namespace Estancia.Web.Controllers;

public class AutenticacionController : Controller
{
    private readonly ILogger<AutenticacionController> _logger;

    public AutenticacionController(ILogger<AutenticacionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Login login)
    {
        if (!ModelState.IsValid)
        {
            return Redirect("/Autenticacion/Login");
        }

        Empleado? e = Sistema.Instancia.Login(login.Mail, login.Contrasena);

        if (e == null)
        {
            return Redirect("/Autenticacion/Login");
        }

        HttpContext.Session.SetInt32("IDUsuario", e.ID);
        HttpContext.Session.SetString("RolUsuario", e.GetTipo());

        return Redirect("/Peones/Detalle");
    }
}