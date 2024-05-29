namespace Estancia.Dominio;

public class Capataz : Empleado
{
    public int CantidadPeones { get; set; }

    public Capataz() { }

    public Capataz(string mail, string contrasena, string nombre, DateTime fechaIngreso, int cantidadPeones) : base(mail, contrasena, nombre, fechaIngreso)
    {
        CantidadPeones = cantidadPeones;
    }

    public override string GetTipo()
    {
        return "Capataz";
    }

    public override void Validar()
    {
        base.Validar();

        List<string> errores = new List<string>();

        if (CantidadPeones < 0)
        {
            errores.Add("La cantidad de peones no puede ser negativa");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}