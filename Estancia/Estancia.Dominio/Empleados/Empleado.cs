namespace Estancia.Dominio;

public abstract class Empleado : IValidable
{
    private static int UltimoID = 1;

    public int ID { get; set; }

    public string Mail { get; set; }

    public string Contrasena { get; set; }

    public string Nombre { get; set; }

    public DateTime FechaIngreso { get; set; }

    public Empleado()
    {
        ID = UltimoID++;
    }

    public Empleado(string mail, string contrasena, string nombre, DateTime fechaIngreso)
    {
        ID = UltimoID++;
        Mail = mail;
        Contrasena = contrasena;
        Nombre = nombre;
        FechaIngreso = fechaIngreso;
    }

    public virtual void Validar()
    {
        List<string> errores = new List<string>();


        if (Validadores.EsStringVacio(Mail))
        {
            errores.Add("El mail es requerido");
        }

        if (Validadores.EsStringVacio(Contrasena))
        {
            errores.Add("La contraseña es requerida");
        }

        if (!Validadores.CumpleMinimoMaximoCaracteres(Contrasena, Config.MIN_CARACTERES_CONTRASENA))
        {
            errores.Add($"La contraseña debe tener al menos {Config.MIN_CARACTERES_CONTRASENA} caracteres");
        }

        if (Validadores.EsStringVacio(Nombre))
        {
            errores.Add("El nombre es requerido");
        }

        if (FechaIngreso == default)
        {
            errores.Add("La fecha de ingreso es requerida");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }

        return;
    }

    public abstract string GetTipo();

    public override string ToString()
    {
        return $"| #{ID} | {Nombre} | {Mail} |";
    }
}
