namespace Estancia.Dominio;

public class Ovino : Animal
{
    public static double PrecioKgPie { get; set; } = 200;
    public static double PrecioKgLana { get; set; } = 50;

    public double PesoLana { get; set; }

    public Ovino() { }

    public Ovino(string id, string raza, ESexo sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, bool esHibrido, double pesoLana) : base(id, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido)
    {
        PesoLana = pesoLana;
    }

    public override string GetTipo()
    {
        return "Ovino";
    }

    // El potencial precio de venta en ovinos se determina multplicando el peso de lana
    // estimada por el precio por kilogramo de lana. 
    // A esto se le suma el producto del precio por kilogramo de ovino en pie por el peso del animal. 
    // Además, si la raza es híbrida a este precio se le resta un 5%.
    public override double GetPrecioVenta()
    {
        double precioBase = (PesoLana * PrecioKgLana) + (PrecioKgPie * Peso);
        double precioFinal = precioBase;

        if (EsHibrido)
        {
            precioFinal -= precioBase * 5 / 100;
        }

        double costoCrianza = GetCostoCrianza();
        precioFinal -= costoCrianza;
        return precioFinal;
    }
}