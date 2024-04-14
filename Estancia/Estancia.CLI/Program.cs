using Estancia.Dominio;

namespace Estancia.CLI;

class Program
{
    static void Main(string[] args)
    {
        Sistema sistema = Sistema.Instancia;

        bool salir = false;
        int opcion = 0;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Listar Animales");
            Console.WriteLine("2. Filtrar Potreros");
            Console.WriteLine("3. Establecer precio de la lana");
            Console.WriteLine("4. Alta de bovinos");
            Console.WriteLine("0. Salir");

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
                    sistema.ListarAnimales();
                    break;
                case 2:
                    int hectareas = 0; // TODO: Leer de consola, validar que sea un número
                    int capacidad = 0;
                    sistema.BuscarPotreros(hectareas, capacidad);
                    break;
                case 3:
                    double precio = 0;
                    sistema.EstablecerPrecioLana(precio);
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
}