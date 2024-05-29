namespace Estancia.Dominio;

public class Vacuna : IValidable
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Patogeno { get; set; }

    public Vacuna() { }

    public Vacuna(string nombre, string descripcion, string patogeno)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Patogeno = patogeno;
    }

    public void Validar()
    {
        List<string> errores = new List<string>();

        if (Validadores.EsStringVacio(Nombre))
        {
            errores.Add("El nombre es requerido");
        }

        if (Validadores.EsStringVacio(Descripcion))
        {
            errores.Add("La descripción es requerida");
        }

        if (Validadores.EsStringVacio(Patogeno))
        {
            errores.Add("El patógeno es requerido");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }
    }
}
