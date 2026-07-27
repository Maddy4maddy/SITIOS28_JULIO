using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    [ServiceContract]
    public interface IWEBSERVICEcore4
    {

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Login")]
        LoginResponse Login(LoginRequest request);


        [OperationContract]
        [WebInvoke(
            Method = "OPTIONS",
            UriTemplate = "*")]
        void Options();

    }
}