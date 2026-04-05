namespace LibraryManagement.WinForms.Forms;

partial class LoanCreateDialog
{
    private System.ComponentModel.IContainer components = null;

    private Label lblReader;
    private Label lblBorrow;
    private Label lblDue;
    private Label lblCopies;
    private Label lblNote;

    private ComboBox cboReader;
    private DateTimePicker dtBorrow;
    private DateTimePicker dtDue;
    private CheckedListBox chkCopies;

    private Button btnOk;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblReader = new Label();
        lblBorrow = new Label();
        lblDue = new Label();
        lblCopies = new Label();
        lblNote = new Label();
        cboReader = new ComboBox();
        dtBorrow = new DateTimePicker();
        dtDue = new DateTimePicker();
        chkCopies = new CheckedListBox();
        btnOk = new Button();
        btnCancel = new Button();
        SuspendLayout();
        // 
        // lblReader
        // 
        lblReader.AutoSize = true;
        lblReader.Location = new Point(43, 50);
        lblReader.Margin = new Padding(4, 0, 4, 0);
        lblReader.Name = "lblReader";
        lblReader.Size = new Size(76, 25);
        lblReader.TabIndex = 0;
        lblReader.Text = "Bạn đọc";
        // 
        // lblBorrow
        // 
        lblBorrow.AutoSize = true;
        lblBorrow.Location = new Point(43, 133);
        lblBorrow.Margin = new Padding(4, 0, 4, 0);
        lblBorrow.Name = "lblBorrow";
        lblBorrow.Size = new Size(107, 25);
        lblBorrow.TabIndex = 1;
        lblBorrow.Text = "Ngày mượn";
        // 
        // lblDue
        // 
        lblDue.AutoSize = true;
        lblDue.Location = new Point(43, 217);
        lblDue.Margin = new Padding(4, 0, 4, 0);
        lblDue.Name = "lblDue";
        lblDue.Size = new Size(70, 25);
        lblDue.TabIndex = 2;
        lblDue.Text = "Hẹn trả";
        // 
        // lblCopies
        // 
        lblCopies.AutoSize = true;
        lblCopies.Location = new Point(43, 300);
        lblCopies.Margin = new Padding(4, 0, 4, 0);
        lblCopies.Name = "lblCopies";
        lblCopies.Size = new Size(114, 25);
        lblCopies.TabIndex = 3;
        lblCopies.Text = "Bản sao sách";
        // 
        // lblNote
        // 
        lblNote.AutoSize = true;
        lblNote.ForeColor = Color.DarkOrange;
        lblNote.Location = new Point(214, 608);
        lblNote.Margin = new Padding(4, 0, 4, 0);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(409, 25);
        lblNote.TabIndex = 4;
        lblNote.Text = "Chỉ hiển thị bản sao đang ở trạng thái SAN_SANG.";
        // 
        // cboReader
        // 
        cboReader.DropDownStyle = ComboBoxStyle.DropDownList;
        cboReader.Location = new Point(214, 42);
        cboReader.Margin = new Padding(4, 5, 4, 5);
        cboReader.Name = "cboReader";
        cboReader.Size = new Size(541, 33);
        cboReader.TabIndex = 5;
        // 
        // dtBorrow
        // 
        dtBorrow.Format = DateTimePickerFormat.Short;
        dtBorrow.Location = new Point(214, 125);
        dtBorrow.Margin = new Padding(4, 5, 4, 5);
        dtBorrow.Name = "dtBorrow";
        dtBorrow.Size = new Size(255, 31);
        dtBorrow.TabIndex = 6;
        // 
        // dtDue
        // 
        dtDue.Format = DateTimePickerFormat.Short;
        dtDue.Location = new Point(214, 211);
        dtDue.Margin = new Padding(4, 5, 4, 5);
        dtDue.Name = "dtDue";
        dtDue.Size = new Size(255, 31);
        dtDue.TabIndex = 7;
        // 
        // chkCopies
        // 
        chkCopies.CheckOnClick = true;
        chkCopies.FormattingEnabled = true;
        chkCopies.HorizontalScrollbar = true;
        chkCopies.Location = new Point(214, 300);
        chkCopies.Margin = new Padding(4, 5, 4, 5);
        chkCopies.Name = "chkCopies";
        chkCopies.Size = new Size(541, 284);
        chkCopies.TabIndex = 8;
        // 
        // btnOk
        // 
        btnOk.BackColor = Color.RoyalBlue;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.ForeColor = Color.White;
        btnOk.Location = new Point(436, 675);
        btnOk.Margin = new Padding(4, 5, 4, 5);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(157, 60);
        btnOk.TabIndex = 9;
        btnOk.Text = "Lưu phiếu";
        btnOk.UseVisualStyleBackColor = false;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Location = new Point(607, 675);
        btnCancel.Margin = new Padding(4, 5, 4, 5);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(150, 60);
        btnCancel.TabIndex = 10;
        btnCancel.Text = "Hủy";
        // 
        // LoanCreateDialog
        // 
        AcceptButton = btnOk;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(814, 775);
        Controls.Add(lblReader);
        Controls.Add(lblBorrow);
        Controls.Add(lblDue);
        Controls.Add(lblCopies);
        Controls.Add(lblNote);
        Controls.Add(cboReader);
        Controls.Add(dtBorrow);
        Controls.Add(dtDue);
        Controls.Add(chkCopies);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoanCreateDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Lập phiếu mượn";
        ResumeLayout(false);
        PerformLayout();
    }
}