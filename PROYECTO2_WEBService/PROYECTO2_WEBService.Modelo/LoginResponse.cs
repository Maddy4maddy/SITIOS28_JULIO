using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public string Nombre { get; set; }
    }
}