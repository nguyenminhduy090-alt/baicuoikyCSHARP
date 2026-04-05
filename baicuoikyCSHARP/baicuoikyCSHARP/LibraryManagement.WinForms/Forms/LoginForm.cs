using System.IO;
using LibraryManagement.WinForms.Models;
using LibraryManagement.WinForms.Services;

namespace LibraryManagement.WinForms.Forms;

public partial class LoginForm : Form
{
    private readonly AuthService _authService = new();

    private static readonly string RememberFile =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "LibraryManagement",
            "remember_username.txt");

    public UserSession? Session { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
        lblTitle.Text = AppSettings.AppTitle;
        Text = $"{AppSettings.AppTitle} - Đăng nhập";
        LoadRememberedUsername();
    }

    private void LoadRememberedUsername()
    {
        try
        {
            if (File.Exists(RememberFile))
            {
                string savedUsername = File.ReadAllText(RememberFile).Trim();
                if (!string.IsNullOrWhiteSpace(savedUsername))
                {
                    _txtUsername.Text = savedUsername;
                    _chkRememberUsername.Checked = true;
                }
            }
        }
        catch
        {
            // bỏ qua lỗi đọc file
        }
    }

    private void SaveRememberedUsername()
    {
        try
        {
            string? folder = Path.GetDirectoryName(RememberFile);
            if (!string.IsNullOrWhiteSpace(folder) && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (_chkRememberUsername.Checked)
            {
                File.WriteAllText(RememberFile, _txtUsername.Text.Trim());
            }
            else
            {
                if (File.Exists(RememberFile))
                    File.Delete(RememberFile);
            }
        }
        catch
        {
            
        }
    }

    private void LoginExit_Click(object? sender, EventArgs e)
    {
        Close();
    }
    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            _lblError.Text = "";

            Session = _authService.Login(_txtUsername.Text.Trim(), _txtPassword.Text);

            if (Session is null)
            {
                _lblError.Text = "Sai tên đăng nhập hoặc mật khẩu.";
                return;
            }

            SaveRememberedUsername();

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không thể kết nối cơ sở dữ liệu.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnExit_Click(object? sender, EventArgs e)
    {
        Close();
    }
}