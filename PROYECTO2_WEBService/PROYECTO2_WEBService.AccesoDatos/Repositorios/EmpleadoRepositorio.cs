using System;
using MySql.Data.MySqlClient;
using PROYECTO2_WEBService.AccesoDatos.Infraestrutura;
using PROYECTO2_WEBService.Modelo;

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

        public bool ExisteEmpleado(
            string numeroEmpleado)
        {
            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM empleados
                    WHERE numero_empleado =
                          @numeroEmpleado";

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

        public int CrearEmpleado(
            CrearEmpleadoRequest request,
            DateTime fechaNacimiento,
            DateTime fechaContratacion)
        {
            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
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