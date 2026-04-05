using LibraryManagement.WinForms.Helpers;
using LibraryManagement.WinForms.Models;
using LibraryManagement.WinForms.Services;
using System.Data;
using System.Text.RegularExpressions;

namespace LibraryManagement.WinForms.Forms;

public partial class MainForm : Form
{
    private readonly UserSession _session;
    private readonly LibraryService _service = new();
    private readonly LookupService _lookup = new();
    private readonly AuthService _auth = new();
    private readonly TabControl _tabs = new()
    {
        Dock = DockStyle.Fill,
        Appearance = TabAppearance.FlatButtons,
        ItemSize = new Size(0, 1),
        SizeMode = TabSizeMode.Fixed,
        Multiline = true
    };
    private readonly DataGridView _gridReaders = new();
    private readonly DataGridView _gridAccounts = new();
    private readonly DataGridView _gridCategories = new();
    private readonly DataGridView _gridBooks = new();
    private readonly DataGridView _gridLoans = new();
    private readonly DataGridView _gridLoanDetails = new();
    private readonly DataGridView _gridFines = new();
    private readonly DataGridView _gridTopBooks = new();

    private readonly TextBox _txtReaderSearch = new() { Width = 240 };
    private readonly TextBox _txtAccountSearch = new() { Width = 240 };
    private readonly TextBox _txtCategorySearch = new() { Width = 240 };
    private readonly TextBox _txtBookSearch = new() { Width = 240 };
    private readonly TextBox _txtLoanSearch = new() { Width = 240 };
    private readonly ComboBox _cboBookCategory = new() { Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox _cboLoanStatus = new() { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly FlowLayoutPanel _summaryPanel = new() { Dock = DockStyle.Top, Height = 120, Padding = new Padding(10) };
    private Label _lblLoanDetailTitle = new();
    private Label _lblLoanDetailHint = new();
    private Button _btnReturnLoanDetail = new();
    private Button _btnEditLoan = new();
    public MainForm(UserSession session)
    {
        _session = session;
        InitializeComponent();
        Text = $"{AppSettings.AppTitle} - {_session.FullName} ({_session.RoleName})";
        lblUser.Text = $"{_session.FullName} | {_session.RoleName}";

        _tabs.Dock = DockStyle.Fill;
        panelRoot.Controls.Add(_tabs);
        panelRoot.Controls.SetChildIndex(_tabs, 0);

        btnNavReaders.Click += (_, _) => _tabs.SelectedIndex = 0;
        btnNavAccounts.Click += (_, _) => _tabs.SelectedIndex = 1;
        btnNavCategories.Click += (_, _) => _tabs.SelectedIndex = 2;
        btnNavBooks.Click += (_, _) => _tabs.SelectedIndex = 3;
        btnNavLoans.Click += (_, _) => _tabs.SelectedIndex = 4;
        btnNavFines.Click += (_, _) => _tabs.SelectedIndex = 5;
        btnNavStatistics.Click += (_, _) => _tabs.SelectedIndex = 6;
        btnLogout.Click += (_, _) => Close();
        btnChangePassword.Click += BtnChangePassword_Click;

        _tabs.TabPages.Add(CreateReadersTab());
        _tabs.TabPages.Add(CreateAccountsTab());
        _tabs.TabPages.Add(CreateCategoriesTab());
        _tabs.TabPages.Add(CreateBooksTab());
        _tabs.TabPages.Add(CreateLoansTab());
        _tabs.TabPages.Add(CreateFinesTab());
        _tabs.TabPages.Add(CreateDashboardTab());

        Load += (_, _) =>
        {
            try
            {
                LoadCategoryLookup();
                LoadDashboard();
                LoadReaders();
                LoadAccounts();
                LoadCategories();
                LoadBooks();
                LoadLoans();
                LoadFines();
                LoadTopBooks();
                ApplyPermissions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
    }

    private TabPage CreateReadersTab()
    {
        UiHelper.ApplyGridTheme(_gridReaders);
        _gridReaders.SelectionChanged += (_, _) => { };

        var tab = new TabPage("Bạn đọc") { Padding = new Padding(8) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

        var top = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnSearch = UiHelper.CreateActionButton("Tìm", Color.RoyalBlue);
        var btnReload = UiHelper.CreateActionButton("Tải lại", Color.DimGray);
        var btnAdd = UiHelper.CreateActionButton("Thêm", Color.SeaGreen);
        var btnEdit = UiHelper.CreateActionButton("Sửa", Color.Goldenrod);
        var btnDelete = UiHelper.CreateActionButton("Xóa", Color.Firebrick);

        btnSearch.Click += (_, _) => LoadReaders();
        btnReload.Click += (_, _) => { _txtReaderSearch.Clear(); LoadReaders(); };
        btnAdd.Click += BtnAddReader_Click;
        btnEdit.Click += BtnEditReader_Click;
        btnDelete.Click += BtnDeleteReader_Click;

        btnAdd.Name = "BAN_DOC_THEM";
        btnEdit.Name = "BAN_DOC_SUA";
        btnDelete.Name = "BAN_DOC_XOA";

        top.Controls.Add(new Label { Text = "Tìm theo tên/SĐT/email:", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        top.Controls.Add(_txtReaderSearch);
        top.Controls.Add(btnSearch);
        top.Controls.Add(btnReload);
        top.Controls.Add(btnAdd);
        top.Controls.Add(btnEdit);
        top.Controls.Add(btnDelete);

        var bottom = new Panel { Dock = DockStyle.Fill };
        bottom.Controls.Add(new Label
        {
            Text = "Mẹo: click chọn 1 bạn đọc để sửa/xóa hoặc xem số sách đang mượn ngay trên lưới dữ liệu.",
            Dock = DockStyle.Fill,
            ForeColor = Color.DimGray,
            Font = new Font("Segoe UI", 10, FontStyle.Italic)
        });

        layout.Controls.Add(top, 0, 0);
        layout.Controls.Add(_gridReaders, 0, 1);
        layout.Controls.Add(bottom, 0, 2);
        tab.Controls.Add(layout);
        return tab;
    }

    private TabPage CreateAccountsTab()
    {
        UiHelper.ApplyGridTheme(_gridAccounts);

        var tab = new TabPage("Tài khoản") { Padding = new Padding(8) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

        var top = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnSearch = UiHelper.CreateActionButton("Tìm", Color.RoyalBlue);
        var btnReload = UiHelper.CreateActionButton("Tải lại", Color.DimGray);
        var btnAdd = UiHelper.CreateActionButton("Thêm", Color.SeaGreen);
        var btnEdit = UiHelper.CreateActionButton("Sửa", Color.Goldenrod);
        var btnDelete = UiHelper.CreateActionButton("Xóa", Color.Firebrick);

        btnSearch.Click += (_, _) => LoadAccounts();
        btnReload.Click += (_, _) => { _txtAccountSearch.Clear(); LoadAccounts(); };
        btnAdd.Click += BtnAddAccount_Click;
        btnEdit.Click += BtnEditAccount_Click;
        btnDelete.Click += BtnDeleteAccount_Click;

        btnAdd.Name = "TAI_KHOAN_THEM";
        btnEdit.Name = "TAI_KHOAN_SUA";
        btnDelete.Name = "TAI_KHOAN_XOA";

        top.Controls.Add(new Label { Text = "Tìm theo tên đăng nhập/họ tên/email:", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        top.Controls.Add(_txtAccountSearch);
        top.Controls.Add(btnSearch);
        top.Controls.Add(btnReload);
        top.Controls.Add(btnAdd);
        top.Controls.Add(btnEdit);
        top.Controls.Add(btnDelete);

        var bottom = new Panel { Dock = DockStyle.Fill };
        bottom.Controls.Add(new Label
        {
            Text = "Chỉ ADMIN mới có quyền thêm, sửa, xóa tài khoản. STAFF sẽ không thao tác được mục này.",
            Dock = DockStyle.Fill,
            ForeColor = Color.DimGray,
            Font = new Font("Segoe UI", 10, FontStyle.Italic)
        });

        layout.Controls.Add(top, 0, 0);
        layout.Controls.Add(_gridAccounts, 0, 1);
        layout.Controls.Add(bottom, 0, 2);
        tab.Controls.Add(layout);
        return tab;
    }

    private TabPage CreateCategoriesTab()
    {
        UiHelper.ApplyGridTheme(_gridCategories);

        var tab = new TabPage("Danh mục") { Padding = new Padding(8) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var top = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnSearch = UiHelper.CreateActionButton("Tìm", Color.RoyalBlue);
        var btnReload = UiHelper.CreateActionButton("Tải lại", Color.DimGray);
        var btnAdd = UiHelper.CreateActionButton("Thêm", Color.SeaGreen);
        var btnEdit = UiHelper.CreateActionButton("Sửa", Color.Goldenrod);
        var btnDelete = UiHelper.CreateActionButton("Xóa", Color.Firebrick);

        btnSearch.Click += (_, _) => LoadCategories();
        btnReload.Click += (_, _) => { _txtCategorySearch.Clear(); LoadCategories(); };
        btnAdd.Click += BtnAddCategory_Click;
        btnEdit.Click += BtnEditCategory_Click;
        btnDelete.Click += BtnDeleteCategory_Click;

        btnAdd.Name = "DANH_MUC_THEM";
        btnEdit.Name = "DANH_MUC_SUA";
        btnDelete.Name = "DANH_MUC_XOA";

        top.Controls.Add(new Label { Text = "Tìm danh mục:", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        top.Controls.Add(_txtCategorySearch);
        top.Controls.Add(btnSearch);
        top.Controls.Add(btnReload);
        top.Controls.Add(btnAdd);
        top.Controls.Add(btnEdit);
        top.Controls.Add(btnDelete);

        layout.Controls.Add(top, 0, 0);
        layout.Controls.Add(_gridCategories, 0, 1);
        tab.Controls.Add(layout);
        return tab;
    }

    private TabPage CreateBooksTab()
    {
        UiHelper.ApplyGridTheme(_gridBooks);

        var tab = new TabPage("Sách") { Padding = new Padding(8) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var top = new FlowLayoutPanel { Dock = DockStyle.Fill };
        _cboBookCategory.Items.Add("Tất cả danh mục");
        _cboBookCategory.SelectedIndex = 0;

        var btnSearch = UiHelper.CreateActionButton("Tìm", Color.RoyalBlue);
        var btnReload = UiHelper.CreateActionButton("Tải lại", Color.DimGray);
        var btnAdd = UiHelper.CreateActionButton("Thêm", Color.SeaGreen);
        var btnEdit = UiHelper.CreateActionButton("Sửa", Color.Goldenrod);
        var btnDelete = UiHelper.CreateActionButton("Xóa", Color.Firebrick);

        btnSearch.Click += (_, _) => LoadBooks();
        btnReload.Click += (_, _) => { _txtBookSearch.Clear(); _cboBookCategory.SelectedIndex = 0; LoadBooks(); };
        btnAdd.Click += BtnAddBook_Click;
        btnEdit.Click += BtnEditBook_Click;
        btnDelete.Click += BtnDeleteBook_Click;

        btnAdd.Name = "SACH_THEM";
        btnEdit.Name = "SACH_SUA";
        btnDelete.Name = "SACH_XOA";

        top.Controls.Add(new Label { Text = "Tìm sách:", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        top.Controls.Add(_txtBookSearch);
        top.Controls.Add(new Label { Text = "Danh mục:", AutoSize = true, Margin = new Padding(10, 12, 3, 3) });
        top.Controls.Add(_cboBookCategory);
        top.Controls.Add(btnSearch);
        top.Controls.Add(btnReload);
        top.Controls.Add(btnAdd);
        top.Controls.Add(btnEdit);
        top.Controls.Add(btnDelete);

        layout.Controls.Add(top, 0, 0);
        layout.Controls.Add(_gridBooks, 0, 1);
        tab.Controls.Add(layout);
        return tab;
    }

    private TabPage CreateLoansTab()
    {
        UiHelper.ApplyGridTheme(_gridLoans);
        UiHelper.ApplyGridTheme(_gridLoanDetails);
        _gridLoans.SelectionChanged += (_, _) =>
        {
            LoadLoanDetails();
            UpdateLoanDetailUiState();
        };

        var tab = new TabPage("Phiếu mượn") { Padding = new Padding(8) };
        var main = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 320
        };

        var topContainer = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
        topContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        topContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        _cboLoanStatus.Items.Clear();
        _cboLoanStatus.Items.AddRange(new object[] { "", "DANG_MUON", "DA_TRA", "QUA_HAN", "HUY" });
        _cboLoanStatus.SelectedIndex = 0;

        var tools = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            WrapContents = false,
            AutoScroll = true
        };

        var btnSearch = UiHelper.CreateActionButton("Tìm", Color.RoyalBlue);
        var btnReload = UiHelper.CreateActionButton("Tải lại", Color.DimGray);
        var btnCreate = UiHelper.CreateActionButton("Lập phiếu", Color.FromArgb(0, 153, 255));
        _btnEditLoan = UiHelper.CreateActionButton("Sửa phiếu", Color.Goldenrod);

        btnCreate.Name = "PHIEU_MUON_THEM";
        _btnEditLoan.Name = "PHIEU_MUON_SUA";

        btnSearch.Click += (_, _) => LoadLoans();
        btnReload.Click += (_, _) =>
        {
            _txtLoanSearch.Clear();
            _cboLoanStatus.SelectedIndex = 0;
            LoadLoans();
            LoadLoanDetails();
            UpdateLoanDetailUiState();
        };
        btnCreate.Click += BtnCreateLoan_Click;
        _btnEditLoan.Click += BtnEditLoan_Click;

        tools.Controls.Add(new Label
        {
            Text = "Tìm theo mã phiếu/bạn đọc:",
            AutoSize = true,
            Margin = new Padding(3, 12, 3, 3)
        });
        tools.Controls.Add(_txtLoanSearch);

        tools.Controls.Add(new Label
        {
            Text = "Trạng thái:",
            AutoSize = true,
            Margin = new Padding(10, 12, 3, 3)
        });
        tools.Controls.Add(_cboLoanStatus);

        tools.Controls.Add(btnSearch);
        tools.Controls.Add(btnReload);
        tools.Controls.Add(btnCreate);
        tools.Controls.Add(_btnEditLoan);

        topContainer.Controls.Add(tools, 0, 0);
        topContainer.Controls.Add(_gridLoans, 0, 1);

        var detailContainer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            BackColor = Color.White
        };
        detailContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        detailContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var detailHeader = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(245, 247, 250),
            Padding = new Padding(10, 6, 10, 6)
        };

        _lblLoanDetailTitle = new Label
        {
            Text = "Chi tiết phiếu mượn",
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(40, 40, 40),
            Location = new Point(10, 8)
        };

        _lblLoanDetailHint = new Label
        {
            Text = "Chọn một phiếu mượn ở bảng trên để xem chi tiết.",
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Italic),
            ForeColor = Color.DimGray,
            Location = new Point(10, 28)
        };

        _btnReturnLoanDetail = UiHelper.CreateActionButton("Trả sách đã chọn", Color.MediumPurple);
        _btnReturnLoanDetail.Name = "TRA_SACH";
        _btnReturnLoanDetail.Size = new Size(140, 34);
        _btnReturnLoanDetail.Location = new Point(1250, 8);
        _btnReturnLoanDetail.Click += BtnReturn_Click;

        detailHeader.Controls.Add(_lblLoanDetailTitle);
        detailHeader.Controls.Add(_lblLoanDetailHint);
        detailHeader.Controls.Add(_btnReturnLoanDetail);

        detailHeader.Resize += (_, _) =>
        {
            _btnReturnLoanDetail.Left = detailHeader.ClientSize.Width - _btnReturnLoanDetail.Width - 10;
            _btnReturnLoanDetail.Top = 8;
        };

        _gridLoanDetails.Dock = DockStyle.Fill;
        _gridLoanDetails.BackgroundColor = Color.White;

        detailContainer.Controls.Add(detailHeader, 0, 0);
        detailContainer.Controls.Add(_gridLoanDetails, 0, 1);

        main.Panel1.Controls.Add(topContainer);
        main.Panel2.Controls.Add(detailContainer);

        tab.Controls.Add(main);
        return tab;
    }

    private TabPage CreateFinesTab()
    {
        UiHelper.ApplyGridTheme(_gridFines);

        var tab = new TabPage("Tiền phạt") { Padding = new Padding(8) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var top = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnReload = UiHelper.CreateActionButton("Tải lại", Color.DimGray);
        var btnPaid = UiHelper.CreateActionButton("Đánh dấu đã thu", Color.SeaGreen);

        btnPaid.Name = "TIEN_PHAT_SUA";
        btnReload.Click += (_, _) => LoadFines();
        btnPaid.Click += BtnMarkFinePaid_Click;

        top.Controls.Add(btnReload);
        top.Controls.Add(btnPaid);

        layout.Controls.Add(top, 0, 0);
        layout.Controls.Add(_gridFines, 0, 1);

        tab.Controls.Add(layout);
        return tab;
    }

    private TabPage CreateDashboardTab()
    {
        UiHelper.ApplyGridTheme(_gridTopBooks);

        var tab = new TabPage("Thống kê") { Padding = new Padding(8) };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var topTools = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnReload = UiHelper.CreateActionButton("Làm mới thống kê", Color.RoyalBlue);
        btnReload.Click += (_, _) =>
        {
            LoadDashboard();
            LoadTopBooks();
        };
        topTools.Controls.Add(btnReload);

        layout.Controls.Add(_summaryPanel, 0, 0);
        layout.Controls.Add(topTools, 0, 1);
        layout.Controls.Add(_gridTopBooks, 0, 2);
        tab.Controls.Add(layout);
        return tab;
    }

    private void LoadDashboard()
    {
        _summaryPanel.Controls.Clear();
        var dt = _service.GetDashboardSummary();
        if (dt.Rows.Count == 0) return;
        var row = dt.Rows[0];

        _summaryPanel.Controls.Add(UiHelper.CreateSummaryCard("Tổng đầu sách", row["tong_sach"].ToString() ?? "0", Color.FromArgb(0, 102, 204)));
        _summaryPanel.Controls.Add(UiHelper.CreateSummaryCard("Tổng bản sao", row["tong_ban_sao"].ToString() ?? "0", Color.FromArgb(0, 153, 153)));
        _summaryPanel.Controls.Add(UiHelper.CreateSummaryCard("Bạn đọc hoạt động", row["tong_ban_doc"].ToString() ?? "0", Color.FromArgb(76, 175, 80)));
        _summaryPanel.Controls.Add(UiHelper.CreateSummaryCard("Phiếu đang mở", row["phieu_dang_mo"].ToString() ?? "0", Color.FromArgb(255, 152, 0)));
        _summaryPanel.Controls.Add(UiHelper.CreateSummaryCard("Phiếu quá hạn", row["phieu_qua_han"].ToString() ?? "0", Color.FromArgb(233, 30, 99)));
        _summaryPanel.Controls.Add(UiHelper.CreateSummaryCard("Phạt chưa thu", $"{Convert.ToDecimal(row["tong_tien_phat_chua_thu"]):N0} đ", Color.FromArgb(121, 85, 72)));
    }

    private void LoadReaders()
    {
        _gridReaders.DataSource = _service.GetReaders(_txtReaderSearch.Text.Trim());

        if (_gridReaders.Columns.Contains("Hạn thẻ"))
            _gridReaders.Columns["Hạn thẻ"].Visible = false;

        if (_gridReaders.Columns.Contains("han_the"))
            _gridReaders.Columns["han_the"].Visible = false;
    }
    private void LoadAccounts() => _gridAccounts.DataSource = _auth.GetAccounts(_txtAccountSearch.Text.Trim());
    private void LoadCategories() => _gridCategories.DataSource = _service.GetCategories(_txtCategorySearch.Text.Trim());
    private void LoadBooks()
    {
        int? categoryId = null;
        if (_cboBookCategory.SelectedValue is int cid) categoryId = cid;
        _gridBooks.DataSource = _service.GetBooks(_txtBookSearch.Text.Trim(), categoryId);
    }
    private void LoadLoans() => _gridLoans.DataSource = _service.GetLoans(_txtLoanSearch.Text.Trim(), _cboLoanStatus.Text);
    private void LoadLoanDetails()
    {
        if (_gridLoans.CurrentRow is null)
        {
            _gridLoanDetails.DataSource = null;
            return;
        }

        int id = Convert.ToInt32(_gridLoans.CurrentRow.Cells["ID"].Value);
        _gridLoanDetails.DataSource = _service.GetLoanDetails(id);
        if (_gridLoanDetails.Columns.Contains("ID"))
            _gridLoanDetails.Columns["ID"].HeaderText = "ID chi tiết";

        if (_gridLoanDetails.Columns.Contains("Lúc mượn"))
            _gridLoanDetails.Columns["Lúc mượn"].HeaderText = "Tình trạng lúc mượn";

        if (_gridLoanDetails.Columns.Contains("Lúc trả"))
            _gridLoanDetails.Columns["Lúc trả"].HeaderText = "Tình trạng lúc trả";

        foreach (DataGridViewRow row in _gridLoanDetails.Rows)
        {
            var thoiGianTra = row.Cells["Thời gian trả"].Value?.ToString();

            if (!string.IsNullOrWhiteSpace(thoiGianTra))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233);
                row.DefaultCellStyle.ForeColor = Color.DarkGreen;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        UpdateLoanDetailUiState();
    }
    private void UpdateLoanDetailUiState()
    {
        if (_gridLoans.CurrentRow is null)
        {
            _lblLoanDetailTitle.Text = "Chi tiết phiếu mượn";
            _lblLoanDetailHint.Text = "Chọn một phiếu mượn ở bảng trên để xem chi tiết.";
            _btnReturnLoanDetail.Enabled = false;
            _btnEditLoan.Enabled = false;
            return;
        }

        string status = _gridLoans.CurrentRow.Cells["Trạng thái"].Value?.ToString() ?? "";
        string maSo = _gridLoans.CurrentRow.Cells["Mã số"].Value?.ToString() ?? "";
        string banDoc = _gridLoans.CurrentRow.Cells["Bạn đọc"].Value?.ToString() ?? "";

        _lblLoanDetailTitle.Text = $"Chi tiết phiếu mượn - {banDoc} ({maSo})";
        _lblLoanDetailHint.Left = 10;
        _lblLoanDetailHint.Top = 28;

        bool isBorrowing = status == "DANG_MUON" || status == "QUA_HAN";
        bool isReturned = status == "DA_TRA";
        bool isCanceled = status == "HUY";

        _btnReturnLoanDetail.Enabled = isBorrowing;
        _btnEditLoan.Enabled = !isCanceled;

        if (status == "DANG_MUON")
        {
            _lblLoanDetailHint.Text = "Phiếu đang mượn. Bạn có thể sửa hạn trả/gia hạn hoặc trả sách.";
            _lblLoanDetailHint.ForeColor = Color.DodgerBlue;
        }
        else if (status == "QUA_HAN")
        {
            _lblLoanDetailHint.Text = "Phiếu đã quá hạn. Nên trả sách hoặc điều chỉnh hạn trả nếu cần.";
            _lblLoanDetailHint.ForeColor = Color.OrangeRed;
        }
        else if (isReturned)
        {
            _lblLoanDetailHint.Text = "Phiếu đã trả xong. Bạn vẫn có thể sửa ngày hẹn trả hoặc số lần gia hạn nếu cần chỉnh lại hồ sơ.";
            _lblLoanDetailHint.ForeColor = Color.SeaGreen;
        }
        else if (isCanceled)
        {
            _lblLoanDetailHint.Text = "Phiếu đã hủy. Chỉ xem thông tin.";
            _lblLoanDetailHint.ForeColor = Color.Gray;
        }
        else
        {
            _lblLoanDetailHint.Text = "Chọn một chi tiết để xem thêm thông tin.";
            _lblLoanDetailHint.ForeColor = Color.DimGray;
        }
    }
    private void LoadFines() => _gridFines.DataSource = _service.GetFines();
    private void LoadTopBooks() => _gridTopBooks.DataSource = _service.GetTopBorrowedBooks();

    private void LoadCategoryLookup()
    {
        var dt = _lookup.GetCategories();
        var allRow = dt.NewRow();
        allRow["danh_muc_id"] = DBNull.Value;
        allRow["ten"] = "Tất cả danh mục";
        dt.Rows.InsertAt(allRow, 0);

        _cboBookCategory.DataSource = dt;
        _cboBookCategory.DisplayMember = "ten";
        _cboBookCategory.ValueMember = "danh_muc_id";
    }

    private void BtnAddAccount_Click(object? sender, EventArgs e)
    {
        using var dlg = new AccountDialog("Thêm tài khoản", _auth.GetRoles());
        if (dlg.ShowDialog() != DialogResult.OK) return;

        SafeExecute(() =>
        {
            _auth.AddAccount(dlg.UsernameValue, dlg.PasswordValue, dlg.FullNameValue, dlg.EmailValue, dlg.PhoneValue, dlg.RoleId, dlg.IsActive);
            LoadAccounts();
            MessageBox.Show("Đã thêm tài khoản.");
        });
    }

    private void BtnEditAccount_Click(object? sender, EventArgs e)
    {
        if (_gridAccounts.CurrentRow is null)
        {
            MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
            return;
        }

        var row = _gridAccounts.CurrentRow;
        var data = new Dictionary<string, object?>
        {
            ["Tên đăng nhập"] = row.Cells["Tên đăng nhập"].Value,
            ["Họ tên"] = row.Cells["Họ tên"].Value,
            ["Email"] = row.Cells["Email"].Value,
            ["SĐT"] = row.Cells["SĐT"].Value,
            ["Vai trò"] = row.Cells["Vai trò"].Value,
            ["Hoạt động"] = row.Cells["Hoạt động"].Value
        };

        using var dlg = new AccountDialog("Sửa tài khoản", _auth.GetRoles(), data, true);
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int id = Convert.ToInt32(row.Cells["ID"].Value);
        SafeExecute(() =>
        {
            _auth.UpdateAccount(id, dlg.UsernameValue, dlg.PasswordValue, dlg.FullNameValue, dlg.EmailValue, dlg.PhoneValue, dlg.RoleId, dlg.IsActive);
            LoadAccounts();
            MessageBox.Show("Đã cập nhật tài khoản.");
        });
    }

    private void BtnDeleteAccount_Click(object? sender, EventArgs e)
    {
        if (_gridAccounts.CurrentRow is null) return;
        int id = Convert.ToInt32(_gridAccounts.CurrentRow.Cells["ID"].Value);
        string username = _gridAccounts.CurrentRow.Cells["Tên đăng nhập"].Value?.ToString() ?? "";

        if (id == _session.UserId)
        {
            MessageBox.Show("Không thể tự xóa chính tài khoản đang đăng nhập.");
            return;
        }

        if (MessageBox.Show($"Bạn có chắc muốn khóa tài khoản '{username}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        SafeExecute(() =>
        {
            _auth.DeleteAccount(id);
            LoadAccounts();
            MessageBox.Show("Đã khóa tài khoản.");
        });
    }

    private void BtnAddCategory_Click(object? sender, EventArgs e)
    {
        using var dlg = new CategoryDialog("Thêm danh mục");
        if (dlg.ShowDialog() != DialogResult.OK) return;
        if (string.IsNullOrWhiteSpace(dlg.CategoryName))
        {
            MessageBox.Show("Tên danh mục không được trống.");
            return;
        }

        SafeExecute(() =>
        {
            _service.AddCategory(dlg.CategoryName, dlg.DescriptionText);
            LoadCategoryLookup();
            LoadCategories();
            MessageBox.Show("Đã thêm danh mục.");
        });
    }

    private void BtnEditCategory_Click(object? sender, EventArgs e)
    {
        if (_gridCategories.CurrentRow is null) return;
        var row = _gridCategories.CurrentRow;
        using var dlg = new CategoryDialog(
            "Sửa danh mục",
            row.Cells["Tên danh mục"].Value?.ToString(),
            row.Cells["Mô tả"].Value?.ToString(),
            Convert.ToBoolean(row.Cells["Hoạt động"].Value));

        if (dlg.ShowDialog() != DialogResult.OK) return;
        int id = Convert.ToInt32(row.Cells["ID"].Value);

        SafeExecute(() =>
        {
            _service.UpdateCategory(id, dlg.CategoryName, dlg.DescriptionText, dlg.IsActive);
            LoadCategoryLookup();
            LoadCategories();
            MessageBox.Show("Đã cập nhật danh mục.");
        });
    }

    private void BtnDeleteCategory_Click(object? sender, EventArgs e)
    {
        if (_gridCategories.CurrentRow is null) return;
        int id = Convert.ToInt32(_gridCategories.CurrentRow.Cells["ID"].Value);
        if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        SafeExecute(() =>
        {
            _service.DeleteCategory(id);
            LoadCategoryLookup();
            LoadCategories();
            MessageBox.Show("Đã xóa danh mục.");
        });
    }

    private void BtnAddReader_Click(object? sender, EventArgs e)
    {
        using var dlg = new ReaderDialog("Thêm bạn đọc");
        if (dlg.ShowDialog() != DialogResult.OK) return;
        if (!ValidateReaderDialog(dlg)) return;

        SafeExecute(() =>
        {
            _service.AddReader(dlg.HoTen, dlg.MaSo, dlg.Loai, dlg.GioiTinh, dlg.NgaySinh, dlg.EmailValue, dlg.Sdt, dlg.DiaChi, dlg.IsActive);
            LoadReaders();
            MessageBox.Show("Đã thêm bạn đọc.");
        });
    }

    private void BtnEditReader_Click(object? sender, EventArgs e)
    {
        if (_gridReaders.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một bạn đọc để sửa.");
            return;
        }

        var selectedRow = _gridReaders.SelectedRows[0];

        IDictionary<string, object?> rowData = selectedRow.Cells
            .Cast<DataGridViewCell>()
            .ToDictionary(c => c.OwningColumn.HeaderText, c => (object?)c.Value);

        using var dlg = new ReaderDialog("Sửa bạn đọc", rowData);
        if (dlg.ShowDialog() != DialogResult.OK) return;
        if (!ValidateReaderDialog(dlg)) return;

        int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

        SafeExecute(() =>
        {
            _service.UpdateReader(
                id,
                dlg.HoTen,
                dlg.MaSo,
                dlg.Loai,
                dlg.GioiTinh,
                dlg.NgaySinh,
                dlg.EmailValue,
                dlg.Sdt,
                dlg.DiaChi,
                dlg.IsActive
            );

            LoadReaders();
            MessageBox.Show("Đã cập nhật bạn đọc.");
        });
    }

    private void BtnDeleteReader_Click(object? sender, EventArgs e)
    {
        if (_gridReaders.CurrentRow is null) return;
        int id = Convert.ToInt32(_gridReaders.CurrentRow.Cells["ID"].Value);
        if (MessageBox.Show("Bạn có chắc muốn xóa bạn đọc này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        SafeExecute(() =>
        {
            _service.DeleteReader(id);
            LoadReaders();
            MessageBox.Show("Đã xóa bạn đọc.");
        });
    }

    private void BtnAddBook_Click(object? sender, EventArgs e)
    {
        using var dlg = new BookDialog("Thêm sách", _lookup.GetCategories());
        if (dlg.ShowDialog() != DialogResult.OK) return;
        
        SafeExecute(() =>
        {
            _service.AddBook(dlg.TitleValue, dlg.Author, dlg.Publisher, dlg.PublishYear, dlg.Isbn, dlg.LanguageText, dlg.Price, dlg.DescriptionText, dlg.CategoryId, dlg.Copies);
            LoadBooks();
            MessageBox.Show("Đã thêm sách và bản sao.");
        });
    }

    private void BtnEditBook_Click(object? sender, EventArgs e)
    {
        if (_gridBooks.CurrentRow is null)
        {
            MessageBox.Show("Vui lòng chọn sách cần sửa.");
            return;
        }

        var row = _gridBooks.CurrentRow;

        var rowData = new Dictionary<string, object?>
        {
            ["Tiêu đề"] = row.Cells["Tiêu đề"].Value,
            ["Tác giả"] = row.Cells["Tác giả"].Value,
            ["NXB"] = row.Cells["NXB"].Value,
            ["Năm XB"] = row.Cells["Năm XB"].Value,
            ["ISBN"] = row.Cells["ISBN"].Value,
            ["Ngôn ngữ"] = row.Cells["Ngôn ngữ"].Value,
            ["Giá bìa"] = row.Cells["Giá bìa"].Value,
            ["Danh mục"] = row.Cells["Danh mục"].Value,
            ["Tổng bản sao"] = row.Cells["Tổng bản sao"].Value
        };

        using var dlg = new BookDialog("Sửa sách", _lookup.GetCategories(), rowData, true);
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int id = Convert.ToInt32(row.Cells["ID"].Value);

        SafeExecute(() =>
        {
            _service.UpdateBook(
                id,
                dlg.TitleValue,
                dlg.Author,
                dlg.Publisher,
                dlg.PublishYear,
                dlg.Isbn,
                dlg.LanguageText,
                dlg.Price,
                dlg.DescriptionText,
                dlg.CategoryId,
                dlg.Copies
            );

            LoadBooks();
            MessageBox.Show("Đã cập nhật sách.");
        });
    }

    private void BtnDeleteBook_Click(object? sender, EventArgs e)
    {
        if (_gridBooks.CurrentRow is null) return;
        int id = Convert.ToInt32(_gridBooks.CurrentRow.Cells["ID"].Value);
        if (MessageBox.Show("Bạn có chắc muốn xóa đầu sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        SafeExecute(() =>
        {
            _service.DeleteBook(id);
            LoadBooks();
            MessageBox.Show("Đã xóa sách.");
        });
    }

    private void BtnCreateLoan_Click(object? sender, EventArgs e)
    {
        using var dlg = new LoanCreateDialog(_lookup.GetReaders(), _lookup.GetAvailableCopies());
        if (dlg.ShowDialog() != DialogResult.OK) return;
        if (dlg.DueDate < dlg.BorrowDate)
        {
            MessageBox.Show("Ngày hẹn trả phải lớn hơn hoặc bằng ngày mượn.");
            return;
        }

        SafeExecute(() =>
        {
            _service.CreateLoan(dlg.ReaderId, dlg.BorrowDate, dlg.DueDate, dlg.SelectedCopyIds, _session.UserId);
            LoadLoans();
            LoadBooks();
            LoadReaders();
            LoadDashboard();
            MessageBox.Show("Đã lập phiếu mượn.");
        });
    }
    private void BtnEditLoan_Click(object? sender, EventArgs e)
    {
        var row = _gridLoans.CurrentRow;
        if (row is null)
        {
            MessageBox.Show("Vui lòng chọn phiếu mượn cần sửa.");
            return;
        }

        int id = Convert.ToInt32(row.Cells["ID"].Value);

        DateTime borrowDate = DateTime.Today;
        DateTime dueDate = DateTime.Today;
        int extendCount = 0;

        if (DateTime.TryParse(row.Cells["Ngày mượn"].Value?.ToString(), out var parsedBorrow))
            borrowDate = parsedBorrow;

        if (DateTime.TryParse(row.Cells["Hẹn trả"].Value?.ToString(), out var parsedDue))
            dueDate = parsedDue;

        int.TryParse(row.Cells["Gia hạn"].Value?.ToString(), out extendCount);

        using var dlg = new LoanEditDialog("Sửa phiếu mượn", borrowDate, dueDate, extendCount);

        if (dlg.ShowDialog() != DialogResult.OK) return;

        if (dlg.DueDate < dlg.BorrowDate)
        {
            MessageBox.Show("Hạn trả phải lớn hơn hoặc bằng ngày mượn.");
            return;
        }

        SafeExecute(() =>
        {
            _service.UpdateLoan(id, dlg.DueDate, dlg.ExtendCount);
            LoadLoanDetails();
            LoadLoans();
            LoadBooks();
            LoadReaders();
            LoadDashboard();
            MessageBox.Show("Đã cập nhật phiếu mượn.");
        });
    }
    private void BtnReturn_Click(object? sender, EventArgs e)
    {
        if (_gridLoans.CurrentRow is null)
        {
            MessageBox.Show("Vui lòng chọn phiếu mượn.");
            return;
        }

        string loanStatus = _gridLoans.CurrentRow.Cells["Trạng thái"].Value?.ToString() ?? "";
        if (loanStatus == "DA_TRA" || loanStatus == "HUY")
        {
            MessageBox.Show("Phiếu này đã kết thúc, không thể trả thêm.");
            return;
        }

        if (_gridLoanDetails.CurrentRow is null)
        {
            MessageBox.Show("Vui lòng chọn chi tiết sách cần trả.");
            return;
        }

        var thoiGianTra = _gridLoanDetails.CurrentRow.Cells["Thời gian trả"].Value?.ToString();
        if (!string.IsNullOrWhiteSpace(thoiGianTra))
        {
            MessageBox.Show("Cuốn sách này đã được trả rồi.");
            return;
        }

        int id = Convert.ToInt32(_gridLoanDetails.CurrentRow.Cells["ID"].Value);
        string currentCondition = _gridLoanDetails.CurrentRow.Cells["Lúc trả"].Value?.ToString() ?? "Tốt";

        using var returnDlg = new ReturnConditionDialog(currentCondition);
        if (returnDlg.ShowDialog() != DialogResult.OK) return;

        SafeExecute(() =>
        {
            _service.ReturnLoanDetail(id, _session.UserId, returnDlg.ReturnCondition);
            LoadLoanDetails();
            LoadLoans();
            LoadBooks();
            LoadReaders();
            LoadDashboard();
            MessageBox.Show("Đã trả sách.");
        });
    }

    private void BtnMarkFinePaid_Click(object? sender, EventArgs e)
    {
        if (_gridFines.CurrentRow is null) return;
        bool paid = Convert.ToBoolean(_gridFines.CurrentRow.Cells["Đã thanh toán"].Value);
        if (paid)
        {
            MessageBox.Show("Khoản phạt này đã thanh toán.");
            return;
        }

        int id = Convert.ToInt32(_gridFines.CurrentRow.Cells["ID"].Value);

        SafeExecute(() =>
        {
            _service.MarkFineAsPaid(id, _session.UserId);
            LoadFines();
            LoadDashboard();
            MessageBox.Show("Đã cập nhật trạng thái thanh toán.");
        });
    }

    private void BtnChangePassword_Click(object? sender, EventArgs e)
    {
        using var dlg = new ChangePasswordDialog();
        if (dlg.ShowDialog() != DialogResult.OK) return;

        SafeExecute(() =>
        {
            _auth.ChangePassword(_session.UserId, dlg.NewPassword);
            MessageBox.Show("Đã đổi mật khẩu.");
        });
    }

    private bool ValidateReaderDialog(ReaderDialog dlg)
    {
        if (string.IsNullOrWhiteSpace(dlg.HoTen))
        {
            MessageBox.Show("Họ tên không được trống.");
            return false;
        }
        if (!string.IsNullOrWhiteSpace(dlg.EmailValue))
        {
            var ok = Regex.IsMatch(dlg.EmailValue, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!ok)
            {
                MessageBox.Show("Email không hợp lệ.");
                return false;
            }
        }
        if (!string.IsNullOrWhiteSpace(dlg.Sdt))
        {
            var ok = Regex.IsMatch(dlg.Sdt, @"^[0-9]{9,11}$");
            if (!ok)
            {
                MessageBox.Show("Số điện thoại phải gồm 9-11 chữ số.");
                return false;
            }
        }
        return true;
    }

    private void SafeExecute(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void ApplyPermissions()
    {
        ApplyPermissionRecursive(this.Controls);
        btnNavAccounts.Visible = _session.HasPermission("TAI_KHOAN_XEM");
        if (!_session.HasPermission("BAO_CAO_XEM")) _tabs.TabPages[6].Text = "Thống kê (bị hạn chế)";
    }

    private void ApplyPermissionRecursive(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (!string.IsNullOrWhiteSpace(control.Name) && control.Name.Contains('_'))
            {
                control.Enabled = _session.HasPermission(control.Name);
            }

            if (control.HasChildren) ApplyPermissionRecursive(control.Controls);
        }
    }
}