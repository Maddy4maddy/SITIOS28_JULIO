using System;
using MySql.Data.MySqlClient;
using PROYECTO2_WEBService.AccesoDatos.Infraestructura;  
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.AccesoDatos.Repositorios
{
    public class LoginRepositorio
    {
        private readonly ConnectionFactory _connectionFactory;

        public LoginRepositorio()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public UsuarioLoginDTO ObtenerUsuarioPorNombre(
            string nombreUsuario)
        {
            UsuarioLoginDTO usuario = null;

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT
                        id_usuario,
                        nombre_completo,
                        contrasena,
                        intentos_fallidos,
                        bloqueado,
                        estado
                    FROM usuarios
                    WHERE nombre_usuario = @usuario";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@usuario",
                        nombreUsuario);

                    using (MySqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new UsuarioLoginDTO
                            {
                                IdUsuario =
                                    Convert.ToInt32(
                                        reader["id_usuario"]),

                                NombreCompleto =
                                    reader["nombre_completo"]
                                        .ToString(),

                                Contrasena =
                                    reader["contrasena"]
                                        .ToString(),

                                IntentosFallidos =
                                    Convert.ToInt32(
                                        reader["intentos_fallidos"]),

                                Bloqueado =
                                    Convert.ToBoolean(
                                        reader["bloqueado"]),

                                Estado =
                                    reader["estado"]
                                        .ToString()
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public void ActualizarIntentosFallidos(
            int idUsuario,
            int intentos)
        {
            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    UPDATE usuarios
                    SET intentos_fallidos = @intentos
                    WHERE id_usuario = @idUsuario";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@intentos",
                        intentos);

                    cmd.Parameters.AddWithValue(
                        "@idUsuario",
                        idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void BloquearUsuario(int idUsuario)
        {
            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    UPDATE usuarios
                    SET bloqueado = 1,
                        estado = 'bloqueado'
                    WHERE id_usuario = @idUsuario";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@idUsuario",
                        idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ReiniciarIntentos(int idUsuario)
        {
            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    UPDATE usuarios
                    SET intentos_fallidos = 0,
                        bloqueado = 0,
                        estado = 'activo'
                    WHERE id_usuario = @idUsuario";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@idUsuario",
                        idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}