using Library.DAL;
using Library.Data;
using Library.Models;

namespace Library.Forms;

public partial class AddBookForm : Form
{
    private readonly TextBox txtTitle = new() { PlaceholderText = "Title" };
    private readonly ComboBox cmbAuthor = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown numYear = new() { Minimum = 1500, Maximum = DateTime.Now.Year };
    private readonly Button btnAdd = new() { Text = "Save" };

    public AddBookForm()
    {
        Text = "New Book";
        AutoSize = true;

        cmbAuthor.DataSource = GlobalData.Authors;

        var tbl = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 2 };
        tbl.Controls.AddRange([
            new Label { Text = "Title" },   txtTitle,
            new Label { Text = "Author"},   cmbAuthor,
            new Label { Text = "Year" },    numYear,
            btnAdd
        ]);
        Controls.Add(tbl);

        btnAdd.Click += (_, _) =>
        {
            if (txtTitle.Text == "" || cmbAuthor.SelectedItem is not Author a)
            {
                MessageBox.Show("Title & author required"); return;
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
    }
}
