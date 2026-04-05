namespace LibraryManagement.WinForms.Forms;

partial class LoanEditDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblBorrow;
    private Label lblDue;
    private Label lblExtend;
    private DateTimePicker dtBorrow;
    private DateTimePicker dtDue;
    private NumericUpDown nudExtend;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblBorrow = new Label();
        lblDue = new Label();
        lblExtend = new Label();
        dtBorrow = new DateTimePicker();
        dtDue = new DateTimePicker();
        nudExtend = new NumericUpDown();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)nudExtend).BeginInit();
        SuspendLayout();
        // 
        // lblBorrow
        // 
        lblBorrow.AutoSize = true;
        lblBorrow.Location = new Point(30, 32);
        lblBorrow.Margin = new Padding(4, 0, 4, 0);
        lblBorrow.Name = "lblBorrow";
        lblBorrow.Size = new Size(107, 25);
        lblBorrow.TabIndex = 0;
        lblBorrow.Text = "Ngày mượn";
        // 
        // lblDue
        // 
        lblDue.AutoSize = true;
        lblDue.Location = new Point(30, 90);
        lblDue.Margin = new Padding(4, 0, 4, 0);
        lblDue.Name = "lblDue";
        lblDue.Size = new Size(70, 25);
        lblDue.TabIndex = 2;
        lblDue.Text = "Hẹn trả";
        // 
        // lblExtend
        // 
        lblExtend.AutoSize = true;
        lblExtend.Location = new Point(30, 148);
        lblExtend.Margin = new Padding(4, 0, 4, 0);
        lblExtend.Name = "lblExtend";
        lblExtend.Size = new Size(71, 25);
        lblExtend.TabIndex = 4;
        lblExtend.Text = "Gia hạn";
        // 
        // dtBorrow
        // 
        dtBorrow.Format = DateTimePickerFormat.Short;
        dtBorrow.Location = new Point(175, 28);
        dtBorrow.Margin = new Padding(4, 4, 4, 4);
        dtBorrow.Name = "dtBorrow";
        dtBorrow.Size = new Size(274, 31);
        dtBorrow.TabIndex = 1;
        // 
        // dtDue
        // 
        dtDue.Format = DateTimePickerFormat.Short;
        dtDue.Location = new Point(175, 85);
        dtDue.Margin = new Padding(4, 4, 4, 4);
        dtDue.Name = "dtDue";
        dtDue.Size = new Size(274, 31);
        dtDue.TabIndex = 3;
        // 
        // nudExtend
        // 
        nudExtend.Location = new Point(175, 142);
        nudExtend.Margin = new Padding(4, 4, 4, 4);
        nudExtend.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        nudExtend.Name = "nudExtend";
        nudExtend.Size = new Size(162, 31);
        nudExtend.TabIndex = 5;
        // 
        // btnSave
        // 
        btnSave.DialogResult = DialogResult.OK;
        btnSave.Location = new Point(232, 212);
        btnSave.Margin = new Padding(4, 4, 4, 4);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(105, 42);
        btnSave.TabIndex = 6;
        btnSave.Text = "Lưu";
        btnSave.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(345, 212);
        btnCancel.Margin = new Padding(4, 4, 4, 4);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(105, 42);
        btnCancel.TabIndex = 7;
        btnCancel.Text = "Hủy";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // LoanEditDialog
        // 
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(490, 282);
        Controls.Add(lblBorrow);
        Controls.Add(dtBorrow);
        Controls.Add(lblDue);
        Controls.Add(dtDue);
        Controls.Add(lblExtend);
        Controls.Add(nudExtend);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoanEditDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Sửa phiếu mượn";
        ((System.ComponentModel.ISupportInitialize)nudExtend).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}