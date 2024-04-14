namespace Estancia.Dominio;

public class Sistema
{
    private static Sistema instancia;

    public static Sistema Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }
    }

    private Sistema()
    {
        Precarga();
    }

    private void Precarga()
    {
        return;
    }

    public void ListarAnimales()
    {
    }

    public void BuscarPotreros(int hectareas, int capacidad)
    {
    }

    public void EstablecerPrecioLana(double precio)
    {
    }
}
