namespace Estancia.Dominio;

public class Tarea : IValidable, IComparable<Tarea>
{
    private static int UltimoID = 1;
    public int ID { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaLimite { get; set; }
    public DateTime? FechaCierre { get; set; } = null;
    public bool Completada { get; set; }
    public string? Comentario { get; set; } // null hasta que se complete la tarea
    public Capataz Capataz { get; set; }

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

        if (Validadores.EsStringVacio(Descripcion))
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
            if (Validadores.EsStringVacio(comentario))
            {
                throw new ErrorDeValidacion("Al completar la tarea, se le debe agregar un comentario");
            }

            Completada = true;
            Comentario = comentario;
            FechaCierre = DateTime.Now;
        }
    }

    public int CompareTo(Tarea? other)
    {
        if (other == null)
        {
            return 1;
        }

        return FechaLimite.CompareTo(other.FechaLimite);
    }
}