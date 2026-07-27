using System;
using System.Globalization;
using System.Text.RegularExpressions;
using PROYECTO2_WEBService.ACCESODATOS;
using PROYECTO2_WEBService.MODELOS;

namespace PROYECTO2_WEBService.LOGICANEGOCIO
{
    public class EmpleadosService
    {
        private readonly EmpleadosRepository _empleadosRepository =
            new EmpleadosRepository();

        public CrearEmpleadoResponse CrearEmpleado(
            EntradaRegistrarEmpleado empleado)
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
                ValidarEmpleado(empleado);

                empleado.NumeroEmpleado =
                    empleado.NumeroEmpleado.Trim();

                empleado.Identificacion =
                    empleado.Identificacion.Trim();

                empleado.TipoIdentificacion =
                    empleado.TipoIdentificacion.Trim();

                empleado.NombreCompleto =
                    empleado.NombreCompleto.Trim();

                empleado.Correo =
                    empleado.Correo
                        .Trim()
                        .ToLowerInvariant();

                empleado.Telefono =
                    empleado.Telefono.Trim();

                empleado.FechaNacimiento =
                    empleado.FechaNacimiento.Trim();

                empleado.FechaContratacion =
                    empleado.FechaContratacion.Trim();

                empleado.Estado =
                    string.IsNullOrWhiteSpace(empleado.Estado)
                        ? "Activo"
                        : empleado.Estado.Trim();

                int idEmpleado =
                    _empleadosRepository.CrearEmpleado(empleado);

                respuesta.Exito = true;
                respuesta.Mensaje =
                    "Empleado creado con éxito.";
                respuesta.IdEmpleado = idEmpleado;
                respuesta.NumeroEmpleado =
                    empleado.NumeroEmpleado;

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = ex.Message;
                respuesta.IdEmpleado = 0;
                respuesta.NumeroEmpleado = string.Empty;

                return respuesta;
            }
        }

        private void ValidarEmpleado(
            EntradaRegistrarEmpleado empleado)
        {
            if (empleado == null)
            {
                throw new ArgumentException(
                    "Debe enviar la información del empleado.");
            }

            if (string.IsNullOrWhiteSpace(
                empleado.NumeroEmpleado))
            {
                throw new ArgumentException(
                    "El número de empleado es obligatorio.");
            }

            if (empleado.NumeroEmpleado.Trim().Length > 30)
            {
                throw new ArgumentException(
                    "El número de empleado no puede superar los 30 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(
                empleado.Identificacion))
            {
                throw new ArgumentException(
                    "La identificación es obligatoria.");
            }

            if (empleado.Identificacion.Trim().Length > 50)
            {
                throw new ArgumentException(
                    "La identificación no puede superar los 50 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(
                empleado.TipoIdentificacion))
            {
                throw new ArgumentException(
                    "El tipo de identificación es obligatorio.");
            }

            string tipoIdentificacion =
                empleado.TipoIdentificacion.Trim();

            if (tipoIdentificacion != "Cédula de identidad" &&
                tipoIdentificacion != "DIMEX" &&
                tipoIdentificacion != "Pasaporte")
            {
                throw new ArgumentException(
                    "El tipo de identificación no es válido.");
            }

            if (string.IsNullOrWhiteSpace(
                empleado.NombreCompleto))
            {
                throw new ArgumentException(
                    "El nombre completo es obligatorio.");
            }

            if (empleado.NombreCompleto.Trim().Length > 150)
            {
                throw new ArgumentException(
                    "El nombre completo no puede superar los 150 caracteres.");
            }

            DateTime fechaNacimiento;

            bool fechaNacimientoValida =
                DateTime.TryParseExact(
                    empleado.FechaNacimiento,
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
                empleado.Correo))
            {
                throw new ArgumentException(
                    "El correo es obligatorio.");
            }

            if (empleado.Correo.Trim().Length > 150)
            {
                throw new ArgumentException(
                    "El correo no puede superar los 150 caracteres.");
            }

            bool correoValido = Regex.IsMatch(
                empleado.Correo.Trim(),
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!correoValido)
            {
                throw new ArgumentException(
                    "El formato del correo no es válido.");
            }

            if (string.IsNullOrWhiteSpace(
                empleado.Telefono))
            {
                throw new ArgumentException(
                    "El teléfono es obligatorio.");
            }

            bool telefonoValido = Regex.IsMatch(
                empleado.Telefono.Trim(),
                @"^[0-9+\-\s]{8,20}$");

            if (!telefonoValido)
            {
                throw new ArgumentException(
                    "El formato del teléfono no es válido.");
            }

            if (empleado.IdPuesto <= 0)
            {
                throw new ArgumentException(
                    "Debe indicar un puesto válido.");
            }

            DateTime fechaContratacion;

            bool fechaContratacionValida =
                DateTime.TryParseExact(
                    empleado.FechaContratacion,
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
                empleado.Estado))
            {
                string estado = empleado.Estado.Trim();

                if (estado != "Activo" &&
                    estado != "Inactivo")
                {
                    throw new ArgumentException(
                        "El estado debe ser Activo o Inactivo.");
                }
            }
        }
    }
}