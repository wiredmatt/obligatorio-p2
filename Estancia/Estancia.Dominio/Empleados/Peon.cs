namespace Estancia.Dominio;

public class Peon : Empleado
{
    public bool EsResidente { get; set; }

    public List<Tarea> Tareas { get; private set; } = new List<Tarea>();

    public Peon() { }

    public Peon(string mail, string contrasena, string nombre, DateTime fechaIngreso, bool esResidente) : base(mail, contrasena, nombre, fechaIngreso)
    {
        EsResidente = esResidente;
    }

    public void AltaTarea(Tarea tarea)
    {
        tarea.Validar();

        foreach (Tarea t in Tareas)
        {
            if (t.ID == tarea.ID)
            {
                return;
            }
        }

        Tareas.Add(tarea);
    }
}