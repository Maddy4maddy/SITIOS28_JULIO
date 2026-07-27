using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IServicioEmpleados
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CrearEmpleado"
        )]
        CrearEmpleadoResponse CrearEmpleado(
            CrearEmpleadoRequest request
        );

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate =
                "ExisteEmpleado?numeroEmpleado={numeroEmpleado}"
        )]
        bool ExisteEmpleado(
            string numeroEmpleado
        );

        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "CrearEmpleado"
        )]
        void OpcionesCrearEmpleado();
    }
}