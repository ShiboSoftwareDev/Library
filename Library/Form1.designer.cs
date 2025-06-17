using System.ComponentModel;

namespace Library
{
    public partial class MainForm
    {
        private IContainer components = null!;

        private Label lblTitle = null!;
        private FlowLayoutPanel buttonBar = null!;
        private Button btnAddAuthor = null!;
        private Button btnAddBook = null!;
        private TableLayoutPanel layout = null!;
        private DataGridView dgvAuthors = null!;
        private DataGridView dgvBooks = null!;

        private void InitializeComponent()
        {
            components = new Container();

            lblTitle = new Label
            {
                Text = "📚  Library dashboard",
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = ColorPrimary,
                ForeColor = Color.White,
                Font = FontHeader,
                Padding = new Padding(20, 0, 0, 0)
            };

            btnAddAuthor = CreateAccentButton("➕  New Author");
            btnAddBook = CreateAccentButton("➕  New Book");

            buttonBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 45,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(15, 5, 0, 5),
                BackColor = ColorCard
            };
            buttonBar.Controls.AddRange(new Control[] { btnAddAuthor, btnAddBook });

            dgvAuthors = CreateGrid("Authors");
            dgvBooks = CreateGrid("Books");

            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorCard,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 35));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 65));
            layout.Controls.Add(dgvAuthors, 0, 0);
            layout.Controls.Add(dgvBooks, 0, 1);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = ColorCard;
            ClientSize = new Size(900, 600);
            MinimumSize = new Size(800, 500);
            Text = "Library";

            Controls.Add(layout);
            Controls.Add(buttonBar);
            Controls.Add(lblTitle);
        }

        private Button CreateAccentButton(string text) =>
            new()
            {
                Text = text,
                AutoSize = true,
                Font = FontNormal,
                BackColor = ColorPrimary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Padding = new Padding(10, 5, 10, 5),
                Margin = new Padding(0, 0, 12, 0),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };

        private DataGridView CreateGrid(string tag)
        {
            var g = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                GridColor = Color.WhiteSmoke,
                RowTemplate = { Height = 28 },
                Tag = tag
            };

            g.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorPrimary,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            g.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorTranslator.FromHtml("#eaf0f8")
            };
            return g;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
