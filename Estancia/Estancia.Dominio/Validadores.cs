namespace Estancia.Dominio;

public static class Validadores
{
    public static bool EsAlfaNumerico(string str)
    {
        if (str == null) return false;
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsLetter(str[i]) && (!char.IsNumber(str[i])))
                return false;
        }

        return true;
    }

    public static bool CumpleMinimoMaximoCaracteres(string str, int min)
    {
        if (str == null) return false;
        return min == 0 || str.Length >= min;
    }

    public static bool CumpleMinimoMaximoCaracteres(string str, int min, int max)
    {
        if (str == null) return false;
        return (min == 0 || str.Length >= min) && (max == 0 || str.Length <= max);
    }

    public static bool EsStringVacio(string str)
    {
        if (str == null) return true;
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str) || str.Trim() == "";
    }
}