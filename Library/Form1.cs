using Library.Data;
using Library.Models;
using System.Drawing;

namespace Library
{
    public partial class MainForm : Form
    {
        private readonly Color ColorPrimary = ColorTranslator.FromHtml("#23395d");
        private readonly Color ColorCard = ColorTranslator.FromHtml("#f4f7fb");
        private readonly Font FontHeader = new("Segoe UI", 13f, FontStyle.Bold);
        private readonly Font FontNormal = new("Segoe UI", 9f);

        public MainForm()
        {
            InitializeComponent();              

            btnAddAuthor.Click += (_, _) =>
            {
                using var f = new Forms.AddAuthorForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                    LoadData();
            };
            btnAddBook.Click += (_, _) =>
            {
                using var f = new Forms.AddBookForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                    LoadData();
            };
            Load += (_, _) => LoadData();
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
