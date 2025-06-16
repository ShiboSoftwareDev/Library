namespace Library.Models;

public class Book
{
    public int BookID { get; set; }
    public string Title { get; set; } = "";
    public int PublicationYear { get; set; }
    public int AuthorID { get; set; }
    public Author? Author { get; set; }  
}
