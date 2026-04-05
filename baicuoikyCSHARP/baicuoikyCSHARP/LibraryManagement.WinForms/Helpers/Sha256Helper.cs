using System.Security.Cryptography;
using System.Text;

namespace LibraryManagement.WinForms.Helpers;

public static class Sha256Helper
{
    public static string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
