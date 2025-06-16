using Library.DAL;
using Library.Models;

namespace Library.Forms;

public partial class AddAuthorForm : Form
{
    private readonly TextBox txtFirst = new() { PlaceholderText = "First name" };
    private readonly TextBox txtLast = new() { PlaceholderText = "Last name" };
    private readonly NumericUpDown numAge = new() { Minimum = 0, Maximum = 120 };
    private readonly Button btnAdd = new() { Text = "Save" };

    public AddAuthorForm()
    {
        Text = "New Author";
        AutoSize = true;

        var tbl = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 2 };
        tbl.Controls.AddRange([
            new Label { Text = "First" }, txtFirst,
            new Label { Text = "Last"  }, txtLast,
            new Label { Text = "Age"   }, numAge,
            btnAdd
        ]);
        Controls.Add(tbl);

        btnAdd.Click += (_, _) =>
        {
            if (txtFirst.Text == "" || txtLast.Text == "")
            {
                MessageBox.Show("Names required"); return;
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
    }
}
