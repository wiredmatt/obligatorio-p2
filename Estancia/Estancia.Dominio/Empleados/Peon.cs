namespace Estancia.Dominio;

public class Peon : Empleado
{
    public bool EsResidente { get; set; }

    private List<Tarea> Tareas { get; set; } = new List<Tarea>();

    public Peon() { }

    public Peon(string mail, string contrasena, string nombre, DateTime fechaIngreso, bool esResidente) : base(mail, contrasena, nombre, fechaIngreso)
    {
        EsResidente = esResidente;
    }

    public IEnumerable<Tarea> GetTareas()
    {
        return Tareas;
    }

    public Tarea? GetTareaPorID(int id)
    {
        foreach (Tarea t in Tareas)
        {
            if (t.ID == id) return t;
        }

        return null;
    }

    public Tarea? GetTareaPorID(int id, bool completada)
    {
        foreach (Tarea t in Tareas)
        {
            if (t.ID == id && t.Completada == completada) return t;
        }

        return null;
    }

    public IEnumerable<Tarea> GetTareasPendientes()
    {
        List<Tarea> tareasRet = new List<Tarea>();

        foreach (Tarea t in Tareas)
        {
            if (!t.Completada)
            {
                tareasRet.Add(t);
            }
        }

        tareasRet.Sort();

        return tareasRet;
    }

    public void AltaTarea(Tarea tarea)
    {
        tarea.Validar();
        Tareas.Add(tarea);
    }

    public override string GetTipo()
    {
        return "Peon";
    }

    public override void Validar()
    {
        base.Validar();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}