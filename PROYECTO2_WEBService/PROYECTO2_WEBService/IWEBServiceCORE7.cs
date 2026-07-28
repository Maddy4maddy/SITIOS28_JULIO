using PROYECTO2_WEBService.Modelo;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IWEBServiceCORE7
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerOferentesPorPuesto?codigoPuesto={codigoPuesto}",
            ResponseFormat = WebMessageFormat.Json)]
        List<OferenteResumenDTO> ObtenerOferentesPorPuesto(string codigoPuesto);

        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerDetalleOferente?idPostulacion={idPostulacion}",
            ResponseFormat = WebMessageFormat.Json)]
        OferenteDetalleDTO ObtenerDetalleOferente(string idPostulacion);

        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "*")]
        void Options();
    }
}