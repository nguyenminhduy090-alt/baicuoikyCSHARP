namespace LibraryManagement.WinForms.Forms;

partial class ReaderDialog
{
    private System.ComponentModel.IContainer? components = null;

    private TextBox txtHoTen;
    private TextBox txtMaSo;
    private ComboBox cboLoai;
    private ComboBox cboGioiTinh;
    private DateTimePicker dtNgaySinh;
    private TextBox txtEmail;
    private TextBox txtSdt;
    private TextBox txtDiaChi;
    private CheckBox chkActive;

    private TableLayoutPanel tableLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel1;
    private Button btnOk;
    private Button btnCancel;

    private Label lblHoTen;
    private Label lblMaSo;
    private Label lblLoai;
    private Label lblGioiTinh;
    private Label lblNgaySinh;
    private Label lblEmail;
    private Label lblSdt;
    private Label lblDiaChi;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtHoTen = new TextBox();
        txtMaSo = new TextBox();
        cboLoai = new ComboBox();
        cboGioiTinh = new ComboBox();
        dtNgaySinh = new DateTimePicker();
        txtEmail = new TextBox();
        txtSdt = new TextBox();
        txtDiaChi = new TextBox();
        chkActive = new CheckBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        lblHoTen = new Label();
        lblMaSo = new Label();
        lblLoai = new Label();
        lblGioiTinh = new Label();
        lblNgaySinh = new Label();
        lblEmail = new Label();
        lblSdt = new Label();
        lblDiaChi = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        btnCancel = new Button();
        btnOk = new Button();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // txtHoTen
        // 
        txtHoTen.Dock = DockStyle.Fill;
        txtHoTen.Location = new Point(142, 16);
        txtHoTen.Margin = new Padding(3, 4, 3, 4);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(295, 23);
        txtHoTen.TabIndex = 1;
        // 
        // txtMaSo
        // 
        txtMaSo.Dock = DockStyle.Fill;
        txtMaSo.Location = new Point(142, 46);
        txtMaSo.Margin = new Padding(3, 4, 3, 4);
        txtMaSo.Name = "txtMaSo";
        txtMaSo.Size = new Size(295, 23);
        txtMaSo.TabIndex = 3;
        // 
        // cboLoai
        // 
        cboLoai.Dock = DockStyle.Fill;
        cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoai.Location = new Point(142, 76);
        cboLoai.Margin = new Padding(3, 4, 3, 4);
        cboLoai.Name = "cboLoai";
        cboLoai.Size = new Size(295, 23);
        cboLoai.TabIndex = 5;
        // 
        // cboGioiTinh
        // 
        cboGioiTinh.Dock = DockStyle.Fill;
        cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGioiTinh.Location = new Point(142, 106);
        cboGioiTinh.Margin = new Padding(3, 4, 3, 4);
        cboGioiTinh.Name = "cboGioiTinh";
        cboGioiTinh.Size = new Size(295, 23);
        cboGioiTinh.TabIndex = 7;
        // 
        // dtNgaySinh
        // 
        dtNgaySinh.Dock = DockStyle.Fill;
        dtNgaySinh.Format = DateTimePickerFormat.Short;
        dtNgaySinh.Location = new Point(142, 136);
        dtNgaySinh.Margin = new Padding(3, 4, 3, 4);
        dtNgaySinh.Name = "dtNgaySinh";
        dtNgaySinh.ShowCheckBox = true;
        dtNgaySinh.Size = new Size(295, 23);
        dtNgaySinh.TabIndex = 9;
        // 
        // txtEmail
        // 
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Location = new Point(142, 166);
        txtEmail.Margin = new Padding(3, 4, 3, 4);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(295, 23);
        txtEmail.TabIndex = 11;
        // 
        // txtSdt
        // 
        txtSdt.Dock = DockStyle.Fill;
        txtSdt.Location = new Point(142, 196);
        txtSdt.Margin = new Padding(3, 4, 3, 4);
        txtSdt.Name = "txtSdt";
        txtSdt.Size = new Size(295, 23);
        txtSdt.TabIndex = 13;
        // 
        // txtDiaChi
        // 
        txtDiaChi.Dock = DockStyle.Fill;
        txtDiaChi.Location = new Point(142, 226);
        txtDiaChi.Margin = new Padding(3, 4, 3, 4);
        txtDiaChi.Name = "txtDiaChi";
        txtDiaChi.Size = new Size(295, 23);
        txtDiaChi.TabIndex = 15;
        // 
        // chkActive
        // 
        chkActive.Anchor = AnchorStyles.Left;
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Location = new Point(142, 308);
        chkActive.Margin = new Padding(3, 5, 3, 5);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(112, 19);
        chkActive.TabIndex = 17;
        chkActive.Text = "Đang hoạt động";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.AutoScroll = true;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 119F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(lblHoTen, 0, 0);
        tableLayoutPanel1.Controls.Add(txtHoTen, 1, 0);
        tableLayoutPanel1.Controls.Add(lblMaSo, 0, 1);
        tableLayoutPanel1.Controls.Add(txtMaSo, 1, 1);
        tableLayoutPanel1.Controls.Add(lblLoai, 0, 2);
        tableLayoutPanel1.Controls.Add(cboLoai, 1, 2);
        tableLayoutPanel1.Controls.Add(lblGioiTinh, 0, 3);
        tableLayoutPanel1.Controls.Add(cboGioiTinh, 1, 3);
        tableLayoutPanel1.Controls.Add(lblNgaySinh, 0, 4);
        tableLayoutPanel1.Controls.Add(dtNgaySinh, 1, 4);
        tableLayoutPanel1.Controls.Add(lblEmail, 0, 5);
        tableLayoutPanel1.Controls.Add(txtEmail, 1, 5);
        tableLayoutPanel1.Controls.Add(lblSdt, 0, 6);
        tableLayoutPanel1.Controls.Add(txtSdt, 1, 6);
        tableLayoutPanel1.Controls.Add(lblDiaChi, 0, 7);
        tableLayoutPanel1.Controls.Add(txtDiaChi, 1, 7);
        tableLayoutPanel1.Controls.Add(chkActive, 1, 8);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(20, 12, 20, 12);
        tableLayoutPanel1.RowCount = 9;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.Size = new Size(460, 395);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // lblHoTen
        // 
        lblHoTen.Anchor = AnchorStyles.Left;
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(22, 19);
        lblHoTen.Margin = new Padding(2, 0, 2, 0);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(43, 15);
        lblHoTen.TabIndex = 0;
        lblHoTen.Text = "Họ tên";
        // 
        // lblMaSo
        // 
        lblMaSo.Anchor = AnchorStyles.Left;
        lblMaSo.AutoSize = true;
        lblMaSo.Location = new Point(22, 49);
        lblMaSo.Margin = new Padding(2, 0, 2, 0);
        lblMaSo.Name = "lblMaSo";
        lblMaSo.Size = new Size(39, 15);
        lblMaSo.TabIndex = 2;
        lblMaSo.Text = "Mã số";
        // 
        // lblLoai
        // 
        lblLoai.Anchor = AnchorStyles.Left;
        lblLoai.AutoSize = true;
        lblLoai.Location = new Point(22, 79);
        lblLoai.Margin = new Padding(2, 0, 2, 0);
        lblLoai.Name = "lblLoai";
        lblLoai.Size = new Size(29, 15);
        lblLoai.TabIndex = 4;
        lblLoai.Text = "Loại";
        // 
        // lblGioiTinh
        // 
        lblGioiTinh.Anchor = AnchorStyles.Left;
        lblGioiTinh.AutoSize = true;
        lblGioiTinh.Location = new Point(22, 109);
        lblGioiTinh.Margin = new Padding(2, 0, 2, 0);
        lblGioiTinh.Name = "lblGioiTinh";
        lblGioiTinh.Size = new Size(52, 15);
        lblGioiTinh.TabIndex = 6;
        lblGioiTinh.Text = "Giới tính";
        // 
        // lblNgaySinh
        // 
        lblNgaySinh.Anchor = AnchorStyles.Left;
        lblNgaySinh.AutoSize = true;
        lblNgaySinh.Location = new Point(22, 139);
        lblNgaySinh.Margin = new Padding(2, 0, 2, 0);
        lblNgaySinh.Name = "lblNgaySinh";
        lblNgaySinh.Size = new Size(60, 15);
        lblNgaySinh.TabIndex = 8;
        lblNgaySinh.Text = "Ngày sinh";
        // 
        // lblEmail
        // 
        lblEmail.Anchor = AnchorStyles.Left;
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(22, 169);
        lblEmail.Margin = new Padding(2, 0, 2, 0);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(36, 15);
        lblEmail.TabIndex = 10;
        lblEmail.Text = "Email";
        // 
        // lblSdt
        // 
        lblSdt.Anchor = AnchorStyles.Left;
        lblSdt.AutoSize = true;
        lblSdt.Location = new Point(22, 199);
        lblSdt.Margin = new Padding(2, 0, 2, 0);
        lblSdt.Name = "lblSdt";
        lblSdt.Size = new Size(28, 15);
        lblSdt.TabIndex = 12;
        lblSdt.Text = "SĐT";
        // 
        // lblDiaChi
        // 
        lblDiaChi.Anchor = AnchorStyles.Left;
        lblDiaChi.AutoSize = true;
        lblDiaChi.Location = new Point(22, 229);
        lblDiaChi.Margin = new Padding(2, 0, 2, 0);
        lblDiaChi.Name = "lblDiaChi";
        lblDiaChi.Size = new Size(43, 15);
        lblDiaChi.TabIndex = 14;
        lblDiaChi.Text = "Địa chỉ";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(btnCancel);
        flowLayoutPanel1.Controls.Add(btnOk);
        flowLayoutPanel1.Dock = DockStyle.Bottom;
        flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new Point(0, 395);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Padding = new Padding(10);
        flowLayoutPanel1.Size = new Size(460, 55);
        flowLayoutPanel1.TabIndex = 1;
        flowLayoutPanel1.WrapContents = false;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(340, 10);
        btnCancel.Margin = new Padding(8, 0, 0, 0);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 34);
        btnCancel.TabIndex = 0;
        btnCancel.Text = "Hủy";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnOk
        // 
        btnOk.BackColor = Color.SeaGreen;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.ForeColor = Color.White;
        btnOk.Location = new Point(232, 10);
        btnOk.Margin = new Padding(8, 0, 0, 0);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(100, 34);
        btnOk.TabIndex = 1;
        btnOk.Text = "Lưu";
        btnOk.UseVisualStyleBackColor = false;
        // 
        // ReaderDialog
        // 
        AcceptButton = btnOk;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(460, 450);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(flowLayoutPanel1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReaderDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Bạn đọc";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }
}