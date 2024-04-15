using System.Globalization;
using Estancia.Dominio;

namespace Estancia.CLI;

class Program
{
    static void Main(string[] args)
    {
        bool salir = false; // uso de bandera en lugar de `opcion` para mayor control.

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Listar Animales");
            Console.WriteLine("2. Filtrar Potreros");
            Console.WriteLine("3. Establecer precio del kilo de lana");
            Console.WriteLine("4. Alta de bovinos");
            Console.WriteLine("0. Salir");

            int opcion = LeerNumeroEntero("Ingrese una opción (1/2/3/4/0):");

            switch (opcion)
            {
                case 1:
                    ListarAnimales();
                    break;
                case 2:
                    BuscarPotreros();
                    break;
                case 3:
                    Console.Clear();
                    double precio = LeerNumeroEntero("Ingrese precio del kilo de lana:");
                    Sistema.EstablecerPrecioLana(precio);
                    break;
                case 4:
                    AltaBovino();
                    break;
                case 0:
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }

            if (salir) return; // Salir del bucle si se selecciona la opción 0 y no esperar a que el usuario presione una tecla.

            Console.ReadKey();
        }
    }

    public static void ListarAnimales()
    {
        Console.Clear();
        Console.WriteLine("LISTADO DE ANIMALES");
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("| Caravana  |  Tipo  |  Sexo  |  Raza  |    Peso    |");
        Console.WriteLine("---------------------------------------------------");
        foreach (Animal a in Sistema.Instancia.Animales)
        {
            string animalStr = a.ToString();
            Console.WriteLine(animalStr);
            Console.WriteLine(new string('-', animalStr.Length));
        }
    }

    public static void BuscarPotreros()
    {
        Console.Clear();
        Console.WriteLine("BUSQUEDA DE POTREROS");
        Console.WriteLine("-------------------------------------------");

        int hectareas = LeerNumeroEntero("Ingrese número de hectareas:");
        int capacidad = LeerNumeroEntero("Ingrese capacidad:");

        List<Potrero> potreros = Sistema.Instancia.BuscarPotreros(hectareas, capacidad);

        Console.WriteLine("Potreros encontrados:");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("| ID  |  Hectareas  |  Cantidad / Capacidad  |    Descripcion    |");
        Console.WriteLine("-----------------------------------------------------------------");
        foreach (Potrero p in potreros)
        {
            Console.WriteLine(p);
        }
    }

    public static void AltaBovino()
    {
        Console.Clear();
        Console.WriteLine("ALTA DE BOVINO");
        Console.WriteLine("---------------------------------------------------");

        string caravana = LeerString("Ingrese la caravana:", Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL, Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL, true);

        string raza = LeerString("Ingrese la raza:", 1);

        double peso = LeerNumero("Ingrese el peso:");

        Console.WriteLine($"Ingrese el sexo: ([M]/H)");
        string _sexo = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(_sexo) || !_sexo.Equals("h", StringComparison.CurrentCultureIgnoreCase) || !_sexo.Equals("m", StringComparison.CurrentCultureIgnoreCase))
        {
            _sexo = "m";
        }
        ESexo sexo = ESexo.Macho;
        if (_sexo.Equals("h", StringComparison.CurrentCultureIgnoreCase))
        {
            sexo = ESexo.Hembra;
        }

        bool esHibrido = LeerString("Es híbrido? (si/[no])").Equals("si", StringComparison.CurrentCultureIgnoreCase);

        Console.WriteLine("Ingrese el tipo de alimentación ([P]/G):");
        string _alimentacion = Console.ReadLine();
        EAlimentacion alimentacion = EAlimentacion.Pastura;
        if (_alimentacion.Equals("g", StringComparison.CurrentCultureIgnoreCase))
        {
            alimentacion = EAlimentacion.Grano;
        }

        DateTime fechaNacimiento = LeerFecha("Ingrese la fecha de nacimiento: (dia/mes/año)");

        double costoAdquisicion = LeerNumero("Ingrese el costo de adquisición:");
        double costoAlimentacion = LeerNumero("Ingrese el costo de alimentación:");

        try
        {
            Bovino b = new Bovino(caravana, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido, alimentacion);
            Sistema.Instancia.AltaBovino(b);

            Console.WriteLine($"El Bovino #{b.ID} fue ingresado correctamente.");
        }
        catch (ErrorDeValidacion e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("No se guardo el Bovino.");
        }
    }


    public static int LeerNumeroEntero(string mensaje, bool positivo = true)
    {
        int numero;
        Console.WriteLine(mensaje);
        string strNumero = Console.ReadLine();

        while (!Int32.TryParse(strNumero, out numero) || (positivo && numero < 0))
        {
            Console.WriteLine("Debe ingresar un número entero.");
            if (positivo && numero < 0)
            {
                Console.WriteLine("El número debe ser positivo.");
            }
            Console.WriteLine(mensaje);
            strNumero = Console.ReadLine();
        }

        return numero;
    }

    public static double LeerNumero(string mensaje, bool positivo = true)
    {
        double numero;
        Console.WriteLine(mensaje);
        string strNumero = Console.ReadLine();

        while (!Double.TryParse(strNumero, out numero) || (positivo && numero < 0))
        {
            Console.WriteLine("Debe ingresar un número.");
            if (positivo && numero < 0)
            {
                Console.WriteLine("El número debe ser positivo.");
            }
            Console.WriteLine(mensaje);
            strNumero = Console.ReadLine();
        }

        return numero;
    }

    public static DateTime LeerFecha(string mensaje)
    {
        DateTime fecha;
        Console.WriteLine(mensaje);
        string strFecha = Console.ReadLine();

        while (!DateTime.TryParse(strFecha, new CultureInfo("es-UY"), out fecha))
        {
            Console.WriteLine("Debe ingresar una fecha valida.");
            Console.WriteLine(mensaje);
            strFecha = Console.ReadLine();
        }

        return fecha;
    }

    public static string LeerString(string mensaje, int min = 0, int max = 0, bool alfanumerico = false)
    {
        Console.WriteLine(mensaje);
        string str = Console.ReadLine();

        while (min > 0 && str.Length < min || max > 0 && str.Length > max || alfanumerico && !Validadores.EsAlfaNumerico(str))
        {
            Console.WriteLine("Debe ingresar un valor válido.");
            if (min > 0 && str.Length < min)
            {
                Console.WriteLine($"> El valor debe tener al menos {min} caracteres.");
            }
            if (max > 0 && str.Length > max)
            {
                Console.WriteLine($"> El valor debe tener menos de {max} caracteres.");
            }
            if (alfanumerico && !Validadores.EsAlfaNumerico(str))
            {
                Console.WriteLine("> El valor debe ser alfanumérico.");
            }

            Console.WriteLine(mensaje);
            str = Console.ReadLine();
        }

        return str;
    }
}