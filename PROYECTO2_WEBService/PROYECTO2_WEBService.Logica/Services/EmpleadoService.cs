using System;
using System.Globalization;
using System.Text.RegularExpressions;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica.Services
{
    public class EmpleadoService
    {
        private readonly EmpleadoRepositorio _empleadoRepositorio;

        public EmpleadoService()
        {
            _empleadoRepositorio =
                new EmpleadoRepositorio();
        }

        public bool ExisteEmpleado(
            string numeroEmpleado)
        {
            if (string.IsNullOrWhiteSpace(numeroEmpleado))
            {
                return false;
            }

            return _empleadoRepositorio
                .ExisteNumeroEmpleado(
                    numeroEmpleado.Trim());
        }

        public CrearEmpleadoResponse CrearEmpleado(
            CrearEmpleadoRequest request)
        {
            CrearEmpleadoResponse respuesta =
                new CrearEmpleadoResponse
                {
                    Exito = false,
                    Mensaje = string.Empty,
                    IdEmpleado = 0,
                    NumeroEmpleado = string.Empty
                };

            try
            {
                ValidarEmpleado(
                    request,
                    out DateTime fechaNacimiento,
                    out DateTime fechaContratacion);

                NormalizarDatos(request);

                if (_empleadoRepositorio
                    .ExisteNumeroEmpleado(
                        request.NumeroEmpleado))
                {
                    throw new ArgumentException(
                        "El número de empleado ya está registrado.");
                }

                if (_empleadoRepositorio
                    .ExisteIdentificacion(
                        request.Identificacion))
                {
                    throw new ArgumentException(
                        "La identificación ya está registrada.");
                }

                if (_empleadoRepositorio
                    .ExisteCorreo(
                        request.Correo))
                {
                    throw new ArgumentException(
                        "El correo ya está registrado.");
                }

                if (_empleadoRepositorio
                    .ExisteTelefono(
                        request.Telefono))
                {
                    throw new ArgumentException(
                        "El teléfono ya está registrado.");
                }

                if (!_empleadoRepositorio
                    .ExistePuesto(
                        request.IdPuesto))
                {
                    throw new ArgumentException(
                        "El puesto seleccionado no existe.");
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
            catch (Exception ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = ex.Message;
                respuesta.IdEmpleado = 0;
                respuesta.NumeroEmpleado =
                    string.Empty;

                return respuesta;
            }
        }

        private void ValidarEmpleado(
            CrearEmpleadoRequest request,
            out DateTime fechaNacimiento,
            out DateTime fechaContratacion)
        {
            fechaNacimiento = DateTime.MinValue;
            fechaContratacion = DateTime.MinValue;

            if (request == null)
            {
                throw new ArgumentException(
                    "No se recibieron los datos del empleado.");
            }

            if (string.IsNullOrWhiteSpace(
                request.NumeroEmpleado))
            {
                throw new ArgumentException(
                    "Debe indicar el número de empleado.");
            }

            if (request.NumeroEmpleado.Trim().Length > 30)
            {
                throw new ArgumentException(
                    "El número de empleado no puede superar los 30 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(
                request.Identificacion))
            {
                throw new ArgumentException(
                    "Debe indicar la identificación.");
            }

            if (request.Identificacion.Trim().Length > 50)
            {
                throw new ArgumentException(
                    "La identificación no puede superar los 50 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(
                request.TipoIdentificacion))
            {
                throw new ArgumentException(
                    "Debe indicar el tipo de identificación.");
            }

            string tipoIdentificacion =
                request.TipoIdentificacion.Trim();

            if (tipoIdentificacion !=
                    "Cédula de identidad" &&
                tipoIdentificacion != "DIMEX" &&
                tipoIdentificacion != "Pasaporte")
            {
                throw new ArgumentException(
                    "El tipo de identificación no es válido.");
            }

            if (string.IsNullOrWhiteSpace(
                request.NombreCompleto))
            {
                throw new ArgumentException(
                    "Debe indicar el nombre completo.");
            }

            if (request.NombreCompleto.Trim().Length > 150)
            {
                throw new ArgumentException(
                    "El nombre completo no puede superar los 150 caracteres.");
            }

            bool fechaNacimientoValida =
                DateTime.TryParseExact(
                    request.FechaNacimiento,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fechaNacimiento);

            if (!fechaNacimientoValida)
            {
                throw new ArgumentException(
                    "La fecha de nacimiento debe tener el formato yyyy-MM-dd.");
            }

            if (fechaNacimiento.Date >= DateTime.Today)
            {
                throw new ArgumentException(
                    "La fecha de nacimiento debe ser anterior a la fecha actual.");
            }

            if (string.IsNullOrWhiteSpace(
                request.Correo))
            {
                throw new ArgumentException(
                    "Debe indicar el correo.");
            }

            if (request.Correo.Trim().Length > 150)
            {
                throw new ArgumentException(
                    "El correo no puede superar los 150 caracteres.");
            }

            bool correoValido =
                Regex.IsMatch(
                    request.Correo.Trim(),
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!correoValido)
            {
                throw new ArgumentException(
                    "El formato del correo no es válido.");
            }

            if (string.IsNullOrWhiteSpace(
                request.Telefono))
            {
                throw new ArgumentException(
                    "Debe indicar el teléfono.");
            }

            bool telefonoValido =
                Regex.IsMatch(
                    request.Telefono.Trim(),
                    @"^[0-9+\-\s]{8,20}$");

            if (!telefonoValido)
            {
                throw new ArgumentException(
                    "El formato del teléfono no es válido.");
            }

            if (request.IdPuesto <= 0)
            {
                throw new ArgumentException(
                    "Debe indicar un puesto válido.");
            }

            bool fechaContratacionValida =
                DateTime.TryParseExact(
                    request.FechaContratacion,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fechaContratacion);

            if (!fechaContratacionValida)
            {
                throw new ArgumentException(
                    "La fecha de contratación debe tener el formato yyyy-MM-dd.");
            }

            if (fechaContratacion.Date <
                fechaNacimiento.Date)
            {
                throw new ArgumentException(
                    "La fecha de contratación no puede ser anterior a la fecha de nacimiento.");
            }

            if (!string.IsNullOrWhiteSpace(
                request.Estado))
            {
                string estado =
                    request.Estado.Trim();

                if (estado != "Activo" &&
                    estado != "Inactivo")
                {
                    throw new ArgumentException(
                        "El estado debe ser Activo o Inactivo.");
                }
            }
        }

        private void NormalizarDatos(
            CrearEmpleadoRequest request)
        {
            request.NumeroEmpleado =
                request.NumeroEmpleado.Trim();

            request.Identificacion =
                request.Identificacion.Trim();

            request.TipoIdentificacion =
                request.TipoIdentificacion.Trim();

            request.NombreCompleto =
                request.NombreCompleto.Trim();

            request.FechaNacimiento =
                request.FechaNacimiento.Trim();

            request.Correo =
                request.Correo
                    .Trim()
                    .ToLowerInvariant();

            request.Telefono =
                request.Telefono.Trim();

            request.FechaContratacion =
                request.FechaContratacion.Trim();

            request.Estado =
                string.IsNullOrWhiteSpace(
                    request.Estado)
                    ? "Activo"
                    : request.Estado.Trim();
        }
    }
}