namespace LibraryManagement.WinForms.Forms;

public partial class LoanEditDialog : Form
{
    public DateTime BorrowDate => dtBorrow.Value.Date;
    public DateTime DueDate => dtDue.Value.Date;
    public int ExtendCount => (int)nudExtend.Value;

    public LoanEditDialog(string title, DateTime borrowDate, DateTime dueDate, int extendCount)
    {
        InitializeComponent();
        Text = title;

        dtBorrow.Value = borrowDate;
        dtDue.Value = dueDate;
        nudExtend.Value = extendCount >= 0 ? extendCount : 0;

        dtBorrow.Enabled = false;

        FormClosing += LoanEditDialog_FormClosing;
    }

    private void LoanEditDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (DialogResult != DialogResult.OK) return;

        if (DueDate < BorrowDate)
        {
            MessageBox.Show("Hạn trả phải lớn hơn hoặc bằng ngày mượn.");
            e.Cancel = true;
        }
    }
}