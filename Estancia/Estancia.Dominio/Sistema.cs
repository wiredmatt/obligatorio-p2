namespace Estancia.Dominio;

public class Sistema
{
    private static Sistema instancia;

    public List<Empleado> Empleados { get; private set; } = new List<Empleado>();
    public List<Animal> Animales { get; private set; } = new List<Animal>();
    public List<Potrero> Potreros { get; private set; } = new List<Potrero>();
    public List<Vacuna> Vacunas { get; private set; } = new List<Vacuna>();

    public static Sistema Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }
    }

    private Sistema()
    {
        Precarga();
    }

    public void AltaCapataz(Capataz c)
    {
        c.Validar();
        Empleados.Add(c);
    }

    public void AltaPeon(Peon p)
    {
        p.Validar();
        Empleados.Add(p);
    }

    public void AltaOvino(Ovino o)
    {
        o.Validar();
        Animales.Add(o);
    }
    public void AltaBovino(Bovino b)
    {
        b.Validar();
        Animales.Add(b);
    }

    public void AltaPotrero(Potrero p)
    {
        p.Validar();
        Potreros.Add(p);
    }

    public void AltaVacuna(Vacuna v)
    {
        v.Validar();
        Vacunas.Add(v);
    }

    public List<Potrero> BuscarPotreros(int hectareas, int capacidad)
    {
        List<Potrero> potreros = new List<Potrero>();

        foreach (Potrero p in Potreros)
        {
            if (p.Hectareas >= hectareas && p.Capacidad >= capacidad)
            {
                potreros.Add(p);
            }
        }

        return potreros;
    }

    public static void EstablecerPrecioLana(double precio)
    {
        Ovino.PrecioKgLana = precio;
    }


    private void Precarga()
    {
        Bovino b1 = new Bovino("12345678", "Angus", ESexo.Macho, new DateTime(2019, 1, 1), 1000, 100, 500, false, EAlimentacion.Pastura);
        Bovino b2 = new Bovino("87654321", "Angus", ESexo.Hembra, new DateTime(2019, 5, 4), 1000, 100, 500, true, EAlimentacion.Grano);

        AltaBovino(b1);
        AltaBovino(b2);
    }
}
