// DAL/DbHelper.cs
using System;
using System.Data.SqlClient;

namespace Library.DAL
{
    public class DbHelper : IDisposable
    {
        protected readonly string connectionString =
            "Server=Shibo;Database=MyLibraryDB;Trusted_Connection=True;";

        private SqlConnection? _connection;

        protected SqlConnection Connection
        {
            get
            {
                _connection ??= new SqlConnection(connectionString);
                return _connection;
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}
