namespace Estancia.Dominio;

public class Capataz : Empleado
{
    public List<Peon> PeonesACargo { get; private set; } = new List<Peon>();

    public Capataz() { }

    public Capataz(string mail, string contrasena, string nombre, DateTime fechaIngreso) : base(mail, contrasena, nombre, fechaIngreso)
    {
    }

    public int GetCantidadDePeones()
    {
        return PeonesACargo.Count;
    }

    public void AgregarPeonACargo(Peon peon)
    {
        foreach (Peon p in PeonesACargo)
        {
            if (p.ID == peon.ID)
            {
                return;
            }
        }

        PeonesACargo.Add(peon);
    }
}