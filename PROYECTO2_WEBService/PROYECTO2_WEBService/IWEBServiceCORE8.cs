using PROYECTO2_WEBService.Modelo;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IWEBServiceCORE8
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerOferente?codigo={codigo}",
            ResponseFormat = WebMessageFormat.Json)]
        OferenteCORE8DTO ObtenerOferente(string codigo);

        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "*")]
        void Options();
    }
}