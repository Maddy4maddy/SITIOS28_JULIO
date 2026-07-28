using PROYECTO2_WEBService.Modelo;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IWEBServiceCORE1
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerPuestoPorCodigo?codigo={codigo}",
            ResponseFormat = WebMessageFormat.Json)]
        PuestoDTO ObtenerPuestoPorCodigo(string codigo);

        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerPuestosActivos",
            ResponseFormat = WebMessageFormat.Json)]
        List<PuestoDTO> ObtenerPuestosActivos();

        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerTodosLosPuestos",
            ResponseFormat = WebMessageFormat.Json)]
        List<PuestoDTO> ObtenerTodosLosPuestos();

        [OperationContract]
        [WebGet(
            UriTemplate = "ObtenerPuestosPorSalario?min={min}&max={max}",
            ResponseFormat = WebMessageFormat.Json)]
        List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max);

        [OperationContract]
        [WebGet(
            UriTemplate = "GetData?value={value}",
            ResponseFormat = WebMessageFormat.Json)]
        string GetData(int value);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "GetDataUsingDataContract",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "*")]
        void Options();
    }

    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; }

        [DataMember]
        public string StringValue { get; set; }
    }
}