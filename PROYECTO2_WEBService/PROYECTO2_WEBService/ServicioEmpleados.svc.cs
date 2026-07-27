using System;
using System.Net;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.LOGICANEGOCIO;
using PROYECTO2_WEBService.MODELOS;

namespace PROYECTO2_WEBService
{
    public class ServicioEmpleados : IServicioEmpleados
    {
        /*
         * Instancia de la capa de lógica de negocio de Core3.
         *
         * El servicio WCF no debe conectarse directamente
         * con la base de datos.
         *
         * La validación se realiza en EmpleadosService
         * y el acceso a MySQL se realiza desde
         * EmpleadosRepository.
         */
        private readonly EmpleadosService _empleadosService =
            new EmpleadosService();

        /*
         * Atiende la petición OPTIONS que realiza el navegador
         * antes de enviar la petición POST.
         *
         * Esto permite que la interfaz consuma Core3 cuando
         * ambos se ejecutan desde direcciones o puertos
         * diferentes.
         */
        public void OpcionesCrearEmpleado()
        {
            AgregarEncabezadosCors();

            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current
                    .OutgoingResponse.StatusCode =
                    HttpStatusCode.OK;
            }
        }

        /*
         * Recibe la información enviada por la interfaz
         * y solicita a la lógica de negocio la creación
         * del empleado.
         */
        public CrearEmpleadoResponse CrearEmpleado(
            EntradaRegistrarEmpleado request
        )
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
                AgregarEncabezadosCors();

                /*
                 * EntradaRegistrarEmpleado ya es el modelo
                 * utilizado por las capas internas de Core3,
                 * por lo que no es necesario convertirlo
                 * a otro DTO.
                 */
                CrearEmpleadoResponse resultado =
                    _empleadosService.CrearEmpleado(request);

                respuesta.Exito =
                    resultado.Exito;

                respuesta.Mensaje =
                    resultado.Mensaje;

                respuesta.IdEmpleado =
                    resultado.IdEmpleado;

                respuesta.NumeroEmpleado =
                    resultado.NumeroEmpleado;

                if (resultado.Exito)
                {
                    EstablecerEstadoHttp(
                        HttpStatusCode.Created
                    );
                }
                else
                {
                    EstablecerEstadoHttp(
                        ObtenerEstadoError(
                            resultado.Mensaje
                        )
                    );
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                AgregarEncabezadosCors();

                EstablecerEstadoHttp(
                    HttpStatusCode.InternalServerError
                );

                respuesta.Exito = false;

                respuesta.Mensaje =
                    "Ocurrió un error inesperado al crear el empleado.";

                respuesta.IdEmpleado = 0;

                respuesta.NumeroEmpleado =
                    string.Empty;

                /*
                 * El error real se escribe únicamente en la
                 * ventana de depuración de Visual Studio.
                 *
                 * De esta manera no se muestra información
                 * interna del sistema en la respuesta enviada
                 * al usuario.
                 */
                System.Diagnostics.Debug.WriteLine(
                    "Error en Core3: " + ex.Message
                );

                return respuesta;
            }
        }

        /*
         * Determina el código HTTP que debe enviarse cuando
         * la lógica de negocio devuelve un error.
         */
        private HttpStatusCode ObtenerEstadoError(
            string mensaje
        )
        {
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                return HttpStatusCode.BadRequest;
            }

            string mensajeNormalizado =
                mensaje.ToLowerInvariant();

            /*
             * Los datos duplicados representan un conflicto
             * con un registro que ya existe en la base de datos.
             */
            if (
                mensajeNormalizado.Contains(
                    "ya está registrado"
                ) ||
                mensajeNormalizado.Contains(
                    "ya esta registrado"
                ) ||
                mensajeNormalizado.Contains(
                    "ya pertenece"
                ) ||
                mensajeNormalizado.Contains(
                    "duplicada"
                ) ||
                mensajeNormalizado.Contains(
                    "duplicado"
                ) ||
                mensajeNormalizado.Contains(
                    "ya existe"
                )
            )
            {
                return HttpStatusCode.Conflict;
            }

            /*
             * Si el puesto indicado no existe, la solicitud
             * contiene una relación inválida.
             */
            if (
                mensajeNormalizado.Contains(
                    "puesto seleccionado no existe"
                ) ||
                mensajeNormalizado.Contains(
                    "puesto no existe"
                )
            )
            {
                return HttpStatusCode.BadRequest;
            }

            /*
             * Los errores producidos por las validaciones
             * se consideran solicitudes incorrectas.
             */
            return HttpStatusCode.BadRequest;
        }

        /*
         * Establece el código HTTP de la respuesta sin producir
         * un error cuando no existe un contexto web activo.
         */
        private void EstablecerEstadoHttp(
            HttpStatusCode estado
        )
        {
            if (WebOperationContext.Current == null)
            {
                return;
            }

            WebOperationContext.Current
                .OutgoingResponse.StatusCode =
                estado;

            WebOperationContext.Current
                .OutgoingResponse.ContentType =
                "application/json; charset=utf-8";
        }

        /*
         * Agrega los encabezados necesarios para permitir que
         * la interfaz consuma este servicio desde otra dirección
         * o desde otro puerto.
         */
        private void AgregarEncabezadosCors()
        {
            if (WebOperationContext.Current == null)
            {
                return;
            }

            WebOperationContext.Current
                .OutgoingResponse.Headers[
                    "Access-Control-Allow-Origin"
                ] = "*";

            WebOperationContext.Current
                .OutgoingResponse.Headers[
                    "Access-Control-Allow-Methods"
                ] = "POST, OPTIONS";

            WebOperationContext.Current
                .OutgoingResponse.Headers[
                    "Access-Control-Allow-Headers"
                ] = "Content-Type, Accept";

            WebOperationContext.Current
                .OutgoingResponse.ContentType =
                "application/json; charset=utf-8";
        }
    }
}