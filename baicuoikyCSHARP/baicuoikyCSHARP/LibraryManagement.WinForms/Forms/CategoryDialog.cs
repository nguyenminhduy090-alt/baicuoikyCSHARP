namespace LibraryManagement.WinForms.Forms;

public partial class CategoryDialog : Form
{
    public string CategoryName => _txtName.Text.Trim();
    public string DescriptionText => _txtDescription.Text.Trim();
    public bool IsActive => _chkActive.Checked;

    public CategoryDialog(string title, string? name = null, string? description = null, bool active = true)
    {
        InitializeComponent();
        Text = title;
        _txtName.Text = name ?? "";
        _txtDescription.Text = description ?? "";
        _chkActive.Checked = active;
    }
}
