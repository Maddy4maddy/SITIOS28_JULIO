using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using PROYECTO2_WEBService.Modelo;
using PROYECTO2_WEBService.AccesoDatos.Infraestructura;

namespace PROYECTO2_WEBService.AccesoDatos.Repositorios
{
    public class PuestoRepositorio
    {
        private readonly ConnectionFactory _connectionFactory;

        public PuestoRepositorio()
        {
            _connectionFactory = new ConnectionFactory();
        }

        public PuestoDTO ObtenerPuestoPorCodigo(string codigo)
        {
            PuestoDTO puesto = null;

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT id, codigo_puesto, nombre_puesto, salario, estado, fecha_creacion
                    FROM puestos
                    WHERE codigo_puesto = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            puesto = new PuestoDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CodigoPuesto = reader["codigo_puesto"].ToString(),
                                NombrePuesto = reader["nombre_puesto"].ToString(),
                                Salario = Convert.ToDecimal(reader["salario"]),
                                Estado = reader["estado"].ToString(),
                                FechaCreacion = reader["fecha_creacion"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fecha_creacion"]).ToString("yyyy-MM-dd HH:mm:ss")
                                    : ""
                            };
                        }
                    }
                }
            }

            return puesto;
        }

        public List<PuestoDTO> ObtenerPuestosActivos()
        {
            List<PuestoDTO> puestos = new List<PuestoDTO>();

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT id, codigo_puesto, nombre_puesto, salario, estado, fecha_creacion
                    FROM puestos
                    WHERE estado = 'Activo'
                    ORDER BY nombre_puesto ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            puestos.Add(new PuestoDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CodigoPuesto = reader["codigo_puesto"].ToString(),
                                NombrePuesto = reader["nombre_puesto"].ToString(),
                                Salario = Convert.ToDecimal(reader["salario"]),
                                Estado = reader["estado"].ToString(),
                                FechaCreacion = reader["fecha_creacion"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fecha_creacion"]).ToString("yyyy-MM-dd HH:mm:ss")
                                    : ""
                            });
                        }
                    }
                }
            }

            return puestos;
        }

        // NUEVO: Obtener todos los puestos
        public List<PuestoDTO> ObtenerTodosLosPuestos()
        {
            List<PuestoDTO> puestos = new List<PuestoDTO>();

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT id, codigo_puesto, nombre_puesto, salario, estado, fecha_creacion
                    FROM puestos
                    ORDER BY nombre_puesto ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            puestos.Add(new PuestoDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CodigoPuesto = reader["codigo_puesto"].ToString(),
                                NombrePuesto = reader["nombre_puesto"].ToString(),
                                Salario = Convert.ToDecimal(reader["salario"]),
                                Estado = reader["estado"].ToString(),
                                FechaCreacion = reader["fecha_creacion"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fecha_creacion"]).ToString("yyyy-MM-dd HH:mm:ss")
                                    : ""
                            });
                        }
                    }
                }
            }

            return puestos;
        }

        // NUEVO: Obtener puestos por rango de salario
        public List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max)
        {
            List<PuestoDTO> puestos = new List<PuestoDTO>();

            using (MySqlConnection conn = _connectionFactory.CrearConexion())
            {
                conn.Open();

                string query = @"
                    SELECT id, codigo_puesto, nombre_puesto, salario, estado, fecha_creacion
                    FROM puestos
                    WHERE salario BETWEEN @min AND @max
                    ORDER BY salario ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@min", min);
                    cmd.Parameters.AddWithValue("@max", max);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            puestos.Add(new PuestoDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CodigoPuesto = reader["codigo_puesto"].ToString(),
                                NombrePuesto = reader["nombre_puesto"].ToString(),
                                Salario = Convert.ToDecimal(reader["salario"]),
                                Estado = reader["estado"].ToString(),
                                FechaCreacion = reader["fecha_creacion"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fecha_creacion"]).ToString("yyyy-MM-dd HH:mm:ss")
                                    : ""
                            });
                        }
                    }
                }
            }

            return puestos;
        }
    }
}