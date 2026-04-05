using System.Text.RegularExpressions;

namespace LibraryManagement.WinForms.Forms;

public partial class ReaderDialog : Form
{
    public string HoTen => txtHoTen.Text.Trim();
    public string MaSo => txtMaSo.Text.Trim();
    public string Loai => cboLoai.SelectedItem?.ToString() ?? "SINH_VIEN";
    public string GioiTinh => cboGioiTinh.SelectedItem?.ToString() ?? "NAM";
    public DateTime? NgaySinh => dtNgaySinh.Checked ? dtNgaySinh.Value.Date : null;
    public string EmailValue => txtEmail.Text.Trim();
    public string Sdt => txtSdt.Text.Trim();
    public string DiaChi => txtDiaChi.Text.Trim();
    public bool IsActive => chkActive.Checked;

    public ReaderDialog(string title, IDictionary<string, object?>? data = null)
    {
        InitializeComponent();
        Text = title;

        cboLoai.Items.Clear();
        cboLoai.Items.AddRange(["HOC_SINH", "SINH_VIEN", "GIAO_VIEN", "KHACH"]);

        cboGioiTinh.Items.Clear();
        cboGioiTinh.Items.AddRange(["NAM", "NU", "KHAC"]);

        cboLoai.SelectedIndex = 1;
        cboGioiTinh.SelectedIndex = 0;

        dtNgaySinh.Checked = false;

        if (data != null)
        {
            txtHoTen.Text = data.TryGetValue("Họ tên", out var hoTen) ? hoTen?.ToString() ?? "" : "";
            txtMaSo.Text = data.TryGetValue("Mã số", out var maSo) ? maSo?.ToString() ?? "" : "";

            cboLoai.SelectedItem = data.TryGetValue("Loại", out var loai) ? loai?.ToString() ?? "SINH_VIEN" : "SINH_VIEN";
            if (cboLoai.SelectedIndex < 0) cboLoai.SelectedIndex = 1;

            cboGioiTinh.SelectedItem = data.TryGetValue("Giới tính", out var gioiTinh) ? gioiTinh?.ToString() ?? "NAM" : "NAM";
            if (cboGioiTinh.SelectedIndex < 0) cboGioiTinh.SelectedIndex = 0;

            if (data.TryGetValue("Ngày sinh", out var ngaySinhObj) &&
                DateTime.TryParse(ngaySinhObj?.ToString(), out var ns))
            {
                dtNgaySinh.Value = ns;
                dtNgaySinh.Checked = true;
            }

            txtEmail.Text = data.TryGetValue("Email", out var email) ? email?.ToString() ?? "" : "";
            txtSdt.Text = data.TryGetValue("SĐT", out var sdt) ? sdt?.ToString() ?? "" : "";
            txtDiaChi.Text = data.TryGetValue("Địa chỉ", out var diaChi) ? diaChi?.ToString() ?? "" : "";

            if (data.TryGetValue("Hoạt động", out var activeObj))
            {
                bool.TryParse(activeObj?.ToString(), out var active);
                chkActive.Checked = active;
            }
        }

        FormClosing += ReaderDialog_FormClosing;
    }

    private void ReaderDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK)
            return;

        if (!ValidateInput())
        {
            e.Cancel = true;
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(HoTen))
        {
            MessageBox.Show("Họ tên không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(MaSo))
        {
            MessageBox.Show("Mã số không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaSo.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(Loai))
        {
            MessageBox.Show("Vui lòng chọn loại bạn đọc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboLoai.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(GioiTinh))
        {
            MessageBox.Show("Vui lòng chọn giới tính.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboGioiTinh.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(EmailValue))
        {
            MessageBox.Show("Email không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return false;
        }

        if (!Regex.IsMatch(EmailValue, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            MessageBox.Show("Email không đúng định dạng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(Sdt))
        {
            MessageBox.Show("Số điện thoại không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSdt.Focus();
            return false;
        }

        if (!Regex.IsMatch(Sdt, @"^\d{10}$"))
        {
            MessageBox.Show("Số điện thoại phải gồm 10 chữ số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSdt.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(DiaChi))
        {
            MessageBox.Show("Địa chỉ không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtDiaChi.Focus();
            return false;
        }

        if (!NgaySinh.HasValue)
        {
            MessageBox.Show("Vui lòng chọn ngày sinh.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtNgaySinh.Focus();
            return false;
        }

        if (NgaySinh.Value.Date > DateTime.Today)
        {
            MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtNgaySinh.Focus();
            return false;
        }

        return true;
    }
}