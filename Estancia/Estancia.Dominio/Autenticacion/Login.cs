namespace Estancia.Dominio;

public class Login : IValidable
{
    public string Mail { get; set; }

    public string Contrasena { get; set; }

    public Login() { }

    public Login(string mail, string contrasena)
    {
        Mail = mail;
        Contrasena = contrasena;
    }

    public void Validar()
    {
        // todo: checks?
    }
}