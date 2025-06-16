namespace Library.Models;

public class Author
{
    public int AuthorID { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }

    public override string ToString() => $"{FirstName} {LastName}";
}
