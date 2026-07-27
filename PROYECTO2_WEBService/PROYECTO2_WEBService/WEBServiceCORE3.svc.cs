using System;
using System.Net;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Logica;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    public class WEBServiceCORE3 : IWEBServiceCORE3
    {
        private readonly EmpleadoService _empleadoService =
            new EmpleadoService();

        private void ConfigurarCors()
        {
            WebOperationContext.Current
                .OutgoingResponse
                .Headers
                .Add(
                    "Access-Control-Allow-Origin",
                    "http://localhost:8000");

            WebOperationContext.Current
                .OutgoingResponse
                .Headers
                .Add(
                    "Access-Control-Allow-Methods",
                    "GET, POST, OPTIONS");

            WebOperationContext.Current
                .OutgoingResponse
                .Headers
                .Add(
                    "Access-Control-Allow-Headers",
                    "Content-Type");
        }

        public CrearEmpleadoResponse CrearEmpleado(
            CrearEmpleadoRequest request)
        {
            ConfigurarCors();

            try
            {
                return _empleadoService
                    .CrearEmpleado(request);
            }
            catch (Exception ex)
            {
                return new CrearEmpleadoResponse
                {
                    Exito = false,
                    Mensaje =
                        "Error al crear el empleado: " +
                        ex.Message
                };
            }
        }

        public bool ExisteEmpleado(
            string numeroEmpleado)
        {
            ConfigurarCors();

            try
            {
                return _empleadoService
                    .ExisteEmpleado(numeroEmpleado);
            }
            catch
            {
                return false;
            }
        }

        public void OpcionesCrearEmpleado()
        {
            WebOperationContext.Current
                .OutgoingResponse
                .StatusCode = HttpStatusCode.OK;

            ConfigurarCors();
        }
    }
}