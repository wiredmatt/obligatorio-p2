namespace Estancia.Dominio;

public static class Validadores
{
    public static bool EsAlfaNumerico(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsLetter(str[i]) && (!char.IsNumber(str[i])))
                return false;
        }

        return true;
    }

    public static bool CumpleMinimoMaximoCaracteres(string str, int min)
    {
        return min == 0 || str.Length >= min;
    }

    public static bool CumpleMinimoMaximoCaracteres(string str, int min, int max)
    {
        return (min == 0 || str.Length >= min) && (max == 0 || str.Length <= max);
    }

    public static bool EsStringVacio(string str)
    {
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }
}