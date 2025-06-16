using Library.DAL;
using Library.Models;

namespace Library.Data;

public static class GlobalData
{
    static GlobalData() { RefreshAll(); }

    public static List<Author> Authors { get; private set; } = new();
    public static List<Book> Books { get; private set; } = new();

    public static void RefreshAuthors()
    {
        using var dal = new AuthorDAL();
        Authors = dal.GetAll();
    }

    public static void RefreshBooks()
    {
        using var dal = new BookDAL();
        Books = dal.GetAll();
    }

    public static void RefreshAll()
    {
        RefreshAuthors();
        RefreshBooks();
    }
}
