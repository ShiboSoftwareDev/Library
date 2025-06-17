using Library.DAL;
using Library.Models;
using System.Drawing;

namespace Library.Forms
{
    public partial class AddAuthorForm : Form
    {
        private readonly Color ColorPrimary = ColorTranslator.FromHtml("#23395d");
        private readonly Font FontNormal = new("Segoe UI", 9f);

        public AddAuthorForm()
        {
            InitializeComponent();          

            btnSave.Click += (_, _) =>
            {
                if (txtFirst.Text.Trim() == "" || txtLast.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Name fields can’t be empty.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
    }
}
