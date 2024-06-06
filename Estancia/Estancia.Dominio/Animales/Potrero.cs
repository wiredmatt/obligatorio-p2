namespace Estancia.Dominio;

public class Potrero : IValidable, IComparable<Potrero>
{
    private static int UltimoID = 1;
    public int ID { get; set; }
    public string Descripcion { get; set; }
    public int Hectareas { get; set; }
    public int Capacidad { get; set; }
    private List<Animal> Animales { get; set; } = new List<Animal>();

    public Potrero()
    {
        ID = UltimoID++;
    }

    public Potrero(string descripcion, int hectareas, int capacidad)
    {
        ID = UltimoID++;
        Descripcion = descripcion;
        Hectareas = hectareas;
        Capacidad = capacidad;
    }

    public IEnumerable<Animal> GetAnimales()
    {
        return Animales;
    }

    public void Validar()
    {
        List<string> errores = new List<string>();

        if (Validadores.EsStringVacio(Descripcion))
        {
            errores.Add("La descripción no puede estar vacía");
        }

        if (Hectareas <= 0)
        {
            errores.Add("Las hectáreas deben ser mayor a 0");
        }

        if (Capacidad <= 0)
        {
            errores.Add("La capacidad debe ser mayor a 0");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }
    }

    public void AgregarAnimal(Animal animal)
    {
        if (!animal.EstaLibre) throw new ErrorDeValidacion("El animal ya está asignado a un Potrero");

        if (Animales.Count < Capacidad)
        {
            animal.EstaLibre = false;
            Animales.Add(animal);
        }
        else
        {
            throw new ErrorDeValidacion("El potrero está lleno. No se pudo agregar al animal.");
        }
    }

    public double CalcularGanancia()
    {
        double ganancia = 0;
        foreach (var animal in Animales)
        {
            ganancia += animal.GetPrecioVenta();
        }
        return ganancia;

    }

    public override string ToString()
    {
        return $"| #{ID} | {Hectareas} | {Animales.Count} / {Capacidad} | {Descripcion} |";
    }


    // Ver listado de todos los potreros ordenados 
    // por capacidad ascendente y cantidad de animales descendente.
    public int CompareTo(Potrero? other)
    {
        if (other == null) return 1;

        int capCompare = Capacidad.CompareTo(other.Capacidad);

        // si tienen la misma capacidad, comparar por cantidad de animales
        if (capCompare == 0) return other.Animales.Count.CompareTo(Animales.Count);
        else return capCompare;
    }
}