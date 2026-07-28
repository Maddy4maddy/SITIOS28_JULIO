using MySql.Data.MySqlClient;
using PROYECTO2_WEBService.AccesoDatos.Infraestructura;  
using PROYECTO2_WEBService.Modelo;
using System;

namespace PROYECTO2_WEBService.AccesoDatos.Repositorios
{
    public class EmpleadoRepositorio
    {
        private readonly ConnectionFactory _connectionFactory;

        public EmpleadoRepositorio()
        {
            _connectionFactory =
                new ConnectionFactory();
        }

        public bool ExisteNumeroEmpleado(
            string numeroEmpleado)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM empleados
                WHERE numero_empleado = @numeroEmpleado;";

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@numeroEmpleado",
                        numeroEmpleado);

                    int cantidad =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }

        public bool ExisteIdentificacion(
            string identificacion)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM empleados
                WHERE identificacion = @identificacion;";

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@identificacion",
                        identificacion);

                    int cantidad =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }

        public bool ExisteCorreo(
            string correo)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM empleados
                WHERE correo = @correo;";

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@correo",
                        correo);

                    int cantidad =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }

        public bool ExisteTelefono(
            string telefono)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM empleados
                WHERE telefono = @telefono;";

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@telefono",
                        telefono);

                    int cantidad =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }

        public bool ExistePuesto(
            int idPuesto)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM puestos
                WHERE id = @idPuesto;"; 

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@idPuesto",
                        idPuesto);

                    int cantidad =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }

        public int CrearEmpleado(
            CrearEmpleadoRequest request,
            DateTime fechaNacimiento,
            DateTime fechaContratacion)
        {
            const string query = @"
                INSERT INTO empleados
                (
                    numero_empleado,
                    identificacion,
                    tipo_identificacion,
                    nombre_completo,
                    fecha_nacimiento,
                    correo,
                    telefono,
                    id_puesto,
                    fecha_contratacion,
                    estado
                )
                VALUES
                (
                    @numeroEmpleado,
                    @identificacion,
                    @tipoIdentificacion,
                    @nombreCompleto,
                    @fechaNacimiento,
                    @correo,
                    @telefono,
                    @idPuesto,
                    @fechaContratacion,
                    @estado
                );

                SELECT LAST_INSERT_ID();";

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@numeroEmpleado",
                        request.NumeroEmpleado);

                    cmd.Parameters.AddWithValue(
                        "@identificacion",
                        request.Identificacion);

                    cmd.Parameters.AddWithValue(
                        "@tipoIdentificacion",
                        request.TipoIdentificacion);

                    cmd.Parameters.AddWithValue(
                        "@nombreCompleto",
                        request.NombreCompleto);

                    cmd.Parameters.AddWithValue(
                        "@fechaNacimiento",
                        fechaNacimiento);

                    cmd.Parameters.AddWithValue(
                        "@correo",
                        request.Correo);

                    cmd.Parameters.AddWithValue(
                        "@telefono",
                        request.Telefono);

                    cmd.Parameters.AddWithValue(
                        "@idPuesto",
                        request.IdPuesto);

                    cmd.Parameters.AddWithValue(
                        "@fechaContratacion",
                        fechaContratacion);

                    cmd.Parameters.AddWithValue(
                        "@estado",
                        request.Estado);

                    object resultado =
                        cmd.ExecuteScalar();

                    return Convert.ToInt32(
                        resultado);
                }
            }
        }
    }
}