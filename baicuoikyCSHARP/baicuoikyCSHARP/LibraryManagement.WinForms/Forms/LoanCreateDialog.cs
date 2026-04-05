using System.Data;

namespace LibraryManagement.WinForms.Forms;

public partial class LoanCreateDialog : Form
{
    public int ReaderId => cboReader.SelectedValue is int id ? id : Convert.ToInt32(cboReader.SelectedValue);
    public DateTime BorrowDate => dtBorrow.Value.Date;
    public DateTime DueDate => dtDue.Value.Date;
    public IEnumerable<int> SelectedCopyIds => chkCopies.CheckedItems.Cast<DataRowView>()
        .Select(x => Convert.ToInt32(x["ban_sao_id"]));

    public LoanCreateDialog(DataTable readers, DataTable copies)
    {
        InitializeComponent();

        cboReader.DataSource = readers;
        cboReader.DisplayMember = "display_name";
        cboReader.ValueMember = "ban_doc_id";

        chkCopies.DataSource = copies;
        chkCopies.DisplayMember = "display_name";
        chkCopies.ValueMember = "ban_sao_id";

        dtBorrow.Value = DateTime.Today;
        dtDue.Value = DateTime.Today.AddDays(7);
    }
}