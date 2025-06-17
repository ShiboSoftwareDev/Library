using Library.DAL;
using Library.Data;
using Library.Models;
using System.Drawing;

namespace Library.Forms
{
    public partial class AddBookForm : Form
    {
        private readonly Color ColorPrimary = ColorTranslator.FromHtml("#23395d");
        private readonly Font FontNormal = new("Segoe UI", 9f);

        public AddBookForm()
        {
            InitializeComponent();          

            btnSave.Click += (_, _) =>
            {
                if (txtTitle.Text.Trim() == "" || cmbAuthor.SelectedItem is not Author a)
                {
                    MessageBox.Show(
                        "Title and author are required.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
}
