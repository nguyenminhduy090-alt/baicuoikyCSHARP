namespace LibraryManagement.WinForms.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel panelRoot;
    private Panel panelHeader;
    private Panel panelSidebar;
    private Label lblTitle;
    private Label lblUser;
    private Button btnNavReaders;
    private Button btnNavAccounts;
    private Button btnNavCategories;
    private Button btnNavBooks;
    private Button btnNavLoans;
    private Button btnNavFines;
    private Button btnNavStatistics;
    private Button btnLogout;
    private Button btnChangePassword;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelRoot = new Panel();
        panelHeader = new Panel();
        panelSidebar = new Panel();
        lblTitle = new Label();
        lblUser = new Label();
        btnNavReaders = new Button();
        btnNavAccounts = new Button();
        btnNavCategories = new Button();
        btnNavBooks = new Button();
        btnNavLoans = new Button();
        btnNavFines = new Button();
        btnNavStatistics = new Button();
        btnLogout = new Button();
        btnChangePassword = new Button();

        panelRoot.SuspendLayout();
        panelHeader.SuspendLayout();
        panelSidebar.SuspendLayout();
        SuspendLayout();

        // panelRoot
        panelRoot.BackColor = Color.FromArgb(245, 246, 250);
        panelRoot.Dock = DockStyle.Fill;
        panelRoot.Controls.Add(panelSidebar);
        panelRoot.Controls.Add(panelHeader);

        // panelHeader
        panelHeader.BackColor = Color.FromArgb(0, 102, 204);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Height = 60;
        panelHeader.Controls.Add(lblUser);
        panelHeader.Controls.Add(lblTitle);

        // lblTitle
        lblTitle.Text = AppSettings.AppTitle;
        lblTitle.Dock = DockStyle.Left;
        lblTitle.Width = 420;
        lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblTitle.ForeColor = Color.White;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Padding = new Padding(15, 0, 0, 0);

        // lblUser
        lblUser.Dock = DockStyle.Right;
        lblUser.Width = 280;
        lblUser.ForeColor = Color.White;
        lblUser.TextAlign = ContentAlignment.MiddleRight;
        lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblUser.Padding = new Padding(0, 0, 15, 0);

        // panelSidebar
        panelSidebar.BackColor = Color.FromArgb(8, 24, 56);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Width = 190;

        // btnNavStatistics
        btnNavStatistics.Text = "Thống kê";
        btnNavStatistics.Dock = DockStyle.Top;
        btnNavStatistics.Height = 55;
        btnNavStatistics.BackColor = Color.FromArgb(8, 24, 56);
        btnNavStatistics.ForeColor = Color.White;
        btnNavStatistics.FlatStyle = FlatStyle.Flat;
        btnNavStatistics.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavStatistics.FlatAppearance.BorderSize = 0;

        // btnNavFines
        btnNavFines.Text = "Tiền phạt";
        btnNavFines.Dock = DockStyle.Top;
        btnNavFines.Height = 55;
        btnNavFines.BackColor = Color.FromArgb(8, 24, 56);
        btnNavFines.ForeColor = Color.White;
        btnNavFines.FlatStyle = FlatStyle.Flat;
        btnNavFines.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavFines.FlatAppearance.BorderSize = 0;

        // btnNavLoans
        btnNavLoans.Text = "Phiếu mượn";
        btnNavLoans.Dock = DockStyle.Top;
        btnNavLoans.Height = 55;
        btnNavLoans.BackColor = Color.FromArgb(8, 24, 56);
        btnNavLoans.ForeColor = Color.White;
        btnNavLoans.FlatStyle = FlatStyle.Flat;
        btnNavLoans.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavLoans.FlatAppearance.BorderSize = 0;

        // btnNavBooks
        btnNavBooks.Text = "Sách";
        btnNavBooks.Dock = DockStyle.Top;
        btnNavBooks.Height = 55;
        btnNavBooks.BackColor = Color.FromArgb(8, 24, 56);
        btnNavBooks.ForeColor = Color.White;
        btnNavBooks.FlatStyle = FlatStyle.Flat;
        btnNavBooks.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavBooks.FlatAppearance.BorderSize = 0;

        // btnNavAccounts
        btnNavAccounts.Text = "Tài khoản";
        btnNavAccounts.Dock = DockStyle.Top;
        btnNavAccounts.Height = 55;
        btnNavAccounts.BackColor = Color.FromArgb(8, 24, 56);
        btnNavAccounts.ForeColor = Color.White;
        btnNavAccounts.FlatStyle = FlatStyle.Flat;
        btnNavAccounts.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavAccounts.FlatAppearance.BorderSize = 0;
        btnNavAccounts.Name = "TAI_KHOAN_XEM";

        // btnNavCategories
        btnNavCategories.Text = "Danh mục";
        btnNavCategories.Dock = DockStyle.Top;
        btnNavCategories.Height = 55;
        btnNavCategories.BackColor = Color.FromArgb(8, 24, 56);
        btnNavCategories.ForeColor = Color.White;
        btnNavCategories.FlatStyle = FlatStyle.Flat;
        btnNavCategories.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavCategories.FlatAppearance.BorderSize = 0;

        // btnNavReaders
        btnNavReaders.Text = "Bạn đọc";
        btnNavReaders.Dock = DockStyle.Top;
        btnNavReaders.Height = 55;
        btnNavReaders.BackColor = Color.FromArgb(8, 24, 56);
        btnNavReaders.ForeColor = Color.White;
        btnNavReaders.FlatStyle = FlatStyle.Flat;
        btnNavReaders.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnNavReaders.FlatAppearance.BorderSize = 0;

        // btnLogout
        btnLogout.Text = "Đăng xuất";
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.Height = 45;
        btnLogout.BackColor = Color.Firebrick;
        btnLogout.ForeColor = Color.White;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.FlatAppearance.BorderSize = 0;

        // btnChangePassword
        btnChangePassword.Text = "Đổi mật khẩu";
        btnChangePassword.Dock = DockStyle.Bottom;
        btnChangePassword.Height = 45;
        btnChangePassword.BackColor = Color.FromArgb(255, 153, 0);
        btnChangePassword.ForeColor = Color.White;
        btnChangePassword.FlatStyle = FlatStyle.Flat;
        btnChangePassword.FlatAppearance.BorderSize = 0;

        // panelSidebar controls
        panelSidebar.Controls.Add(btnNavStatistics);
        panelSidebar.Controls.Add(btnNavFines);
        panelSidebar.Controls.Add(btnNavLoans);
        panelSidebar.Controls.Add(btnNavBooks);
        panelSidebar.Controls.Add(btnNavCategories);
        panelSidebar.Controls.Add(btnNavAccounts);
        panelSidebar.Controls.Add(btnNavReaders);
        panelSidebar.Controls.Add(btnChangePassword);
        panelSidebar.Controls.Add(btnLogout);

        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1280, 760);
        Controls.Add(panelRoot);
        MinimumSize = new Size(1200, 750);
        WindowState = FormWindowState.Maximized;
        Name = "MainForm";
        Text = AppSettings.AppTitle;

        panelRoot.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelSidebar.ResumeLayout(false);
        ResumeLayout(false);
    }
}