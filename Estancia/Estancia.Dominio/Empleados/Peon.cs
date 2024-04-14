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
}