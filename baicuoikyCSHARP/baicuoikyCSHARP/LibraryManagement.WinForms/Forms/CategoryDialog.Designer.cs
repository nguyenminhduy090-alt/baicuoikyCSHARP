namespace LibraryManagement.WinForms.Forms;

partial class CategoryDialog
{
    private System.ComponentModel.IContainer components = null;
    private TextBox _txtName;
    private TextBox _txtDescription;
    private CheckBox _chkActive;
    private TableLayoutPanel tableLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel1;
    private Button btnOk;
    private Button btnCancel;
    private Label label1;
    private Label label2;
    protected override void Dispose(bool disposing){ if(disposing && components!=null) components.Dispose(); base.Dispose(disposing);}
    private void InitializeComponent()
    {
        _txtName = new TextBox();
        _txtDescription = new TextBox();
        _chkActive = new CheckBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        label2 = new Label();
        label1 = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        btnCancel = new Button();
        btnOk = new Button();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // _txtName
        // 
        _txtName.Location = new Point(176, 38);
        _txtName.Margin = new Padding(4, 5, 4, 5);
        _txtName.Multiline = true;
        _txtName.Name = "_txtName";
        _txtName.Size = new Size(388, 34);
        _txtName.TabIndex = 1;
        // 
        // _txtDescription
        // 
        _txtDescription.Location = new Point(176, 82);
        _txtDescription.Margin = new Padding(4, 5, 4, 5);
        _txtDescription.Multiline = true;
        _txtDescription.Name = "_txtDescription";
        _txtDescription.Size = new Size(388, 178);
        _txtDescription.TabIndex = 3;
        // 
        // _chkActive
        // 
        _chkActive.Checked = true;
        _chkActive.CheckState = CheckState.Checked;
        _chkActive.Location = new Point(176, 292);
        _chkActive.Margin = new Padding(4, 5, 4, 5);
        _chkActive.Name = "_chkActive";
        _chkActive.Size = new Size(221, 37);
        _chkActive.TabIndex = 4;
        _chkActive.Text = "Đang hoạt động";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 143F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(label2, 0, 1);
        tableLayoutPanel1.Controls.Add(_chkActive, 1, 2);
        tableLayoutPanel1.Controls.Add(label1, 0, 0);
        tableLayoutPanel1.Controls.Add(_txtName, 1, 0);
        tableLayoutPanel1.Controls.Add(_txtDescription, 1, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(29, 33, 29, 33);
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        tableLayoutPanel1.Size = new Size(614, 367);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left;
        label2.AutoSize = true;
        label2.Location = new Point(33, 169);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(59, 25);
        label2.TabIndex = 2;
        label2.Text = "Mô tả";
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Location = new Point(33, 42);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(38, 25);
        label1.TabIndex = 0;
        label1.Text = "Tên";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(btnCancel);
        flowLayoutPanel1.Controls.Add(btnOk);
        flowLayoutPanel1.Dock = DockStyle.Bottom;
        flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new Point(0, 367);
        flowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Padding = new Padding(14, 17, 14, 17);
        flowLayoutPanel1.Size = new Size(614, 83);
        flowLayoutPanel1.TabIndex = 1;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(439, 22);
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
        btnOk.Location = new Point(288, 22);
        btnOk.Margin = new Padding(4, 5, 4, 5);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(143, 38);
        btnOk.TabIndex = 1;
        btnOk.Text = "Lưu";
        btnOk.UseVisualStyleBackColor = false;
        // 
        // CategoryDialog
        // 
        AcceptButton = btnOk;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(614, 450);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(flowLayoutPanel1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CategoryDialog";
        StartPosition = FormStartPosition.CenterParent;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }
}
