using System.Data;
using System.Text.RegularExpressions;

namespace LibraryManagement.WinForms.Forms;

public partial class AccountDialog : Form
{
    public string UsernameValue => txtUsername.Text.Trim();
    public string PasswordValue => txtPassword.Text;
    public string ConfirmPasswordValue => txtConfirmPassword.Text;
    public string FullNameValue => txtFullName.Text.Trim();
    public string EmailValue => txtEmail.Text.Trim();
    public string PhoneValue => txtPhone.Text.Trim();
    public int RoleId => cboRole.SelectedValue is int value ? value : 0;
    public bool IsActive => chkActive.Checked;

    private readonly bool _isEdit;

    public AccountDialog(string title, DataTable roles, IDictionary<string, object?>? data = null, bool isEdit = false)
    {
        _isEdit = isEdit;
        InitializeComponent();
        Text = title;

        cboRole.DataSource = roles;
        cboRole.DisplayMember = "ten_vai_tro";
        cboRole.ValueMember = "vai_tro_id";

        if (data != null)
        {
            txtUsername.Text = data.TryGetValue("Tên đăng nhập", out var u) ? u?.ToString() ?? "" : "";
            txtFullName.Text = data.TryGetValue("Họ tên", out var n) ? n?.ToString() ?? "" : "";
            txtEmail.Text = data.TryGetValue("Email", out var e) ? e?.ToString() ?? "" : "";
            txtPhone.Text = data.TryGetValue("SĐT", out var p) ? p?.ToString() ?? "" : "";
            var roleName = data.TryGetValue("Vai trò", out var r) ? r?.ToString() ?? "" : "";
            if (!string.IsNullOrWhiteSpace(roleName)) cboRole.Text = roleName;
            if (data.TryGetValue("Hoạt động", out var a) && bool.TryParse(a?.ToString(), out var active)) chkActive.Checked = active;
        }

        lblPasswordHint.Text = isEdit
            ? "Để trống nếu không đổi mật khẩu"
            : "Mật khẩu tối thiểu 6 ký tự";

        FormClosing += AccountDialog_FormClosing;
    }

    private void AccountDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK) return;
        if (!ValidateInput()) e.Cancel = true;
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(UsernameValue))
        {
            MessageBox.Show("Tên đăng nhập không được để trống.");
            txtUsername.Focus();
            return false;
        }

        if (UsernameValue.Length < 3)
        {
            MessageBox.Show("Tên đăng nhập phải có ít nhất 3 ký tự.");
            txtUsername.Focus();
            return false;
        }

        if (!_isEdit && string.IsNullOrWhiteSpace(PasswordValue))
        {
            MessageBox.Show("Vui lòng nhập mật khẩu.");
            txtPassword.Focus();
            return false;
        }

        if (!string.IsNullOrWhiteSpace(PasswordValue) && PasswordValue.Length < 6)
        {
            MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.");
            txtPassword.Focus();
            return false;
        }

        if (PasswordValue != ConfirmPasswordValue)
        {
            MessageBox.Show("Xác nhận mật khẩu chưa khớp.");
            txtConfirmPassword.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(FullNameValue))
        {
            MessageBox.Show("Họ tên không được để trống.");
            txtFullName.Focus();
            return false;
        }

        if (!string.IsNullOrWhiteSpace(EmailValue) && !Regex.IsMatch(EmailValue, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            MessageBox.Show("Email không đúng định dạng.");
            txtEmail.Focus();
            return false;
        }

        if (!string.IsNullOrWhiteSpace(PhoneValue) && !Regex.IsMatch(PhoneValue, @"^[0-9]{9,11}$"))
        {
            MessageBox.Show("Số điện thoại phải gồm 9-11 chữ số.");
            txtPhone.Focus();
            return false;
        }

        if (RoleId <= 0)
        {
            MessageBox.Show("Vui lòng chọn vai trò.");
            cboRole.Focus();
            return false;
        }

        return true;
    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }
}
