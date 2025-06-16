using Library.DAL;
using Library.Data;
using Library.Models;
using System.Drawing;

namespace Library.Forms;

public partial class AddBookForm : Form
{
    private readonly Color ColorPrimary = ColorTranslator.FromHtml("#23395d");
    private readonly Font FontNormal = new("Segoe UI", 9f);

    public AddBookForm()
    {
        Text = "Add new book";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Padding = new Padding(20);
        BackColor = ColorTranslator.FromHtml("#f4f7fb");
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = MinimizeBox = false;

        var txtTitle = new TextBox
        {
            Font = FontNormal,
            Width = 220,
            PlaceholderText = "Book title …"
        };
        var cmbAuthor = new ComboBox
        {
            Font = FontNormal,
            DropDownStyle = ComboBoxStyle.DropDownList,
            DataSource = GlobalData.Authors,
            Width = 220
        };
        var numYear = new NumericUpDown
        {
            Font = FontNormal,
            Minimum = 1500,
            Maximum = DateTime.Now.Year,
            Width = 100
        };
        var btnSave = new Button
        {
            Text = "Save book",
            BackColor = ColorPrimary,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = FontNormal,
            AutoSize = true,
            Padding = new Padding(12, 6, 12, 6),
            Anchor = AnchorStyles.Right
        };
        btnSave.FlatAppearance.BorderSize = 0;

        var tbl = new TableLayoutPanel
        {
            ColumnCount = 2,
            AutoSize = true,
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };
        tbl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        void addRow(string label, Control ctl)
        {
            tbl.RowCount += 1;
            tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tbl.Controls.Add(new Label
            {
                Text = label,
                Font = FontNormal,
                AutoSize = true,
                Padding = new Padding(0, 5, 5, 0),
                ForeColor = ColorPrimary
            }, 0, tbl.RowCount - 1);
            tbl.Controls.Add(ctl, 1, tbl.RowCount - 1);
        }

        addRow("Title", txtTitle);
        addRow("Author", cmbAuthor);
        addRow("Year", numYear);
        tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tbl.Controls.Add(btnSave, 1, tbl.RowCount);

        Controls.Add(tbl);

        btnSave.Click += (_, _) =>
        {
            if (txtTitle.Text.Trim() == "" || cmbAuthor.SelectedItem is not Author a)
            {
                MessageBox.Show("Title and author are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var dal = new BookDAL();
            dal.Add(new Book
            {
                Title = txtTitle.Text.Trim(),
                AuthorID = a.AuthorID,
                PublicationYear = (int)numYear.Value
            });
            DialogResult = DialogResult.OK;
            Close();
        };

        txtTitle.Focus();
    }
}
