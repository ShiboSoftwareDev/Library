using System.Collections.Generic;
using System.Data.SqlClient;
using Library.Models;

namespace Library.DAL;

public class BookDAL : DbHelper
{
    public List<Book> GetAll()
    {
        var list = new List<Book>();
        const string sql = @"SELECT b.BookID, b.Title, b.PublicationYear,
                                    a.AuthorID, a.FirstName, a.LastName, a.Age
                             FROM Books b
                             JOIN Authors a ON b.AuthorID = a.AuthorID";
        using var cmd = new SqlCommand(sql, Connection);
        Connection.Open();
        using var rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            list.Add(new Book
            {
                BookID = rdr.GetInt32(0),
                Title = rdr.GetString(1),
                PublicationYear = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                AuthorID = rdr.GetInt32(3),
                Author = new Author
                {
                    AuthorID = rdr.GetInt32(3),
                    FirstName = rdr.GetString(4),
                    LastName = rdr.GetString(5),
                    Age = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6)
                }
            });
        }
        Connection.Close();
        return list;
    }

    public void Add(Book b)
    {
        const string sql = @"INSERT INTO Books (Title,AuthorID,PublicationYear)
                             VALUES (@T,@AID,@Y)";
        using var cmd = new SqlCommand(sql, Connection);
        cmd.Parameters.AddWithValue("@T", b.Title);
        cmd.Parameters.AddWithValue("@AID", b.AuthorID);
        cmd.Parameters.AddWithValue("@Y", b.PublicationYear);
        Connection.Open();
        cmd.ExecuteNonQuery();
        Connection.Close();
    }
}
