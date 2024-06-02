using Microsoft.AspNetCore.Mvc;
using Estancia.Dominio;

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
    public IActionResult Login(string mail, string contrasena)
    {
        try
        {
            Empleado e = Sistema.Instancia.Login(mail, contrasena);

            HttpContext.Session.SetInt32("IDUsuario", e.ID);
            HttpContext.Session.SetString("RolUsuario", e.GetTipo());

            return e.GetTipo() switch
            {
                "Peon" => Redirect("/Peon/Index"),
                "Capataz" => Redirect("/Capataz/Index"),
                _ => View(),
            };
        }
        catch (Exception err)
        {
            // pasar los valores que uso el usuario para pre-popular
            // su siguiente intento, y no borrarle todo.
            ViewBag.msg = err.Message;
            ViewBag.Mail = mail;
            ViewBag.Contrasena = contrasena;
            return View();
        }
    }

    public IActionResult RegistroPeones()
    {
        return View();
    }

    [HttpPost]
    // no castea "on" | "off" a boolean.
    public IActionResult RegistroPeones(string mail, string contrasena, string nombre, string esResidente)
    {
        try
        {
            Peon p = Sistema.Instancia.RegistroPeon(mail, contrasena, nombre, esResidente == "on");

            HttpContext.Session.SetInt32("IDUsuario", p.ID);
            HttpContext.Session.SetString("RolUsuario", p.GetTipo());

            return Redirect("/Peon/Index");
        }
        catch (Exception err)
        {
            // pasar los valores que uso el usuario para pre-popular
            // su siguiente intento, y no borrarle todo.
            ViewBag.msg = err.Message;
            ViewBag.Mail = mail;
            ViewBag.Contrasena = contrasena;
            ViewBag.Nombre = nombre;
            ViewBag.EsResidente = "on";

            return View();
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("IDUsuario");
        HttpContext.Session.Remove("RolUsuario");
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }
}