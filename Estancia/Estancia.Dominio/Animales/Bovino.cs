namespace Estancia.Dominio;

public class Bovino : Animal
{
    public EAlimentacion Alimentacion { get; private set; }
    public static double PrecioKgPie { get; set; } = 100;

    public Bovino() { }

    public Bovino(string id, string raza, ESexo sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, bool esHibrido, EAlimentacion alimentacion) : base(id, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido)
    {
        Alimentacion = alimentacion;
    }

    public override double GetPrecioVenta()
    {
        return 0;
    }
}