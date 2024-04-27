namespace Estancia.Dominio;

public class Tarea : IValidable
{
    private static int UltimoID = 1;
    public int ID { get; private set; }
    public string Descripcion { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaLimite { get; private set; }
    public DateTime? FechaCierre { get; private set; } = null;
    public bool Completada { get; private set; }
    public string? Comentario { get; private set; } // null hasta que se complete la tarea
    public Capataz Capataz { get; private set; }

    public Tarea()
    {
        ID = UltimoID++;
    }

    public Tarea(string descripcion, DateTime fechaInicio, DateTime fechaLimite, Capataz capataz)
    {
        ID = UltimoID++;
        Descripcion = descripcion;
        FechaInicio = fechaInicio;
        FechaLimite = fechaLimite;
        Capataz = capataz;
    }

    public void Validar()
    {
        List<string> errores = new List<string>();

        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            errores.Add("La descripción no puede estar vacía");
        }

        if (FechaInicio == default)
        {
            errores.Add("La fecha de inicio no puede estar vacía");
        }

        if (FechaLimite == default)
        {
            errores.Add("La fecha limite no puede estar vacía");
        }

        if (FechaInicio > FechaLimite)
        {
            errores.Add("La fecha de inicio no puede ser mayor a la fecha de limite");
        }

        if (Capataz == null)
        {
            errores.Add("La tarea debe tener un capataz asignado");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }
    }

    public void Completar(string comentario)
    {
        if (!Completada)
        {
            Completada = true;
            Comentario = comentario;
            FechaCierre = DateTime.Now;
        }
    }
}