namespace LibraryManagement.WinForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer? components = null;
    private TextBox _txtUsername;
    private TextBox _txtPassword;
    private Button _btnLogin;
    private Button _btnExit;
    private Label _lblError;
    private Label lblTitle;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label1;
    private Label label2;
    private FlowLayoutPanel flowLayoutPanel1;
    private CheckBox _chkRememberUsername;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _txtUsername = new TextBox();
        _txtPassword = new TextBox();
        _btnLogin = new Button();
        _btnExit = new Button();
        _lblError = new Label();
        lblTitle = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        label1 = new Label();
        label2 = new Label();
        _chkRememberUsername = new CheckBox();
        flowLayoutPanel1 = new FlowLayoutPanel();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // _txtUsername
        // 
        _txtUsername.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtUsername.Location = new Point(244, 64);
        _txtUsername.Margin = new Padding(4, 10, 4, 10);
        _txtUsername.Name = "_txtUsername";
        _txtUsername.Size = new Size(373, 31);
        _txtUsername.TabIndex = 1;
        // 
        // _txtPassword
        // 
        _txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtPassword.Location = new Point(244, 139);
        _txtPassword.Margin = new Padding(4, 10, 4, 10);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.PasswordChar = '*';
        _txtPassword.Size = new Size(373, 31);
        _txtPassword.TabIndex = 3;
        // 
        // _btnLogin
        // 
        _btnLogin.BackColor = Color.FromArgb(0, 102, 204);
        _btnLogin.FlatStyle = FlatStyle.Flat;
        _btnLogin.ForeColor = Color.White;
        _btnLogin.Location = new Point(4, 22);
        _btnLogin.Margin = new Padding(4, 5, 4, 5);
        _btnLogin.Name = "_btnLogin";
        _btnLogin.Size = new Size(171, 58);
        _btnLogin.TabIndex = 0;
        _btnLogin.Text = "Đăng nhập";
        _btnLogin.UseVisualStyleBackColor = false;
        _btnLogin.Click += BtnLogin_Click;
        // 
        // _btnExit
        // 
        _btnExit.BackColor = Color.Gray;
        _btnExit.FlatStyle = FlatStyle.Flat;
        _btnExit.ForeColor = Color.White;
        _btnExit.Location = new Point(183, 22);
        _btnExit.Margin = new Padding(4, 5, 4, 5);
        _btnExit.Name = "_btnExit";
        _btnExit.Size = new Size(171, 58);
        _btnExit.TabIndex = 1;
        _btnExit.Text = "Thoát";
        _btnExit.UseVisualStyleBackColor = false;
        _btnExit.Click += LoginExit_Click;
        // 
        // _lblError
        // 
        _lblError.AutoSize = true;
        _lblError.Dock = DockStyle.Fill;
        _lblError.ForeColor = Color.Firebrick;
        _lblError.Location = new Point(244, 192);
        _lblError.Margin = new Padding(4, 0, 4, 0);
        _lblError.Name = "_lblError";
        _lblError.Size = new Size(373, 36);
        _lblError.TabIndex = 5;
        _lblError.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTitle
        // 
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(0, 102, 204);
        lblTitle.Location = new Point(0, 0);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(657, 117);
        lblTitle.TabIndex = 1;
        lblTitle.Text = "QUẢN LÝ THƯ VIỆN";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        tableLayoutPanel1.Controls.Add(label1, 0, 0);
        tableLayoutPanel1.Controls.Add(_txtUsername, 1, 0);
        tableLayoutPanel1.Controls.Add(label2, 0, 1);
        tableLayoutPanel1.Controls.Add(_txtPassword, 1, 1);
        tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 4);
        tableLayoutPanel1.Controls.Add(_lblError, 1, 2);
        tableLayoutPanel1.Controls.Add(_chkRememberUsername, 1, 3);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 117);
        tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(36, 42, 36, 42);
        tableLayoutPanel1.RowCount = 5;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(657, 416);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        label1.Location = new Point(40, 65);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(152, 28);
        label1.TabIndex = 0;
        label1.Text = "Tên đăng nhập";
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left;
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        label2.Location = new Point(40, 140);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(102, 28);
        label2.TabIndex = 2;
        label2.Text = "Mật khẩu";
        // 
        // _chkRememberUsername
        // 
        _chkRememberUsername.Anchor = AnchorStyles.Left;
        _chkRememberUsername.AutoSize = true;
        _chkRememberUsername.Location = new Point(244, 239);
        _chkRememberUsername.Margin = new Padding(4, 0, 4, 0);
        _chkRememberUsername.Name = "_chkRememberUsername";
        _chkRememberUsername.Size = new Size(193, 29);
        _chkRememberUsername.TabIndex = 4;
        _chkRememberUsername.Text = "Nhớ tên đăng nhập";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(_btnLogin);
        flowLayoutPanel1.Controls.Add(_btnExit);
        flowLayoutPanel1.Dock = DockStyle.Fill;
        flowLayoutPanel1.Location = new Point(244, 284);
        flowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Padding = new Padding(0, 17, 0, 0);
        flowLayoutPanel1.Size = new Size(373, 113);
        flowLayoutPanel1.TabIndex = 6;
        flowLayoutPanel1.WrapContents = false;
        // 
        // LoginForm
        // 
        AcceptButton = _btnLogin;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(657, 533);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Đăng nhập";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }
}