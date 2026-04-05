namespace LibraryManagement.WinForms.Models;

public class UserSession
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string FullName { get; set; } = "";
    public string RoleName { get; set; } = "";
    public HashSet<string> Permissions { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public bool HasPermission(string permission) => Permissions.Contains(permission);
}
