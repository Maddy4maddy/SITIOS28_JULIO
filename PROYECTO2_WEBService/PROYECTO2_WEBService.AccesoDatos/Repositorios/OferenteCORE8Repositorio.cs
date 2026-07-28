using MySql.Data.MySqlClient;
using System;
using PROYECTO2_WEBService.Modelo;
using PROYECTO2_WEBService.AccesoDatos.Infraestructura;

namespace PROYECTO2_WEBService.AccesoDatos.Repositorios
{
    public class OferenteCORE8Repositorio
    {
        private readonly ConnectionFactory _connectionFactory;

        public OferenteCORE8Repositorio()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public OferenteCORE8DTO ObtenerOferentePorCodigo(string codigo)
        {
            OferenteCORE8DTO oferente = null;

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT o.codigo_oferente, o.identificacion, o.tipo_identificacion,
                           o.nombre_completo, o.fecha_nacimiento, o.correo, o.telefono
                    FROM oferentes o
                    WHERE o.codigo_oferente = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oferente = new OferenteCORE8DTO
                            {
                                CodigoOferente = reader["codigo_oferente"]?.ToString() ?? "",
                                Identificacion = reader["identificacion"]?.ToString() ?? "",
                                TipoIdentificacion = reader["tipo_identificacion"]?.ToString() ?? "",
                                NombreCompleto = reader["nombre_completo"]?.ToString() ?? "",
                                FechaNacimiento = reader["fecha_nacimiento"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fecha_nacimiento"]).ToString("yyyy-MM-dd")
                                    : "",
                                Correo = reader["correo"]?.ToString() ?? "",
                                Telefono = reader["telefono"]?.ToString() ?? ""
                            };
                        }
                    }
                }
            }

            return oferente;
        }
    }
}