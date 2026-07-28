using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using PROYECTO2_WEBService.Modelo;
using PROYECTO2_WEBService.AccesoDatos.Infraestructura;

namespace PROYECTO2_WEBService.AccesoDatos.Repositorios
{
    public class OferenteRepositorio
    {
        private readonly ConnectionFactory _connectionFactory;

        public OferenteRepositorio()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public int ObtenerIdPuestoPorCodigo(string codigoPuesto)
        {
            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT id
                    FROM puestos
                    WHERE codigo_puesto = @codigoPuesto";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigoPuesto", codigoPuesto);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }

                    throw new Exception("Puesto no encontrado");
                }
            }
        }

        public List<OferenteResumenDTO> ObtenerOferentesPorIdPuesto(int idPuesto)
        {
            List<OferenteResumenDTO> oferentes =
                new List<OferenteResumenDTO>();

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT
                        pp.id_postulacion,
                        o.codigo_oferente,
                        o.identificacion,
                        o.nombre_completo,
                        o.correo,
                        o.telefono,
                        pp.curriculum,
                        pp.fecha_postulacion
                    FROM postulaciones_puestos pp
                    INNER JOIN oferentes o
                        ON pp.identificacion = o.identificacion
                    WHERE pp.id_puesto = @idPuesto
                    ORDER BY pp.fecha_postulacion DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPuesto", idPuesto);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oferentes.Add(new OferenteResumenDTO
                            {
                                IdPostulacion = Convert.ToInt32(
                                    reader["id_postulacion"]
                                ),

                                CodigoOferente =
                                    reader["codigo_oferente"]?.ToString() ?? "",

                                Identificacion =
                                    reader["identificacion"]?.ToString() ?? "",

                                Nombre =
                                    reader["nombre_completo"]?.ToString() ?? "",

                                Apellido = "",

                                Email =
                                    reader["correo"]?.ToString() ?? "",

                                Telefono =
                                    reader["telefono"]?.ToString() ?? "",

                                Curriculum =
                                    reader["curriculum"]?.ToString() ?? "",

                                FechaPostulacion =
                                    reader["fecha_postulacion"] != DBNull.Value
                                        ? Convert
                                            .ToDateTime(
                                                reader["fecha_postulacion"]
                                            )
                                            .ToString("yyyy-MM-dd HH:mm:ss")
                                        : ""
                            });
                        }
                    }
                }
            }

            return oferentes;
        }

        public OferenteDetalleDTO ObtenerDetalleOferente(int idPostulacion)
        {
            OferenteDetalleDTO oferente = null;

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT
                        pp.id_postulacion,
                        o.identificacion,
                        o.nombre_completo,
                        o.correo,
                        o.telefono,
                        o.fecha_nacimiento,
                        pp.curriculum,
                        pp.fecha_postulacion,
                        p.nombre_puesto,
                        p.codigo_puesto,
                        p.salario,
                        p.estado
                    FROM postulaciones_puestos pp
                    INNER JOIN oferentes o
                        ON pp.identificacion = o.identificacion
                    INNER JOIN puestos p
                        ON pp.id_puesto = p.id
                    WHERE pp.id_postulacion = @idPostulacion";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@idPostulacion",
                        idPostulacion
                    );

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oferente = new OferenteDetalleDTO
                            {
                                IdPostulacion = Convert.ToInt32(
                                    reader["id_postulacion"]
                                ),

                                Identificacion =
                                    reader["identificacion"]?.ToString() ?? "",

                                Nombre =
                                    reader["nombre_completo"]?.ToString() ?? "",

                                Apellido = "",

                                Email =
                                    reader["correo"]?.ToString() ?? "",

                                Telefono =
                                    reader["telefono"]?.ToString() ?? "",

                                Direccion = "",

                                FechaNacimiento =
                                    reader["fecha_nacimiento"] != DBNull.Value
                                        ? Convert
                                            .ToDateTime(
                                                reader["fecha_nacimiento"]
                                            )
                                            .ToString("yyyy-MM-dd")
                                        : "",

                                Curriculum =
                                    reader["curriculum"]?.ToString() ?? "",

                                FechaPostulacion =
                                    reader["fecha_postulacion"] != DBNull.Value
                                        ? Convert
                                            .ToDateTime(
                                                reader["fecha_postulacion"]
                                            )
                                            .ToString("yyyy-MM-dd HH:mm:ss")
                                        : "",

                                NombrePuesto =
                                    reader["nombre_puesto"]?.ToString() ?? "",

                                CodigoPuesto =
                                    reader["codigo_puesto"]?.ToString() ?? "",

                                Salario =
                                    reader["salario"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["salario"])
                                        : 0,

                                EstadoPuesto =
                                    reader["estado"]?.ToString() ?? ""
                            };
                        }
                    }
                }
            }

            return oferente;
        }
    }
}