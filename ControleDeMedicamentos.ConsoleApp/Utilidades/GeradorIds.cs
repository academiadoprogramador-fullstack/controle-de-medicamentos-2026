using System.Security.Cryptography;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public static class GeradorIds
{
    public static string GerarIdCurto()
    {
        return Convert
                .ToHexString(RandomNumberGenerator.GetBytes(4))
                .ToLower()
                .Substring(0, 7);
    }
}
