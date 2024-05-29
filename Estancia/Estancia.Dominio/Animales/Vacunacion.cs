namespace Estancia.Dominio;

public class Vacunacion : IValidable
{
    public Vacuna Vacuna { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime Vencimiento { get; set; }

    public Vacunacion() { }

    public Vacunacion(Vacuna vacuna, DateTime fecha)
    {
        Vacuna = vacuna;
        Fecha = fecha;
        Vencimiento = fecha.AddYears(1);
    }

    public void Validar()
    {
        List<string> errores = new List<string>();

        if (Vacuna == null)
        {
            errores.Add("La vacuna es requerida");
        }

        if (Fecha == default)
        {
            errores.Add("La fecha es requerida");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }
    }
}