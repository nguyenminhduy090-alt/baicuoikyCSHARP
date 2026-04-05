namespace LibraryManagement.WinForms;

public static class AppSettings
{
    public static string ConnectionString =>
        "Host=localhost;Port=5432;Database=DBquanlythuvien;Username=postgres;Password=2006;Include Error Detail=true";

    public const string AppTitle = "QUẢN LÝ THƯ VIỆN";
}
