using System;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica
{
    public class EmpleadoService
    {
        private readonly EmpleadoRepositorio _empleadoRepositorio;

        public EmpleadoService()
        {
            _empleadoRepositorio =
                new EmpleadoRepositorio();
        }

        public bool ExisteEmpleado(string numeroEmpleado)
        {
            if (string.IsNullOrWhiteSpace(numeroEmpleado))
            {
                return false;
            }

            return _empleadoRepositorio
                .ExisteEmpleado(numeroEmpleado.Trim());
        }

        public CrearEmpleadoResponse CrearEmpleado(
            CrearEmpleadoRequest request)
        {
            CrearEmpleadoResponse respuesta =
                new CrearEmpleadoResponse();

            if (request == null)
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "No se recibieron los datos del empleado.";

                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(
                request.NumeroEmpleado))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar el número de empleado.";

                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(
                request.Identificacion))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar la identificación.";

                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(
                request.TipoIdentificacion))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar el tipo de identificación.";

                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(
                request.NombreCompleto))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar el nombre completo.";

                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(
                request.Correo))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar el correo.";

                return respuesta;
            }

            if (string.IsNullOrWhiteSpace(
                request.Telefono))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar el teléfono.";

                return respuesta;
            }

            if (request.IdPuesto <= 0)
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Debe indicar un puesto válido.";

                return respuesta;
            }

            DateTime fechaNacimiento;
            DateTime fechaContratacion;

            if (!DateTime.TryParse(
                request.FechaNacimiento,
                out fechaNacimiento))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "La fecha de nacimiento no es válida.";

                return respuesta;
            }

            if (!DateTime.TryParse(
                request.FechaContratacion,
                out fechaContratacion))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "La fecha de contratación no es válida.";

                return respuesta;
            }

            request.NumeroEmpleado =
                request.NumeroEmpleado.Trim();

            request.Identificacion =
                request.Identificacion.Trim();

            request.TipoIdentificacion =
                request.TipoIdentificacion.Trim();

            request.NombreCompleto =
                request.NombreCompleto.Trim();

            request.Correo =
                request.Correo.Trim();

            request.Telefono =
                request.Telefono.Trim();

            if (string.IsNullOrWhiteSpace(
                request.Estado))
            {
                request.Estado = "Activo";
            }
            else
            {
                request.Estado =
                    request.Estado.Trim();
            }

            if (_empleadoRepositorio.ExisteEmpleado(
                request.NumeroEmpleado))
            {
                respuesta.Exito = false;
                respuesta.Mensaje =
                    "El número de empleado ya existe.";

                return respuesta;
            }

            int idEmpleado =
                _empleadoRepositorio.CrearEmpleado(
                    request,
                    fechaNacimiento,
                    fechaContratacion);

            respuesta.Exito = true;
            respuesta.Mensaje =
                "Empleado creado correctamente.";

            respuesta.IdEmpleado =
                idEmpleado;

            respuesta.NumeroEmpleado =
                request.NumeroEmpleado;

            return respuesta;
        }
    }
}