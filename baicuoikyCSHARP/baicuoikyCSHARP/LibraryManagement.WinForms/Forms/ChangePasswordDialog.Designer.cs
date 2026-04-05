namespace LibraryManagement.WinForms.Forms;

partial class ChangePasswordDialog
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtNew;
    private TextBox txtConfirm;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label1;
    private Label label2;
    private FlowLayoutPanel flowLayoutPanel1;
    private Button btnOk;
    private Button btnCancel;
    protected override void Dispose(bool disposing){ if(disposing && components!=null) components.Dispose(); base.Dispose(disposing);}
    private void InitializeComponent()
    {
        txtNew = new TextBox();
        txtConfirm = new TextBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        label1 = new Label();
        label2 = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        btnCancel = new Button();
        btnOk = new Button();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // txtNew
        // 
        txtNew.Location = new Point(209, 38);
        txtNew.Margin = new Padding(4, 5, 4, 5);
        txtNew.Multiline = true;
        txtNew.Name = "txtNew";
        txtNew.PasswordChar = '*';
        txtNew.Size = new Size(294, 30);
        txtNew.TabIndex = 1;
        // 
        // txtConfirm
        // 
        txtConfirm.Location = new Point(209, 85);
        txtConfirm.Margin = new Padding(4, 5, 4, 5);
        txtConfirm.Multiline = true;
        txtConfirm.Name = "txtConfirm";
        txtConfirm.PasswordChar = '*';
        txtConfirm.Size = new Size(294, 29);
        txtConfirm.TabIndex = 3;
        txtConfirm.TextChanged += txtConfirm_TextChanged;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 176F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(label1, 0, 0);
        tableLayoutPanel1.Controls.Add(txtConfirm, 1, 1);
        tableLayoutPanel1.Controls.Add(label2, 0, 1);
        tableLayoutPanel1.Controls.Add(txtNew, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(29, 33, 29, 33);
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 54.65116F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.34884F));
        tableLayoutPanel1.Size = new Size(536, 152);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Location = new Point(33, 44);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(122, 25);
        label1.TabIndex = 0;
        label1.Text = "Mật khẩu mới";
        label1.Click += label1_Click;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left;
        label2.AutoSize = true;
        label2.Location = new Point(33, 87);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(77, 25);
        label2.TabIndex = 2;
        label2.Text = "Nhập lại";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(btnCancel);
        flowLayoutPanel1.Controls.Add(btnOk);
        flowLayoutPanel1.Dock = DockStyle.Bottom;
        flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new Point(0, 152);
        flowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Padding = new Padding(14, 17, 14, 17);
        flowLayoutPanel1.Size = new Size(536, 67);
        flowLayoutPanel1.TabIndex = 1;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(361, 22);
        btnCancel.Margin = new Padding(4, 5, 4, 5);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(143, 38);
        btnCancel.TabIndex = 0;
        btnCancel.Text = "Hủy";
        // 
        // btnOk
        // 
        btnOk.BackColor = Color.SeaGreen;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.ForeColor = Color.White;
        btnOk.Location = new Point(210, 22);
        btnOk.Margin = new Padding(4, 5, 4, 5);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(143, 38);
        btnOk.TabIndex = 1;
        btnOk.Text = "Đổi";
        btnOk.UseVisualStyleBackColor = false;
        btnOk.Click += btnOk_Click;
        // 
        // ChangePasswordDialog
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(536, 219);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(flowLayoutPanel1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ChangePasswordDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Đổi mật khẩu";
        Load += ChangePasswordDialog_Load;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }
}
