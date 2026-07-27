using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IWEBServiceCORE3
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "CrearEmpleado")]
        CrearEmpleadoResponse CrearEmpleado(
            CrearEmpleadoRequest request);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ExisteEmpleado?numeroEmpleado={numeroEmpleado}")]
        bool ExisteEmpleado(string numeroEmpleado);

        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "CrearEmpleado")]
        void OpcionesCrearEmpleado();
    }
}