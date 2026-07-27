using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace PROYECTO2_WEBService.AccesoDatos.Infraestrutura
{
    public class ConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["MySQLConnection"]
                .ConnectionString;
        }

        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}