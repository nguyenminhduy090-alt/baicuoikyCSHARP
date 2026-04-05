namespace LibraryManagement.WinForms.Forms;

partial class AccountDialog
{
    private System.ComponentModel.IContainer? components = null;

    private TextBox txtUsername;
    private TextBox txtPassword;
    private TextBox txtConfirmPassword;
    private TextBox txtFullName;
    private TextBox txtEmail;
    private TextBox txtPhone;
    private ComboBox cboRole;
    private CheckBox chkActive;
    private Label lblPasswordHint;
    private TableLayoutPanel tableLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel1;
    private Button btnOk;
    private Button btnCancel;
    private Label lblUsername;
    private Label lblPassword;
    private Label lblConfirmPassword;
    private Label lblFullName;
    private Label lblEmail;
    private Label lblPhone;
    private Label lblRole;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        txtConfirmPassword = new TextBox();
        txtFullName = new TextBox();
        txtEmail = new TextBox();
        txtPhone = new TextBox();
        cboRole = new ComboBox();
        chkActive = new CheckBox();
        lblPasswordHint = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        lblUsername = new Label();
        lblPassword = new Label();
        lblConfirmPassword = new Label();
        lblFullName = new Label();
        lblEmail = new Label();
        lblPhone = new Label();
        lblRole = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        btnCancel = new Button();
        btnOk = new Button();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // txtUsername
        // 
        txtUsername.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtUsername.Location = new Point(173, 22);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(345, 23);
        txtUsername.TabIndex = 1;
        // 
        // txtPassword
        // 
        txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtPassword.Location = new Point(173, 60);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(345, 23);
        txtPassword.TabIndex = 3;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // txtConfirmPassword
        // 
        txtConfirmPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtConfirmPassword.Location = new Point(173, 98);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.Size = new Size(345, 23);
        txtConfirmPassword.TabIndex = 5;
        txtConfirmPassword.UseSystemPasswordChar = true;
        // 
        // txtFullName
        // 
        txtFullName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtFullName.Location = new Point(173, 136);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(345, 23);
        txtFullName.TabIndex = 7;
        // 
        // txtEmail
        // 
        txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtEmail.Location = new Point(173, 174);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(345, 23);
        txtEmail.TabIndex = 9;
        // 
        // txtPhone
        // 
        txtPhone.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtPhone.Location = new Point(173, 212);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(345, 23);
        txtPhone.TabIndex = 11;
        // 
        // cboRole
        // 
        cboRole.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRole.FormattingEnabled = true;
        cboRole.Location = new Point(173, 250);
        cboRole.Name = "cboRole";
        cboRole.Size = new Size(345, 23);
        cboRole.TabIndex = 13;
        // 
        // chkActive
        // 
        chkActive.Anchor = AnchorStyles.Left;
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Location = new Point(173, 290);
        chkActive.Margin = new Padding(3, 2, 3, 2);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(112, 19);
        chkActive.TabIndex = 14;
        chkActive.Text = "Đang hoạt động";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // lblPasswordHint
        // 
        lblPasswordHint.Dock = DockStyle.Top;
        lblPasswordHint.ForeColor = Color.DimGray;
        lblPasswordHint.Location = new Point(0, 0);
        lblPasswordHint.Name = "lblPasswordHint";
        lblPasswordHint.Padding = new Padding(170, 6, 0, 0);
        lblPasswordHint.Size = new Size(542, 24);
        lblPasswordHint.TabIndex = 1;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 149F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(lblUsername, 0, 0);
        tableLayoutPanel1.Controls.Add(txtUsername, 1, 0);
        tableLayoutPanel1.Controls.Add(lblPassword, 0, 1);
        tableLayoutPanel1.Controls.Add(txtPassword, 1, 1);
        tableLayoutPanel1.Controls.Add(lblConfirmPassword, 0, 2);
        tableLayoutPanel1.Controls.Add(txtConfirmPassword, 1, 2);
        tableLayoutPanel1.Controls.Add(lblFullName, 0, 3);
        tableLayoutPanel1.Controls.Add(txtFullName, 1, 3);
        tableLayoutPanel1.Controls.Add(lblEmail, 0, 4);
        tableLayoutPanel1.Controls.Add(txtEmail, 1, 4);
        tableLayoutPanel1.Controls.Add(lblPhone, 0, 5);
        tableLayoutPanel1.Controls.Add(txtPhone, 1, 5);
        tableLayoutPanel1.Controls.Add(lblRole, 0, 6);
        tableLayoutPanel1.Controls.Add(cboRole, 1, 6);
        tableLayoutPanel1.Controls.Add(chkActive, 1, 7);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 24);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(21, 15, 21, 15);
        tableLayoutPanel1.RowCount = 8;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel1.Size = new Size(542, 325);
        tableLayoutPanel1.TabIndex = 0;
        tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
        // 
        // lblUsername
        // 
        lblUsername.Anchor = AnchorStyles.Left;
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(24, 26);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(86, 15);
        lblUsername.TabIndex = 0;
        lblUsername.Text = "Tên đăng nhập";
        // 
        // lblPassword
        // 
        lblPassword.Anchor = AnchorStyles.Left;
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(24, 64);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(57, 15);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Mật khẩu";
        // 
        // lblConfirmPassword
        // 
        lblConfirmPassword.Anchor = AnchorStyles.Left;
        lblConfirmPassword.AutoSize = true;
        lblConfirmPassword.Location = new Point(24, 102);
        lblConfirmPassword.Name = "lblConfirmPassword";
        lblConfirmPassword.Size = new Size(104, 15);
        lblConfirmPassword.TabIndex = 4;
        lblConfirmPassword.Text = "Nhập lại mật khẩu";
        // 
        // lblFullName
        // 
        lblFullName.Anchor = AnchorStyles.Left;
        lblFullName.AutoSize = true;
        lblFullName.Location = new Point(24, 140);
        lblFullName.Name = "lblFullName";
        lblFullName.Size = new Size(43, 15);
        lblFullName.TabIndex = 6;
        lblFullName.Text = "Họ tên";
        // 
        // lblEmail
        // 
        lblEmail.Anchor = AnchorStyles.Left;
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(24, 178);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(36, 15);
        lblEmail.TabIndex = 8;
        lblEmail.Text = "Email";
        // 
        // lblPhone
        // 
        lblPhone.Anchor = AnchorStyles.Left;
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(24, 216);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(28, 15);
        lblPhone.TabIndex = 10;
        lblPhone.Text = "SĐT";
        // 
        // lblRole
        // 
        lblRole.Anchor = AnchorStyles.Left;
        lblRole.AutoSize = true;
        lblRole.Location = new Point(24, 254);
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(40, 15);
        lblRole.TabIndex = 12;
        lblRole.Text = "Vai trò";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(btnCancel);
        flowLayoutPanel1.Controls.Add(btnOk);
        flowLayoutPanel1.Dock = DockStyle.Bottom;
        flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new Point(0, 349);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Padding = new Padding(12);
        flowLayoutPanel1.Size = new Size(542, 54);
        flowLayoutPanel1.TabIndex = 2;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(422, 14);
        btnCancel.Margin = new Padding(3, 2, 3, 2);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(93, 27);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "Hủy";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnOk
        // 
        btnOk.BackColor = Color.SeaGreen;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.ForeColor = Color.White;
        btnOk.Location = new Point(323, 14);
        btnOk.Margin = new Padding(3, 2, 3, 2);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(93, 27);
        btnOk.TabIndex = 0;
        btnOk.Text = "Lưu";
        btnOk.UseVisualStyleBackColor = false;
        // 
        // AccountDialog
        // 
        AcceptButton = btnOk;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(542, 403);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(lblPasswordHint);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AccountDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Tài khoản";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }
}
