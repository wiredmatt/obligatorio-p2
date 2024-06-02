namespace Estancia.Dominio;

public abstract class Animal : IValidable
{
    public string ID { get; set; } // Caravana - renombrado a ID para mantener consistencia con otras Entidades.
    public string Raza { get; set; }
    public ESexo Sexo { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public double CostoAdquisicion { get; set; }
    public double CostoAlimentacion { get; set; }
    public double Peso { get; set; }
    public bool EsHibrido { get; set; }
    public bool EstaLibre { get; set; } = true;

    public List<Vacunacion> Vacunaciones { get; set; } = new List<Vacunacion>();

    public Animal()
    {
    }

    public Animal(string id, string raza, ESexo sexo, DateTime fechaNacimiento, double costoAdquisicion, double costoAlimentacion, double peso, bool esHibrido)
    {
        ID = id;
        Raza = raza;
        Sexo = sexo;
        FechaNacimiento = fechaNacimiento;
        CostoAdquisicion = costoAdquisicion;
        CostoAlimentacion = costoAlimentacion;
        Peso = peso;
        EsHibrido = esHibrido;
    }

    public void Vacunar(Vacuna vacuna, DateTime fecha)
    {
        if (fecha < FechaNacimiento)
        {
            throw new ErrorDeValidacion("La fecha de vacunación no puede ser anterior a la fecha de nacimiento del Animal");
        }
        Vacunacion vacunacion = new Vacunacion(vacuna, fecha);
        Vacunaciones.Add(vacunacion);
    }

    public virtual void Validar()
    {
        List<string> errores = new List<string>();

        if (Validadores.EsStringVacio(ID))
        {
            errores.Add("La Caravana no puede estar vacía");
        }

        if (!Validadores.CumpleMinimoMaximoCaracteres(ID, Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL, Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL))
        {
            errores.Add($"La caravana debe tener {Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL} caracteres");
        }

        if (!Validadores.EsAlfaNumerico(ID))
        {
            errores.Add("La caravana debe ser alfanumérica");
        }

        if (Validadores.EsStringVacio(Raza))
        {
            errores.Add("La raza no puede estar vacía");
        }

        if (FechaNacimiento == default)
        {
            errores.Add("La fecha de nacimiento no puede estar vacía");
        }

        if (CostoAdquisicion < 0)
        {
            errores.Add("El costo de adquisición no puede ser negativo");
        }

        if (CostoAlimentacion < 0)
        {
            errores.Add("El costo de alimentación no puede ser negativo");
        }

        if (Peso < 0)
        {
            errores.Add("El peso no puede ser negativo");
        }

        if (errores.Count > 0)
        {
            throw new ErrorDeValidacion(errores);
        }

        return;
    }

    public abstract double GetPrecioVenta();


    // 1. El costo de crianza del animal se determina sumando el coso de adquisición más el 
    // costo de alimentación.
    // 2. A dicho costo se le suman $200 por cada vacuna administrada.
    public double GetCostoCrianza()
    {
        double costoBase = CostoAdquisicion + CostoAlimentacion;
        double costoVacunas = 200 * Vacunaciones.Count;

        return costoBase + costoVacunas;
    }

    public abstract string GetTipo();

    public override string ToString()
    {
        return $"| #{ID} | {GetTipo()} | {Sexo} | {Raza} | {Peso}kg |";
    }
}
