using System.Data;

namespace LibraryManagement.WinForms.Forms;

public partial class BookDialog : Form
{
    public string TitleValue => txtTitle.Text.Trim();
    public string Author => txtAuthor.Text.Trim();
    public string Publisher => txtPublisher.Text.Trim();
    public int? PublishYear => nudYear.Value > 0 ? (int)nudYear.Value : null;
    public string Isbn => txtIsbn.Text.Trim();
    public string LanguageText => txtLanguage.Text.Trim();
    public decimal Price => nudPrice.Value;

    public int CategoryId
    {
        get
        {
            if (cboCategory.SelectedValue == null || cboCategory.SelectedValue == DBNull.Value)
                return 0;

            if (int.TryParse(cboCategory.SelectedValue.ToString(), out int id))
                return id;

            return 0;
        }
    }

    public int Copies => (int)nudCopies.Value;
    public string DescriptionText => txtDescription.Text.Trim();

    public BookDialog(string title, DataTable categories, IDictionary<string, object?>? data = null, bool isEdit = false)
    {
        InitializeComponent();
        Text = title;

        var dt = categories.Clone();

        foreach (DataRow r in categories.Rows)
        {
            var ten = r["ten"]?.ToString()?.Trim() ?? "";
            if (!string.IsNullOrWhiteSpace(ten))
            {
                dt.ImportRow(r);
            }
        }

        var row = dt.NewRow();
        row["danh_muc_id"] = DBNull.Value;
        row["ten"] = "-- Chọn danh mục --";
        dt.Rows.InsertAt(row, 0);

        cboCategory.DataSource = dt;
        cboCategory.DisplayMember = "ten";
        cboCategory.ValueMember = "danh_muc_id";
        cboCategory.SelectedIndex = 0;
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

        if (data != null)
        {
            txtTitle.Text = data.TryGetValue("Tiêu đề", out var tieuDe)
                ? tieuDe?.ToString() ?? ""
                : "";

            txtAuthor.Text = data.TryGetValue("Tác giả", out var tacGia)
                ? tacGia?.ToString() ?? ""
                : "";

            txtPublisher.Text = data.TryGetValue("NXB", out var nxb)
                ? nxb?.ToString() ?? ""
                : "";

            if (data.TryGetValue("Năm XB", out var namXbObj) &&
                int.TryParse(namXbObj?.ToString(), out var year))
            {
                if (year >= nudYear.Minimum && year <= nudYear.Maximum)
                    nudYear.Value = year;
            }

            txtIsbn.Text = data.TryGetValue("ISBN", out var isbnObj)
                ? isbnObj?.ToString() ?? ""
                : "";

            txtLanguage.Text = data.TryGetValue("Ngôn ngữ", out var ngonNguObj)
                ? ngonNguObj?.ToString() ?? "Tiếng Việt"
                : "Tiếng Việt";

            if (data.TryGetValue("Giá bìa", out var giaBiaObj) &&
                decimal.TryParse(giaBiaObj?.ToString(), out var price))
            {
                if (price >= nudPrice.Minimum && price <= nudPrice.Maximum)
                    nudPrice.Value = price;
            }

            var tenDanhMuc = data.TryGetValue("Danh mục", out var danhMucObj)
                ? danhMucObj?.ToString() ?? ""
                : "";

            if (!string.IsNullOrWhiteSpace(tenDanhMuc))
            {
                for (int i = 0; i < cboCategory.Items.Count; i++)
                {
                    if (((DataRowView)cboCategory.Items[i])["ten"]?.ToString() == tenDanhMuc)
                    {
                        cboCategory.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (data.TryGetValue("Tổng bản sao", out var tongBanSaoObj) &&
                int.TryParse(tongBanSaoObj?.ToString(), out var copies) &&
                copies >= nudCopies.Minimum && copies <= nudCopies.Maximum)
            {
                nudCopies.Value = copies;
            }

            nudCopies.Enabled = true;
        }

        lblNote.Text = isEdit
            ? "Bạn có thể tăng/giảm số bản sao. Hệ thống chỉ cho giảm nếu còn đủ bản sao đang sẵn sàng."
            : "Khi thêm sách mới, hệ thống tạo mã bản sao tự động.";

        FormClosing += BookDialog_FormClosing;
    }

    private void BookDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK)
            return;

        if (!ValidateInput())
            e.Cancel = true;
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(TitleValue))
        {
            MessageBox.Show("Tiêu đề không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTitle.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(Author))
        {
            MessageBox.Show("Tác giả không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtAuthor.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(Publisher))
        {
            MessageBox.Show("Nhà xuất bản không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPublisher.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(Isbn))
        {
            MessageBox.Show("ISBN không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtIsbn.Focus();
            return false;
        }

        if (CategoryId <= 0)
        {
            MessageBox.Show("Vui lòng chọn danh mục.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboCategory.Focus();
            return false;
        }

        if (Price < 0)
        {
            MessageBox.Show("Giá bìa phải >= 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudPrice.Focus();
            return false;
        }

        if (nudCopies.Enabled && Copies <= 0)
        {
            MessageBox.Show("Tổng bản sao phải lớn hơn 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudCopies.Focus();
            return false;
        }

        return true;
    }
}