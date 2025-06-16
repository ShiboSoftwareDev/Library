// MainForm.cs   —  paste over the entire existing file
using Library.Data;
using Library.Models;
using System.Drawing;

namespace Library
{
    public partial class MainForm : Form
    {
        // ---------- palette & fonts ----------
        private readonly Color ColorPrimary = ColorTranslator.FromHtml("#23395d");
        private readonly Color ColorCard = ColorTranslator.FromHtml("#f4f7fb");
        private readonly Font FontHeader = new("Segoe UI", 13f, FontStyle.Bold);
        private readonly Font FontNormal = new("Segoe UI", 9f);

        // ---------- UI controls ----------
        private readonly Label lblTitle = new();
        private readonly DataGridView dgvAuthors = new();
        private readonly DataGridView dgvBooks = new();
        private readonly Button btnAddAuthor = new();
        private readonly Button btnAddBook = new();
        private readonly Button btnRefresh = new();

        public MainForm()
        {
            Text = "Library";
            Width = 900;
            Height = 600;
            MinimumSize = new Size(800, 500);
            BackColor = ColorCard;

            // ---- header strip ----
            lblTitle.Text = "📚  Library dashboard";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 50;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.BackColor = ColorPrimary;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = FontHeader;
            lblTitle.Padding = new Padding(20, 0, 0, 0);

            // ---- buttons strip ----
            ConfigureButton(btnAddAuthor, "➕  New Author");
            ConfigureButton(btnAddBook, "➕  New Book");
            ConfigureButton(btnRefresh, "🔄  Refresh");

            var buttonBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 45,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(15, 5, 0, 5),
                BackColor = ColorCard
            };
            buttonBar.Controls.AddRange([btnAddAuthor, btnAddBook, btnRefresh]);

            // ---- grids inside a single layout panel ----
            ConfigureGrid(dgvAuthors, "Authors");
            ConfigureGrid(dgvBooks, "Books");

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorCard,
                RowCount = 2,
                ColumnCount = 1,
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 35)); // top grid 35 %
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 65)); // bottom grid 65 %
            layout.Controls.Add(dgvAuthors, 0, 0);
            layout.Controls.Add(dgvBooks, 0, 1);

            // ---- assemble form ----
            Controls.AddRange([layout, buttonBar, lblTitle]);

            // ---- events ----
            btnAddAuthor.Click += (_, _) => new Forms.AddAuthorForm().ShowDialog(this);
            btnAddBook.Click += (_, _) => new Forms.AddBookForm().ShowDialog(this);
            btnRefresh.Click += (_, _) => LoadData();
            Load += (_, _) => LoadData();
        }

        // ---------- helpers ----------
        private void ConfigureButton(Button btn, string text)
        {
            btn.Text = text;
            btn.AutoSize = true;
            btn.Font = FontNormal;
            btn.BackColor = ColorPrimary;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Padding = new Padding(10, 5, 10, 5);
            btn.Margin = new Padding(0, 0, 12, 0);
            btn.Cursor = Cursors.Hand;
        }

        private void ConfigureGrid(DataGridView dgv, string tag)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorPrimary,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = Color.WhiteSmoke;
            dgv.RowTemplate.Height = 28;
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorTranslator.FromHtml("#eaf0f8")
            };
            dgv.Tag = tag;            // used in LoadData()
        }

        private void LoadData()
        {
            GlobalData.RefreshAll();

            dgvAuthors.DataSource = GlobalData.Authors.ToList();
            dgvBooks.DataSource = GlobalData.Books.Select(b => new
            {
                b.BookID,
                b.Title,
                b.PublicationYear,
                Author = b.Author?.ToString()
            }).ToList();

            // nice column captions
            if (dgvAuthors.Columns.Count > 0)
            {
                dgvAuthors.Columns[nameof(Author.AuthorID)].Visible = false;
                dgvAuthors.Columns[nameof(Author.FirstName)].HeaderText = "First name";
                dgvAuthors.Columns[nameof(Author.LastName)].HeaderText = "Last name";
            }
            if (dgvBooks.Columns.Count > 0)
            {
                dgvBooks.Columns["BookID"].Visible = false;
                dgvBooks.Columns["PublicationYear"].HeaderText = "Year";
            }
        }
    }
}
