using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Library.Data;

namespace Library.Forms
{
    public partial class AddBookForm
    {
        private IContainer components = null!;

        private TextBox txtTitle = null!;
        private ComboBox cmbAuthor = null!;
        private NumericUpDown numYear = null!;
        private Button btnSave = null!;

        private void InitializeComponent()
        {
            components = new Container();

            Text = "Add new book";
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(20);
            BackColor = ColorTranslator.FromHtml("#f4f7fb");
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;

            txtTitle = new TextBox
            {
                Font = FontNormal,
                Width = 220,
                PlaceholderText = "Book title …"
            };

            cmbAuthor = new ComboBox
            {
                Font = FontNormal,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = GlobalData.Authors,
                Width = 220
            };

            numYear = new NumericUpDown
            {
                Font = FontNormal,
                Minimum = 1500,
                Maximum = DateTime.Now.Year,
                Width = 100
            };

            btnSave = new Button
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
