using Estancia.Dominio;

namespace Estancia.CLI;

class Program
{
    static void Main(string[] args)
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Listar Animales");
            Console.WriteLine("2. Filtrar Potreros");
            Console.WriteLine("3. Establecer precio de la lana");
            Console.WriteLine("4. Alta de bovinos");
            Console.WriteLine("0. Salir");

            int opcion;
            try
            {
                opcion = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Debe ingresar un número para operar el menú.");
                Console.ReadKey();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    ListarAnimales();
                    break;
                case 2:
                    int hectareas = 0; // TODO: Leer de consola, validar que sea un número
                    int capacidad = 0;
                    Sistema.Instancia.BuscarPotreros(hectareas, capacidad);
                    break;
                case 3:
                    double precio = 0;
                    Sistema.EstablecerPrecioLana(precio);
                    break;
                case 4:
                    // todo
                    break;
                case 0:
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }

            Console.ReadKey();
        }
    }

    public static void ListarAnimales()
    {
        Console.Clear();
        Console.WriteLine("Listado de animales:");
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
}