using MySql.Data.MySqlClient;
using System.Configuration;

namespace PROYECTO2_WEBService.AccesoDatos.Infraestructura
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