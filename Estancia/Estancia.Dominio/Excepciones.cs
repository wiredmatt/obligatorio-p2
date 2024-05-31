namespace Estancia.Dominio;

public class ErrorDeValidacion : Exception
{
    public ErrorDeValidacion()
    {
    }

    public ErrorDeValidacion(string message)
        : base(message)
    {
    }

    public ErrorDeValidacion(List<string> mensajes) : base(ListaDeErroresAUnMensaje(mensajes)) { }


    public ErrorDeValidacion(string message, Exception inner)
        : base(message, inner)
    {
    }

    private static string ListaDeErroresAUnMensaje(List<string> mensajes)
    {
        string mensajeFinal = "\n";
        mensajeFinal += "Errores al validar!\n";

        for (int i = 0; i < mensajes.Count; i++)
        {
            string mensaje = mensajes.ElementAt(i);

            mensajeFinal += $"[ERROR #{i + 1}]: {mensaje}\n";
        }
        mensajeFinal += "\n";

        return mensajeFinal;
    }
}
