using PROYECTO2_WEBService.MODELOS;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IServicioEmpleados
    {
        /*
         * Petición previa que realiza el navegador antes del POST.
         * Es necesaria cuando la interfaz y el servicio WCF
         * se ejecutan desde direcciones o puertos diferentes.
         */
        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "CrearEmpleado"
        )]
        void OpcionesCrearEmpleado();

        /*
         * Recibe en el cuerpo de la petición toda la información
         * necesaria para registrar un nuevo empleado.
         */
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "CrearEmpleado",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare
        )]
        CrearEmpleadoResponse CrearEmpleado(
            EntradaRegistrarEmpleado request
        );
    }
}