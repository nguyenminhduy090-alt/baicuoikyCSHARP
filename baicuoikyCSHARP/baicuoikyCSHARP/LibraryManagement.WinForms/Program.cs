using LibraryManagement.WinForms.Forms;

namespace LibraryManagement.WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using var login = new LoginForm();
        if (login.ShowDialog() == DialogResult.OK && login.Session is not null)
        {
            Application.Run(new MainForm(login.Session));
        }
    }
}
