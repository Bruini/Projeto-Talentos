using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fatec.RD.Infra.Repositorio.Contexto
{
    public class DapperContexto
    {
        readonly string _connectionString;
        IDbConnection _connection;
        internal int Timeout => 100000;
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);

                if (_connection.State == ConnectionState.Closed && !string.IsNullOrEmpty(_connection.ConnectionString))
                    _connection.Open();

                return _connection;
            }
        }

        public DapperContexto()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();

            GC.SuppressFinalize(this);
        }

    }
}
