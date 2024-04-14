namespace Estancia.Dominio;

public class Potrero
{
    private static int UltimoID = 1;
    public int ID { get; private set; }

    public string Descripcion { get; set; }

    public int Hectareas { get; private set; }
    public int Capacidad { get; private set; }

    public List<Animal> animales { get; private set; } = new List<Animal>();

    public Potrero() { }

    public Potrero(string descripcion, int hectareas, int capacidad)
    {
        ID = UltimoID;
        UltimoID++;
        Descripcion = descripcion;
        Hectareas = hectareas;
        Capacidad = capacidad;
    }

    public void Validar()
    {
        List<string> errores = new List<string>();

        if (string.IsNullOrWhiteSpace(Descripcion))
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
        if (animales.Count < Capacidad)
        {
            animales.Add(animal);
        }
        else
        {
            throw new ErrorDeValidacion("El potrero está lleno. No se pudo agregar al animal.");
        }
    }
}