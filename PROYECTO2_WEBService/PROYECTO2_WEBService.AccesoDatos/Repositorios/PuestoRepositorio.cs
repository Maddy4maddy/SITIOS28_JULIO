using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PROYECTO2_WEBService.AccesoDatos.Infraestrutura;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.AccesoDatos.Repositorios
{
    public class PuestoRepositorio
    {
        private readonly ConnectionFactory _connectionFactory;

        public PuestoRepositorio()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public List<PuestoDTO> ObtenerPuestosActivos()
        {
            List<PuestoDTO> lista = new List<PuestoDTO>();

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT id,
                           codigo_puesto,
                           nombre_puesto,
                           salario,
                           estado,
                           fecha_creacion
                    FROM puestos
                    WHERE estado = 'Activo'
                    ORDER BY nombre_puesto";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new PuestoDTO
                            {
                                Id = Convert.ToInt32(
                                    reader["id"]),

                                CodigoPuesto =
                                    reader["codigo_puesto"].ToString(),

                                NombrePuesto =
                                    reader["nombre_puesto"].ToString(),

                                Salario = Convert.ToDecimal(
                                    reader["salario"]),

                                Estado =
                                    reader["estado"].ToString(),

                                FechaCreacion =
                                    reader["fecha_creacion"] != DBNull.Value
                                        ? Convert.ToDateTime(
                                            reader["fecha_creacion"])
                                            .ToString(
                                                "yyyy-MM-dd HH:mm:ss")
                                        : DateTime.Now.ToString(
                                            "yyyy-MM-dd HH:mm:ss")
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public PuestoDTO ObtenerPuestoPorCodigo(string codigo)
        {
            PuestoDTO puesto = null;

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
            SELECT id,
                   codigo_puesto,
                   nombre_puesto,
                   salario,
                   estado,
                   fecha_creacion
            FROM puestos
            WHERE codigo_puesto = @codigo";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@codigo",
                        codigo);

                    using (MySqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            puesto = new PuestoDTO
                            {
                                Id = Convert.ToInt32(
                                    reader["id"]),

                                CodigoPuesto =
                                    reader["codigo_puesto"].ToString(),

                                NombrePuesto =
                                    reader["nombre_puesto"].ToString(),

                                Salario = Convert.ToDecimal(
                                    reader["salario"]),

                                Estado =
                                    reader["estado"].ToString(),

                                FechaCreacion =
                                    reader["fecha_creacion"] != DBNull.Value
                                        ? Convert.ToDateTime(
                                            reader["fecha_creacion"])
                                            .ToString("yyyy-MM-dd HH:mm:ss")
                                        : string.Empty
                            };
                        }
                    }
                }
            }

            return puesto;
        }

        public List<PuestoDTO> ObtenerPuestosPorSalario(
    decimal min,
    decimal max)
        {
            List<PuestoDTO> lista = new List<PuestoDTO>();

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
            SELECT id,
                   codigo_puesto,
                   nombre_puesto,
                   salario,
                   estado,
                   fecha_creacion
            FROM puestos
            WHERE estado = 'Activo'
              AND salario BETWEEN @min AND @max
            ORDER BY salario DESC";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@min", min);
                    cmd.Parameters.AddWithValue("@max", max);

                    using (MySqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new PuestoDTO
                            {
                                Id = Convert.ToInt32(
                                    reader["id"]),

                                CodigoPuesto =
                                    reader["codigo_puesto"].ToString(),

                                NombrePuesto =
                                    reader["nombre_puesto"].ToString(),

                                Salario = Convert.ToDecimal(
                                    reader["salario"]),

                                Estado =
                                    reader["estado"].ToString(),

                                FechaCreacion =
                                    reader["fecha_creacion"] != DBNull.Value
                                        ? Convert.ToDateTime(
                                            reader["fecha_creacion"])
                                            .ToString("yyyy-MM-dd HH:mm:ss")
                                        : string.Empty
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public List<PuestoDTO> ObtenerTodosLosPuestos()
        {
            List<PuestoDTO> lista = new List<PuestoDTO>();

            using (MySqlConnection conn =
                _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
            SELECT id,
                   codigo_puesto,
                   nombre_puesto,
                   salario,
                   estado,
                   fecha_creacion
            FROM puestos
            ORDER BY estado DESC,
                     nombre_puesto";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new PuestoDTO
                            {
                                Id = Convert.ToInt32(
                                    reader["id"]),

                                CodigoPuesto =
                                    reader["codigo_puesto"].ToString(),

                                NombrePuesto =
                                    reader["nombre_puesto"].ToString(),

                                Salario = Convert.ToDecimal(
                                    reader["salario"]),

                                Estado =
                                    reader["estado"].ToString(),

                                FechaCreacion =
                                    reader["fecha_creacion"] != DBNull.Value
                                        ? Convert.ToDateTime(
                                            reader["fecha_creacion"])
                                            .ToString("yyyy-MM-dd HH:mm:ss")
                                        : string.Empty
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}