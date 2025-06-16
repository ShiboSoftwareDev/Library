using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Library.Models;

namespace Library.DAL;

public class AuthorDAL : DbHelper
{
    public List<Author> GetAll()
    {
        var list = new List<Author>();
        using var cmd = new SqlCommand("SELECT * FROM Authors", Connection);
        Connection.Open();
        using var rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            list.Add(new Author
            {
                AuthorID = rdr.GetInt32(0),
                FirstName = rdr.GetString(1),
                LastName = rdr.GetString(2),
                Age = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3)
            });
        }
        Connection.Close();
        return list;
    }

    public void Add(Author a)
    {
        const string sql = "INSERT INTO Authors (FirstName,LastName,Age) VALUES (@F,@L,@A)";
        using var cmd = new SqlCommand(sql, Connection);
        cmd.Parameters.AddWithValue("@F", a.FirstName);
        cmd.Parameters.AddWithValue("@L", a.LastName);
        cmd.Parameters.AddWithValue("@A", a.Age);
        Connection.Open();
        cmd.ExecuteNonQuery();
        Connection.Close();
    }
}
