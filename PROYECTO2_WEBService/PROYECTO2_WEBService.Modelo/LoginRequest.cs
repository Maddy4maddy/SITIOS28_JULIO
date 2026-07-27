using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }
    }
}