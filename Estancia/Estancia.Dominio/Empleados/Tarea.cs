namespace Estancia.Dominio;

public class Tarea
{
    private static int UltimoID = 1;
    public int ID { get; private set; }
    public string Descripcion { get; set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaCierre { get; private set; }
    public bool Completada { get; private set; }
    public string? Comentario { get; private set; } // null hasta que se complete la tarea
    public Capataz Capataz { get; private set; }
    public Peon Peon { get; private set; }

    public Tarea()
    {
        ID = UltimoID++;
    }

    public Tarea(string descripcion, DateTime fechaInicio, DateTime fechaCierre, Capataz capataz, Peon peon)
    {
        ID = UltimoID++;
        Descripcion = descripcion;
        FechaInicio = fechaInicio;
        FechaCierre = fechaCierre;
        Capataz = capataz;
        Peon = peon;
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

        if (FechaCierre == default)
        {
            errores.Add("La fecha de cierre no puede estar vacía");
        }

        if (FechaInicio > FechaCierre)
        {
            errores.Add("La fecha de inicio no puede ser mayor a la fecha de cierre");
        }

        if (Capataz == null)
        {
            errores.Add("La tarea debe tener un capataz asignado");
        }

        if (Peon == null)
        {
            errores.Add("La tarea debe tener un peón asignado");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }
    }

    public void Completar(string comentario)
    {
        Completada = true;
        Comentario = comentario;
    }
}