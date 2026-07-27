using MySql.Data.MySqlClient;
using PROYECTO2_WEBService.MODELOS;
using System;
using System.Configuration;

namespace PROYECTO2_WEBService.ACCESODATOS
{
    public class EmpleadosRepository
    {
        private readonly string connectionString =
            ConfigurationManager
                .ConnectionStrings["MySQLConnection"]
                .ConnectionString;

        public int CrearEmpleado(EntradaRegistrarEmpleado empleado)
        {
            using (MySqlConnection conexion =
                new MySqlConnection(connectionString))
            {
                conexion.Open();

                using (MySqlTransaction transaccion =
                    conexion.BeginTransaction())
                {
                    try
                    {
                        if (!ExistePuesto(
                            conexion,
                            transaccion,
                            empleado.IdPuesto))
                        {
                            throw new InvalidOperationException(
                                "El puesto seleccionado no existe.");
                        }

                        string conflicto = BuscarConflictoEmpleado(
                            conexion,
                            transaccion,
                            empleado);

                        if (!string.IsNullOrWhiteSpace(conflicto))
                        {
                            throw new InvalidOperationException(conflicto);
                        }

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
                                @numero_empleado,
                                @identificacion,
                                @tipo_identificacion,
                                @nombre_completo,
                                @fecha_nacimiento,
                                @correo,
                                @telefono,
                                @id_puesto,
                                @fecha_contratacion,
                                @estado
                            );";

                        using (MySqlCommand comando =
                            new MySqlCommand(
                                query,
                                conexion,
                                transaccion))
                        {
                            comando.Parameters.Add(
                                "@numero_empleado",
                                MySqlDbType.VarChar,
                                30
                            ).Value = empleado.NumeroEmpleado.Trim();

                            comando.Parameters.Add(
                                "@identificacion",
                                MySqlDbType.VarChar,
                                50
                            ).Value = empleado.Identificacion.Trim();

                            comando.Parameters.Add(
                                "@tipo_identificacion",
                                MySqlDbType.VarChar,
                                30
                            ).Value = empleado.TipoIdentificacion.Trim();

                            comando.Parameters.Add(
                                "@nombre_completo",
                                MySqlDbType.VarChar,
                                150
                            ).Value = empleado.NombreCompleto.Trim();

                            comando.Parameters.Add(
                                "@fecha_nacimiento",
                                MySqlDbType.Date
                            ).Value = Convert.ToDateTime(
                                empleado.FechaNacimiento).Date;

                            comando.Parameters.Add(
                                "@correo",
                                MySqlDbType.VarChar,
                                150
                            ).Value = empleado.Correo
                                .Trim()
                                .ToLowerInvariant();

                            comando.Parameters.Add(
                                "@telefono",
                                MySqlDbType.VarChar,
                                20
                            ).Value = empleado.Telefono.Trim();

                            comando.Parameters.Add(
                                "@id_puesto",
                                MySqlDbType.Int32
                            ).Value = empleado.IdPuesto;

                            comando.Parameters.Add(
                                "@fecha_contratacion",
                                MySqlDbType.Date
                            ).Value = Convert.ToDateTime(
                                empleado.FechaContratacion).Date;

                            comando.Parameters.Add(
                                "@estado",
                                MySqlDbType.VarChar,
                                10
                            ).Value = empleado.Estado.Trim();

                            comando.ExecuteNonQuery();

                            int idEmpleado =
                                Convert.ToInt32(comando.LastInsertedId);

                            transaccion.Commit();

                            return idEmpleado;
                        }
                    }
                    catch
                    {
                        try
                        {
                            transaccion.Rollback();
                        }
                        catch
                        {
                            // La transacción ya no se encontraba activa.
                        }

                        throw;
                    }
                }
            }
        }

        private bool ExistePuesto(
            MySqlConnection conexion,
            MySqlTransaction transaccion,
            int idPuesto)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM puestos
                WHERE id = @idPuesto;";

            using (MySqlCommand comando =
                new MySqlCommand(
                    query,
                    conexion,
                    transaccion))
            {
                comando.Parameters.Add(
                    "@idPuesto",
                    MySqlDbType.Int32
                ).Value = idPuesto;

                int cantidad = Convert.ToInt32(
                    comando.ExecuteScalar());

                return cantidad > 0;
            }
        }

        private string BuscarConflictoEmpleado(
            MySqlConnection conexion,
            MySqlTransaction transaccion,
            EntradaRegistrarEmpleado empleado)
        {
            const string query = @"
                SELECT
                    numero_empleado,
                    identificacion,
                    correo,
                    telefono
                FROM empleados
                WHERE numero_empleado = @numeroEmpleado
                   OR identificacion = @identificacion
                   OR correo = @correo
                   OR telefono = @telefono
                LIMIT 1;";

            using (MySqlCommand comando =
                new MySqlCommand(
                    query,
                    conexion,
                    transaccion))
            {
                comando.Parameters.Add(
                    "@numeroEmpleado",
                    MySqlDbType.VarChar,
                    30
                ).Value = empleado.NumeroEmpleado.Trim();

                comando.Parameters.Add(
                    "@identificacion",
                    MySqlDbType.VarChar,
                    50
                ).Value = empleado.Identificacion.Trim();

                comando.Parameters.Add(
                    "@correo",
                    MySqlDbType.VarChar,
                    150
                ).Value = empleado.Correo
                    .Trim()
                    .ToLowerInvariant();

                comando.Parameters.Add(
                    "@telefono",
                    MySqlDbType.VarChar,
                    20
                ).Value = empleado.Telefono.Trim();

                using (MySqlDataReader lector =
                    comando.ExecuteReader())
                {
                    if (!lector.Read())
                    {
                        return string.Empty;
                    }

                    string numeroEmpleadoRegistrado =
                        lector["numero_empleado"].ToString();

                    string identificacionRegistrada =
                        lector["identificacion"].ToString();

                    string correoRegistrado =
                        lector["correo"].ToString();

                    string telefonoRegistrado =
                        lector["telefono"].ToString();

                    if (string.Equals(
                        numeroEmpleadoRegistrado,
                        empleado.NumeroEmpleado.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return "El número de empleado ya está registrado.";
                    }

                    if (string.Equals(
                        identificacionRegistrada,
                        empleado.Identificacion.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return "La identificación ya pertenece a un empleado.";
                    }

                    if (string.Equals(
                        correoRegistrado,
                        empleado.Correo.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return "El correo ya pertenece a un empleado.";
                    }

                    if (string.Equals(
                        telefonoRegistrado,
                        empleado.Telefono.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return "El teléfono ya pertenece a un empleado.";
                    }

                    return string.Empty;
                }
            }
        }
    }
}