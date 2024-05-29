namespace Estancia.Dominio;

public class Bovino : Animal
{
    public EAlimentacion Alimentacion { get; set; }
    public static double PrecioKgPie { get; set; } = 100;

    public Bovino() { }

    public Bovino(string id, string raza, ESexo sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, bool esHibrido, EAlimentacion alimentacion) : base(id, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido)
    {
        Alimentacion = alimentacion;
    }

    // Para calcular el precio potencial de venta de un bovino se multplica su peso por el
    // precio por kilogramo de bovino. 
    // Si el bovino fue alimentado a grano se le agrega un 30% 
    // y si además es hembra se le agrega otro 10%.
    public override double GetPrecioVenta()
    {
        double precioBase = PrecioKgPie * Peso;
        double precioFinal = precioBase;

        if (Alimentacion == EAlimentacion.Grano)
        {
            precioFinal += precioBase * 30 / 100;
        }

        if (Sexo == ESexo.Hembra)
        {
            precioFinal += precioBase * 10 / 100;
        }

        double costoCrianza = GetCostoCrianza();
        precioFinal -= costoCrianza;
        return precioFinal;
    }

    public override string GetTipo()
    {
        return "Bovino";
    }
}