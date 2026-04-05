namespace LibraryManagement.WinForms.Forms;

public partial class ChangePasswordDialog : Form
{
    public string NewPassword => txtNew.Text;

    public ChangePasswordDialog()
    {
        InitializeComponent();
    }

    private void btnOk_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNew.Text) || txtNew.Text.Length < 6)
        {
            MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.");
            DialogResult = DialogResult.None;
            return;
        }
        if (txtNew.Text != txtConfirm.Text)
        {
            MessageBox.Show("Mật khẩu xác nhận không khớp.");
            DialogResult = DialogResult.None;
        }
    }

    private void txtConfirm_TextChanged(object sender, EventArgs e)
    {

    }

    private void ChangePasswordDialog_Load(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}
