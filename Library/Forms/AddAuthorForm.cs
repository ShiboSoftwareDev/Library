using Library.DAL;
using Library.Models;
using System.Drawing;

namespace Library.Forms;

public partial class AddAuthorForm : Form
{
    private readonly Color ColorPrimary = ColorTranslator.FromHtml("#23395d");
    private readonly Font FontNormal = new("Segoe UI", 9f);

    public AddAuthorForm()
    {
        Text = "Add new author";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Padding = new Padding(20);
        BackColor = ColorTranslator.FromHtml("#f4f7fb");
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = MinimizeBox = false;

        var txtFirst = CreateTextBox("First name …");
        var txtLast = CreateTextBox("Last name …");
        var numAge = new NumericUpDown
        {
            Minimum = 0,
            Maximum = 120,
            Font = FontNormal,
            Width = 80
        };
        var btnSave = new Button
        {
            Text = "Save author",
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
            Padding = new Padding(10),
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

        addRow("First name", txtFirst);
        addRow("Last name", txtLast);
        addRow("Age", numAge);
        tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tbl.Controls.Add(btnSave, 1, tbl.RowCount);

        Controls.Add(tbl);

        btnSave.Click += (_, _) =>
        {
            if (txtFirst.Text.Trim() == "" || txtLast.Text.Trim() == "")
            {
                MessageBox.Show("Name fields can’t be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var dal = new AuthorDAL();
            dal.Add(new Author
            {
                FirstName = txtFirst.Text.Trim(),
                LastName = txtLast.Text.Trim(),
                Age = (int)numAge.Value
            });
            DialogResult = DialogResult.OK;
            Close();
        };

        txtFirst.Focus();
    }

    private TextBox CreateTextBox(string placeholder)
        => new()
        {
            Font = FontNormal,
            Width = 200,
            PlaceholderText = placeholder
        };
}
