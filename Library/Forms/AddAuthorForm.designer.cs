using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Library.Forms
{
    public partial class AddAuthorForm
    {
        private IContainer components = null!;

        private TextBox txtFirst = null!;
        private TextBox txtLast = null!;
        private NumericUpDown numAge = null!;
        private Button btnSave = null!;

        private void InitializeComponent()
        {
            components = new Container();

            Text = "Add new author";
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(20);
            BackColor = ColorTranslator.FromHtml("#f4f7fb");
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;

            txtFirst = CreateTextBox("First name …");
            txtLast = CreateTextBox("Last name …");

            numAge = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 120,
                Font = FontNormal,
                Width = 80
            };

            btnSave = new Button
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

            addRow("First name", txtFirst);
            addRow("Last name", txtLast);
            addRow("Age", numAge);

            tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tbl.Controls.Add(btnSave, 1, tbl.RowCount);

            Controls.Add(tbl);
        }

        private TextBox CreateTextBox(string placeholder) =>
            new()
            {
                Font = FontNormal,
                Width = 200,
                PlaceholderText = placeholder
            };

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
