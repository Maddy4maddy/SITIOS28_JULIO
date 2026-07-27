using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IService1
    {

        // ========================================
        // OBTENER PUESTOS ACTIVOS
        // ========================================

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ObtenerPuestosActivos")]
        List<PuestoDTO> ObtenerPuestosActivos();



        // ========================================
        // OBTENER PUESTO POR CODIGO
        // ========================================

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ObtenerPuestoPorCodigo?codigo={codigo}")]
        PuestoDTO ObtenerPuestoPorCodigo(string codigo);



        // ========================================
        // OBTENER POR RANGO SALARIO
        // ========================================

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ObtenerPuestosPorSalario?min={min}&max={max}")]
        List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max);



        // ========================================
        // OBTENER TODOS
        // ========================================

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ObtenerTodosLosPuestos")]
        List<PuestoDTO> ObtenerTodosLosPuestos();



        // ========================================
        // CORS PREFLIGHT
        // ========================================

        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "*")]
        void Options();



        // ========================================
        // MÉTODOS BASE WCF
        // ========================================

        [OperationContract]
        string GetData(int value);


        [OperationContract]
        CompositeType GetDataUsingDataContract(
            CompositeType composite);

    }

    // ========================================
    // DTO ORIGINAL WCF
    // ========================================

    [DataContract]
    public class CompositeType
    {

        bool boolValue = true;

        string stringValue = "Hello ";



        [DataMember]
        public bool BoolValue
        {
            get
            {
                return boolValue;
            }
            set
            {
                boolValue = value;
            }
        }



        [DataMember]
        public string StringValue
        {
            get
            {
                return stringValue;
            }
            set
            {
                stringValue = value;
            }
        }

    }
}