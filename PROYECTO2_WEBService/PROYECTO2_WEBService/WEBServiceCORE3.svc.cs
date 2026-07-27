using System;
using System.Net;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Logica.Services;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    public class ServicioEmpleados : IServicioEmpleados
    {
        private readonly EmpleadoService _empleadoService =
            new EmpleadoService();

        private void ConfigurarCors()
        {
            if (WebOperationContext.Current == null)
            {
                return;
            }

            WebOperationContext.Current
                .OutgoingResponse
                .Headers[
                    "Access-Control-Allow-Origin"
                ] = "http://localhost:8000";

            WebOperationContext.Current
                .OutgoingResponse
                .Headers[
                    "Access-Control-Allow-Methods"
                ] = "GET, POST, OPTIONS";

            WebOperationContext.Current
                .OutgoingResponse
                .Headers[
                    "Access-Control-Allow-Headers"
                ] = "Content-Type, Accept";

            WebOperationContext.Current
                .OutgoingResponse
                .ContentType =
                "application/json; charset=utf-8";
        }

        public CrearEmpleadoResponse CrearEmpleado(
            CrearEmpleadoRequest request)
        {
            ConfigurarCors();

            try
            {
                CrearEmpleadoResponse respuesta =
                    _empleadoService
                        .CrearEmpleado(request);

                if (respuesta.Exito)
                {
                    EstablecerEstadoHttp(
                        HttpStatusCode.Created
                    );
                }
                else
                {
                    EstablecerEstadoHttp(
                        ObtenerEstadoError(
                            respuesta.Mensaje
                        )
                    );
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                EstablecerEstadoHttp(
                    HttpStatusCode.InternalServerError
                );

                System.Diagnostics.Debug.WriteLine(
                    "Error en Core3: " + ex.Message
                );

                return new CrearEmpleadoResponse
                {
                    Exito = false,
                    Mensaje =
                        "Ocurrió un error inesperado al crear el empleado.",
                    IdEmpleado = 0,
                    NumeroEmpleado = string.Empty
                };
            }
        }

        public bool ExisteEmpleado(
            string numeroEmpleado)
        {
            ConfigurarCors();

            try
            {
                bool existe =
                    _empleadoService
                        .ExisteEmpleado(numeroEmpleado);

                EstablecerEstadoHttp(
                    HttpStatusCode.OK
                );

                return existe;
            }
            catch (Exception ex)
            {
                EstablecerEstadoHttp(
                    HttpStatusCode.InternalServerError
                );

                System.Diagnostics.Debug.WriteLine(
                    "Error al verificar empleado: " +
                    ex.Message
                );

                return false;
            }
        }

        public void OpcionesCrearEmpleado()
        {
            ConfigurarCors();

            EstablecerEstadoHttp(
                HttpStatusCode.OK
            );
        }

        private HttpStatusCode ObtenerEstadoError(
            string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                return HttpStatusCode.BadRequest;
            }

            string mensajeNormalizado =
                mensaje.ToLowerInvariant();

            if (
                mensajeNormalizado.Contains(
                    "ya está registrado"
                ) ||
                mensajeNormalizado.Contains(
                    "ya esta registrado"
                ) ||
                mensajeNormalizado.Contains(
                    "ya está registrada"
                ) ||
                mensajeNormalizado.Contains(
                    "ya esta registrada"
                ) ||
                mensajeNormalizado.Contains(
                    "ya existe"
                ) ||
                mensajeNormalizado.Contains(
                    "duplicado"
                ) ||
                mensajeNormalizado.Contains(
                    "duplicada"
                )
            )
            {
                return HttpStatusCode.Conflict;
            }

            return HttpStatusCode.BadRequest;
        }

        private void EstablecerEstadoHttp(
            HttpStatusCode estado)
        {
            if (WebOperationContext.Current == null)
            {
                return;
            }

            WebOperationContext.Current
                .OutgoingResponse
                .StatusCode = estado;

            WebOperationContext.Current
                .OutgoingResponse
                .ContentType =
                "application/json; charset=utf-8";
        }
    }
}