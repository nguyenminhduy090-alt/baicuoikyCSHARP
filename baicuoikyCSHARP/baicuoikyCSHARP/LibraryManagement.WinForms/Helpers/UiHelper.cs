namespace LibraryManagement.WinForms.Helpers;

public static class UiHelper
{
    public static void ApplyGridTheme(DataGridView grid)
    {
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);
    }

    public static Button CreateActionButton(string text, Color color)
    {
        return new Button
        {
            Text = text,
            BackColor = color,
            ForeColor = Color.White,
            Width = 120,
            Height = 36,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(5)
        };
    }

    public static Panel CreateSummaryCard(string title, string value, Color color)
    {
        var panel = new Panel
        {
            Width = 210,
            Height = 90,
            BackColor = color,
            Margin = new Padding(10),
            Padding = new Padding(12)
        };

        var lblTitle = new Label
        {
            Dock = DockStyle.Top,
            Height = 26,
            Text = title,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };

        var lblValue = new Label
        {
            Dock = DockStyle.Fill,
            Text = value,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Name = "lblValue"
        };

        panel.Controls.Add(lblValue);
        panel.Controls.Add(lblTitle);
        return panel;
    }
}
