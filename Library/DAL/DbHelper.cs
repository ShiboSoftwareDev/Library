// DAL/DbHelper.cs
using System;
using System.Data.SqlClient;

namespace Library.DAL
{
    /// <summary>
    /// Base helper that owns the raw connection string and disposes its connection.
    /// Every DAL class can inherit from this so the string stays in ONE place.
    /// </summary>
    public class DbHelper : IDisposable
    {
        // *****  استبدل هذه السلسلة ببياناتك المعتادة للاتصال  *****
        protected readonly string connectionString =
            "Server=YOUR_SERVER_NAME;Database=MyLibraryDB;Trusted_Connection=True;";

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
