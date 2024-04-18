namespace Estancia.Dominio;

public class Ovino : Animal
{
    public static double PrecioKgPie { get; set; } = 200;
    public static double PrecioKgLana { get; set; } = 50;

    public double PesoLana { get; private set; }

    public Ovino() { }

    public Ovino(string id, string raza, ESexo sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, bool esHibrido, double pesoLana) : base(id, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido)
    {
        PesoLana = pesoLana;
    }

    public override string GetTipo()
    {
        return "Ovino";
    }

    public override double GetPrecioVenta()
    {
        double precioBase = (PesoLana * PrecioKgLana) + (PrecioKgPie * Peso);
        double precio = precioBase;

        if (EsHibrido)
        {
            precio -= precioBase * 5 / 100;
        }

        return precio;
    }
}