namespace Estancia.Dominio;

public class Vacunacion
{
    public Vacuna Vacuna { get; private set; }
    public DateTime Fecha { get; private set; }

    public DateTime Vencimiento { get; private set; }

    public Vacunacion() { }

    public Vacunacion(Vacuna vacuna, DateTime fecha)
    {
        Vacuna = vacuna;
        Fecha = fecha;
        Vencimiento = fecha.AddYears(1);
    }
}