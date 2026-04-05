namespace LibraryManagement.WinForms.Forms;

partial class BookDialog
{
    private System.ComponentModel.IContainer components = null;

    private TextBox txtTitle;
    private TextBox txtAuthor;
    private TextBox txtPublisher;
    private NumericUpDown nudYear;
    private TextBox txtIsbn;
    private TextBox txtLanguage;
    private NumericUpDown nudPrice;
    private ComboBox cboCategory;
    private NumericUpDown nudCopies;
    private TextBox txtDescription;
    private Label lblNote;

    private TableLayoutPanel tableLayoutPanel1;
    private FlowLayoutPanel flowLayoutPanel1;
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
        txtTitle = new TextBox();
        txtAuthor = new TextBox();
        txtPublisher = new TextBox();
        nudYear = new NumericUpDown();
        txtIsbn = new TextBox();
        txtLanguage = new TextBox();
        nudPrice = new NumericUpDown();
        cboCategory = new ComboBox();
        nudCopies = new NumericUpDown();
        txtDescription = new TextBox();
        lblNote = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        lblTitle = new Label();
        lblAuthor = new Label();
        lblPublisher = new Label();
        lblYear = new Label();
        lblIsbn = new Label();
        lblLanguage = new Label();
        lblPrice = new Label();
        lblCategory = new Label();
        lblCopies = new Label();
        lblDescription = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        btnCancel = new Button();
        btnOk = new Button();
        ((System.ComponentModel.ISupportInitialize)nudYear).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudCopies).BeginInit();
        tableLayoutPanel1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // txtTitle
        // 
        txtTitle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtTitle.Location = new Point(219, 52);
        txtTitle.Margin = new Padding(4, 5, 4, 5);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(551, 31);
        txtTitle.TabIndex = 1;
        // 
        // txtAuthor
        // 
        txtAuthor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtAuthor.Location = new Point(219, 122);
        txtAuthor.Margin = new Padding(4, 5, 4, 5);
        txtAuthor.Name = "txtAuthor";
        txtAuthor.Size = new Size(551, 31);
        txtAuthor.TabIndex = 3;
        // 
        // txtPublisher
        // 
        txtPublisher.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtPublisher.Location = new Point(219, 192);
        txtPublisher.Margin = new Padding(4, 5, 4, 5);
        txtPublisher.Name = "txtPublisher";
        txtPublisher.Size = new Size(551, 31);
        txtPublisher.TabIndex = 5;
        // 
        // nudYear
        // 
        nudYear.Anchor = AnchorStyles.Left;
        nudYear.Location = new Point(219, 262);
        nudYear.Margin = new Padding(4, 5, 4, 5);
        nudYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
        nudYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
        nudYear.Name = "nudYear";
        nudYear.Size = new Size(129, 31);
        nudYear.TabIndex = 7;
        nudYear.Value = new decimal(new int[] { 1900, 0, 0, 0 });
        // 
        // txtIsbn
        // 
        txtIsbn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtIsbn.Location = new Point(219, 332);
        txtIsbn.Margin = new Padding(4, 5, 4, 5);
        txtIsbn.Name = "txtIsbn";
        txtIsbn.Size = new Size(551, 31);
        txtIsbn.TabIndex = 9;
        // 
        // txtLanguage
        // 
        txtLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtLanguage.Location = new Point(219, 402);
        txtLanguage.Margin = new Padding(4, 5, 4, 5);
        txtLanguage.Name = "txtLanguage";
        txtLanguage.Size = new Size(551, 31);
        txtLanguage.TabIndex = 11;
        txtLanguage.Text = "Tiếng Việt";
        // 
        // nudPrice
        // 
        nudPrice.Anchor = AnchorStyles.Left;
        nudPrice.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
        nudPrice.Location = new Point(219, 472);
        nudPrice.Margin = new Padding(4, 5, 4, 5);
        nudPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        nudPrice.Name = "nudPrice";
        nudPrice.Size = new Size(129, 31);
        nudPrice.TabIndex = 13;
        // 
        // cboCategory
        // 
        cboCategory.Anchor = AnchorStyles.Left;
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Location = new Point(219, 541);
        cboCategory.Margin = new Padding(4, 5, 4, 5);
        cboCategory.Name = "cboCategory";
        cboCategory.Size = new Size(129, 33);
        cboCategory.TabIndex = 15;
        // 
        // nudCopies
        // 
        nudCopies.Anchor = AnchorStyles.Left;
        nudCopies.Location = new Point(219, 612);
        nudCopies.Margin = new Padding(4, 5, 4, 5);
        nudCopies.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        nudCopies.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudCopies.Name = "nudCopies";
        nudCopies.Size = new Size(129, 31);
        nudCopies.TabIndex = 17;
        nudCopies.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // txtDescription
        // 
        txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtDescription.Location = new Point(219, 673);
        txtDescription.Margin = new Padding(4, 5, 4, 5);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Size = new Size(551, 147);
        txtDescription.TabIndex = 19;
        // 
        // lblNote
        // 
        lblNote.AutoSize = true;
        lblNote.ForeColor = Color.DarkOrange;
        lblNote.Location = new Point(219, 847);
        lblNote.Margin = new Padding(4, 17, 4, 5);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(0, 25);
        lblNote.TabIndex = 20;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.AutoScroll = true;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 186F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(lblTitle, 0, 0);
        tableLayoutPanel1.Controls.Add(txtTitle, 1, 0);
        tableLayoutPanel1.Controls.Add(lblAuthor, 0, 1);
        tableLayoutPanel1.Controls.Add(txtAuthor, 1, 1);
        tableLayoutPanel1.Controls.Add(lblPublisher, 0, 2);
        tableLayoutPanel1.Controls.Add(txtPublisher, 1, 2);
        tableLayoutPanel1.Controls.Add(lblYear, 0, 3);
        tableLayoutPanel1.Controls.Add(nudYear, 1, 3);
        tableLayoutPanel1.Controls.Add(lblIsbn, 0, 4);
        tableLayoutPanel1.Controls.Add(txtIsbn, 1, 4);
        tableLayoutPanel1.Controls.Add(lblLanguage, 0, 5);
        tableLayoutPanel1.Controls.Add(txtLanguage, 1, 5);
        tableLayoutPanel1.Controls.Add(lblPrice, 0, 6);
        tableLayoutPanel1.Controls.Add(nudPrice, 1, 6);
        tableLayoutPanel1.Controls.Add(lblCategory, 0, 7);
        tableLayoutPanel1.Controls.Add(cboCategory, 1, 7);
        tableLayoutPanel1.Controls.Add(lblCopies, 0, 8);
        tableLayoutPanel1.Controls.Add(nudCopies, 1, 8);
        tableLayoutPanel1.Controls.Add(lblDescription, 0, 9);
        tableLayoutPanel1.Controls.Add(txtDescription, 1, 9);
        tableLayoutPanel1.Controls.Add(lblNote, 1, 10);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(29, 33, 29, 33);
        tableLayoutPanel1.RowCount = 11;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 167F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.Size = new Size(829, 808);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.Anchor = AnchorStyles.Left;
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(33, 55);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(69, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Tiêu đề";
        // 
        // lblAuthor
        // 
        lblAuthor.Anchor = AnchorStyles.Left;
        lblAuthor.AutoSize = true;
        lblAuthor.Location = new Point(33, 125);
        lblAuthor.Margin = new Padding(4, 0, 4, 0);
        lblAuthor.Name = "lblAuthor";
        lblAuthor.Size = new Size(65, 25);
        lblAuthor.TabIndex = 2;
        lblAuthor.Text = "Tác giả";
        // 
        // lblPublisher
        // 
        lblPublisher.Anchor = AnchorStyles.Left;
        lblPublisher.AutoSize = true;
        lblPublisher.Location = new Point(33, 195);
        lblPublisher.Margin = new Padding(4, 0, 4, 0);
        lblPublisher.Name = "lblPublisher";
        lblPublisher.Size = new Size(117, 25);
        lblPublisher.TabIndex = 4;
        lblPublisher.Text = "Nhà xuất bản";
        // 
        // lblYear
        // 
        lblYear.Anchor = AnchorStyles.Left;
        lblYear.AutoSize = true;
        lblYear.Location = new Point(33, 265);
        lblYear.Margin = new Padding(4, 0, 4, 0);
        lblYear.Name = "lblYear";
        lblYear.Size = new Size(76, 25);
        lblYear.TabIndex = 6;
        lblYear.Text = "Năm XB";
        // 
        // lblIsbn
        // 
        lblIsbn.Anchor = AnchorStyles.Left;
        lblIsbn.AutoSize = true;
        lblIsbn.Location = new Point(33, 335);
        lblIsbn.Margin = new Padding(4, 0, 4, 0);
        lblIsbn.Name = "lblIsbn";
        lblIsbn.Size = new Size(50, 25);
        lblIsbn.TabIndex = 8;
        lblIsbn.Text = "ISBN";
        // 
        // lblLanguage
        // 
        lblLanguage.Anchor = AnchorStyles.Left;
        lblLanguage.AutoSize = true;
        lblLanguage.Location = new Point(33, 405);
        lblLanguage.Margin = new Padding(4, 0, 4, 0);
        lblLanguage.Name = "lblLanguage";
        lblLanguage.Size = new Size(94, 25);
        lblLanguage.TabIndex = 10;
        lblLanguage.Text = "Ngôn ngữ";
        // 
        // lblPrice
        // 
        lblPrice.Anchor = AnchorStyles.Left;
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(33, 475);
        lblPrice.Margin = new Padding(4, 0, 4, 0);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(66, 25);
        lblPrice.TabIndex = 12;
        lblPrice.Text = "Giá bìa";
        // 
        // lblCategory
        // 
        lblCategory.Anchor = AnchorStyles.Left;
        lblCategory.AutoSize = true;
        lblCategory.Location = new Point(33, 545);
        lblCategory.Margin = new Padding(4, 0, 4, 0);
        lblCategory.Name = "lblCategory";
        lblCategory.Size = new Size(93, 25);
        lblCategory.TabIndex = 14;
        lblCategory.Text = "Danh mục";
        // 
        // lblCopies
        // 
        lblCopies.Anchor = AnchorStyles.Left;
        lblCopies.AutoSize = true;
        lblCopies.Location = new Point(33, 615);
        lblCopies.Margin = new Padding(4, 0, 4, 0);
        lblCopies.Name = "lblCopies";
        lblCopies.Size = new Size(101, 25);
        lblCopies.TabIndex = 16;
        lblCopies.Text = "Số bản sao";
        // 
        // lblDescription
        // 
        lblDescription.Anchor = AnchorStyles.Left;
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(33, 734);
        lblDescription.Margin = new Padding(4, 0, 4, 0);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(59, 25);
        lblDescription.TabIndex = 18;
        lblDescription.Text = "Mô tả";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(btnCancel);
        flowLayoutPanel1.Controls.Add(btnOk);
        flowLayoutPanel1.Dock = DockStyle.Bottom;
        flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new Point(0, 808);
        flowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Padding = new Padding(14, 17, 14, 17);
        flowLayoutPanel1.Size = new Size(829, 92);
        flowLayoutPanel1.TabIndex = 1;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Location = new Point(654, 22);
        btnCancel.Margin = new Padding(4, 5, 4, 5);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(143, 53);
        btnCancel.TabIndex = 0;
        btnCancel.Text = "Hủy";
        // 
        // btnOk
        // 
        btnOk.BackColor = Color.SeaGreen;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.ForeColor = Color.White;
        btnOk.Location = new Point(503, 22);
        btnOk.Margin = new Padding(4, 5, 4, 5);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(143, 53);
        btnOk.TabIndex = 1;
        btnOk.Text = "Lưu";
        btnOk.UseVisualStyleBackColor = false;
        // 
        // BookDialog
        // 
        AcceptButton = btnOk;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(829, 900);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(flowLayoutPanel1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "BookDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Sách";
        ((System.ComponentModel.ISupportInitialize)nudYear).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudCopies).EndInit();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label lblTitle;
    private Label lblAuthor;
    private Label lblPublisher;
    private Label lblYear;
    private Label lblIsbn;
    private Label lblLanguage;
    private Label lblPrice;
    private Label lblCategory;
    private Label lblCopies;
    private Label lblDescription;
}