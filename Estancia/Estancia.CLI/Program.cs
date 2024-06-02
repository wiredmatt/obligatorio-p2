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
                    GetPotreros();
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
        foreach (Animal a in Sistema.Instancia.GetAnimales())
        {
            string animalStr = a.ToString();
            Console.WriteLine(animalStr);
            Console.WriteLine(new string('-', animalStr.Length));
        }
    }

    public static void GetPotreros()
    {
        Console.Clear();
        Console.WriteLine("BUSQUEDA DE POTREROS");
        Console.WriteLine("-------------------------------------------");

        int hectareas = LeerNumeroEntero("Ingrese número de hectareas:");
        int capacidad = LeerNumeroEntero("Ingrese capacidad:");

        IEnumerable<Potrero> potreros = Sistema.Instancia.GetPotreros(hectareas, capacidad);

        Console.WriteLine("Potreros encontrados:");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("| ID  |  Hectareas  |  Cantidad / Capacidad  |    Descripcion    |");
        Console.WriteLine("-----------------------------------------------------------------");
        foreach (Potrero p in potreros)
        {
            string potreroStr = p.ToString();
            Console.WriteLine(potreroStr);
            Console.WriteLine(new string('-', potreroStr.Length));
        }
    }

    public static void AltaBovino()
    {
        Console.Clear();
        Console.WriteLine("ALTA DE BOVINO");
        Console.WriteLine("---------------------------------------------------");

        bool caravanaLibre = false;
        string id = "";

        while (!caravanaLibre)
        {
            id = LeerString("Ingrese la caravana:", Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL, Config.CANTIDAD_CARACTERES_CARAVANA_ANIMAL, true);
            caravanaLibre = Sistema.Instancia.GetAnimalPorID(id) == null;

            if (!caravanaLibre)
            {
                Console.WriteLine("Ya existe un animal con esa caravana.");
            }
        }


        string raza = LeerString("Ingrese la raza:", 1);

        double peso = LeerNumero("Ingrese el peso:");

        string _sexo = LeerString($"Ingrese el sexo: (Macho / Hembra)", 1, 0, false, new List<string> { "m", "macho", "h", "hembra" }).ToLower();
        ESexo sexo = _sexo switch
        {
            "m" or "macho" => ESexo.Macho,
            "h" or "hembra" => ESexo.Hembra,
            _ => ESexo.Hembra,
        };

        bool esHibrido = LeerString("Es híbrido? (si / no)", 1, 0, false, new List<string> { "s", "si", "n", "no" }).StartsWith("s", StringComparison.CurrentCultureIgnoreCase);

        string _alimentacion = LeerString($"Ingrese el tipo de alimentación (Pastura / Grano):", 1, 0, false, new List<string> { "p", "pastura", "g", "grano" });
        var alimentacion = _alimentacion switch
        {
            "p" or "pastura" => EAlimentacion.Pastura,
            "g" or "grano" => EAlimentacion.Grano,
            _ => EAlimentacion.Pastura,
        };

        DateTime fechaNacimiento = LeerFecha("Ingrese la fecha de nacimiento: (dia/mes/año)");

        double costoAdquisicion = LeerNumero("Ingrese el costo de adquisición:");
        double costoAlimentacion = LeerNumero("Ingrese el costo de alimentación:");

        try
        {
            Bovino b = new Bovino(id, raza, sexo, fechaNacimiento, costoAdquisicion, costoAlimentacion, peso, esHibrido, alimentacion);
            Sistema.Instancia.AltaAnimal(b);

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
        int numero = 0;
        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine(mensaje);
            string strNumero = Console.ReadLine();

            try
            {
                numero = Int32.Parse(strNumero);
                if (positivo && numero < 0)
                {
                    Console.WriteLine("El número debe ser positivo.");
                }
                else
                {
                    isValid = true;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Debe ingresar un número entero.");
            }
        }

        return numero;
    }

    public static double LeerNumero(string mensaje, bool positivo = true)
    {
        double numero = 0;
        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine(mensaje);
            string strNumero = Console.ReadLine();

            try
            {
                numero = Double.Parse(strNumero);
                if (positivo && numero < 0)
                {
                    Console.WriteLine("El número debe ser positivo.");
                }
                else
                {
                    isValid = true;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Debe ingresar un número.");
            }
        }

        return numero;
    }

    public static DateTime LeerFecha(string mensaje)
    {
        DateTime fecha = DateTime.MinValue;
        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine(mensaje);
            string strFecha = Console.ReadLine();

            try
            {
                fecha = DateTime.Parse(strFecha, new CultureInfo("es-UY"));
                isValid = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Debe ingresar una fecha valida.");
            }
        }

        return fecha;
    }

    public static string LeerString(string mensaje, int min = 0, int max = 0, bool alfanumerico = false, List<string> opcionesValidas = null)
    {
        Console.WriteLine(mensaje);
        string str = Console.ReadLine();

        while (!Validadores.CumpleMinimoMaximoCaracteres(str, min, max)
               || alfanumerico && !Validadores.EsAlfaNumerico(str)
               || opcionesValidas != null && !opcionesValidas.Contains(str, StringComparer.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("Debe ingresar un valor válido.");
            if (min > 0 && str.Length < min)
            {
                Console.WriteLine($"> El valor debe tener al menos {min} caracteres.");
            }
            if (max > 0 && str.Length > max)
            {
                Console.WriteLine($"> El valor debe tener no más de {max} caracteres.");
            }
            if (alfanumerico && !Validadores.EsAlfaNumerico(str))
            {
                Console.WriteLine("> El valor debe ser alfanumérico.");
            }
            if (opcionesValidas != null && !opcionesValidas.Contains(str))
            {
                Console.WriteLine($"> El valor debe ser una de las siguientes opciones: {string.Join(", ", opcionesValidas)}");
            }

            Console.WriteLine(mensaje);
            str = Console.ReadLine();
        }

        return str;
    }
}