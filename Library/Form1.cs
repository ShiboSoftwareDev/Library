using System;
using Library.Data;
using Library.Models;

namespace Library
{
    public partial class MainForm : Form
    {
        private readonly DataGridView dgvAuthors = new() { Dock = DockStyle.Top, Height = 150 };
        private readonly DataGridView dgvBooks = new() { Dock = DockStyle.Fill };
        private readonly Button btnAddAuthor = new() { Text = "Add Author" };
        private readonly Button btnAddBook = new() { Text = "Add Book" };
        private readonly Button btnRefresh = new() { Text = "Refresh" };

        public MainForm()
        {
            Text = "Library";
            Width = 800; Height = 600;

            var panel = new FlowLayoutPanel { Dock = DockStyle.Bottom, AutoSize = true };
            panel.Controls.AddRange([btnAddAuthor, btnAddBook, btnRefresh]);

            Controls.AddRange([dgvBooks, dgvAuthors, panel]);

            btnAddAuthor.Click += (_, _) => new Forms.AddAuthorForm().ShowDialog(this);
            btnAddBook.Click += (_, _) => new Forms.AddBookForm().ShowDialog(this);
            btnRefresh.Click += (_, _) => LoadData();

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
        }
    }
}
