namespace Estancia.Dominio;

public class Sistema
{
    private static Sistema instancia;

    public List<Capataz> Capataces { get; private set; } = new List<Capataz>();
    public List<Peon> Peones { get; private set; } = new List<Peon>();
    public List<Bovino> Bovinos { get; private set; } = new List<Bovino>();
    public List<Ovino> Ovinos { get; private set; } = new List<Ovino>();
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

    private void Precarga()
    {
        return;
    }

    public List<Animal> ListarAnimales()
    {
        List<Animal> animales = [.. Bovinos, .. Ovinos];

        animales.Sort(); // TODO: sort criteria?

        return animales;
    }

    public void AltaOvino(Ovino o)
    {
        o.Validar();
        Ovinos.Add(o);
    }
    public void AltaBovino(Bovino b)
    {
        b.Validar();
        Bovinos.Add(b);
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
}
