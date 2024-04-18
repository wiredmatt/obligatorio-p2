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
        double precioBase = PrecioKgPie * Peso;
        double precio = precioBase;

        if (Alimentacion == EAlimentacion.Grano)
        {
            precio += precioBase * 30 / 100;
        }

        if (Sexo == ESexo.Hembra)
        {
            precio += precioBase * 10 / 100;
        }

        return precio;
    }

    public override string GetTipo()
    {
        return "Bovino";
    }
}