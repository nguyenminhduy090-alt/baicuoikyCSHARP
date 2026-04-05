namespace LibraryManagement.WinForms.Forms;

public partial class ReturnConditionDialog : Form
{
    private readonly ComboBox _cboCondition = new();
    private readonly Label _lblHint = new();
    private readonly Button _btnSave = new();
    private readonly Button _btnCancel = new();

    public string ReturnCondition => _cboCondition.Text.Trim();

    public ReturnConditionDialog(string initialValue = "Tốt")
    {
        InitializeComponent();

        string normalized = string.IsNullOrWhiteSpace(initialValue) ? "Tốt" : initialValue.Trim();
        if (!_cboCondition.Items.Contains(normalized))
            _cboCondition.Items.Add(normalized);

        _cboCondition.Text = normalized;
    }

    private void InitializeComponent()
    {
        Text = "Tình trạng lúc trả";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(420, 170);

        var lblTitle = new Label
        {
            AutoSize = true,
            Left = 24,
            Top = 24,
            Text = "Chọn hoặc nhập tình trạng sách khi trả"
        };

        _cboCondition.Left = 24;
        _cboCondition.Top = 58;
        _cboCondition.Width = 360;
        _cboCondition.DropDownStyle = ComboBoxStyle.DropDown;
        _cboCondition.Items.AddRange(new object[]
        {
            "Tốt",
            "Bình thường",
            "Cũ",
            "Rách nhẹ",
            "Hỏng"
        });

        _lblHint.AutoSize = true;
        _lblHint.Left = 24;
        _lblHint.Top = 96;
        _lblHint.ForeColor = Color.DimGray;
        _lblHint.Text = "Ví dụ: Tốt, Cũ, Rách nhẹ, Hỏng...";

        _btnSave.Text = "Lưu";
        _btnSave.DialogResult = DialogResult.OK;
        _btnSave.Left = 198;
        _btnSave.Top = 122;
        _btnSave.Width = 90;

        _btnCancel.Text = "Hủy";
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Left = 294;
        _btnCancel.Top = 122;
        _btnCancel.Width = 90;

        AcceptButton = _btnSave;
        CancelButton = _btnCancel;

        Controls.Add(lblTitle);
        Controls.Add(_cboCondition);
        Controls.Add(_lblHint);
        Controls.Add(_btnSave);
        Controls.Add(_btnCancel);

        FormClosing += ReturnConditionDialog_FormClosing;
    }

    private void ReturnConditionDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK) return;

        if (string.IsNullOrWhiteSpace(ReturnCondition))
        {
            MessageBox.Show("Vui lòng nhập tình trạng lúc trả.");
            e.Cancel = true;
        }
    }
}
