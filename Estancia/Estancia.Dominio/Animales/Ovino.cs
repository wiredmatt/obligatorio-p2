namespace Estancia.Dominio;

public class Ovino : Animal
{
    public static double PrecioKgPie { get; set; } = 200;
    public static double PrecioKgLana { get; set; } = 50;

    public Ovino() { }

    public Ovino(string id, string raza, ESexo sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, bool esHibrido) : base(id, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido)
    {
    }

    public override double GetPrecioVenta()
    {
        return 0;
    }

    public override string GetTipo()
    {
        return "Ovino";
    }
}