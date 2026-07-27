using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [DataContract]
    public class CrearEmpleadoResponse
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int IdEmpleado { get; set; }

        [DataMember]
        public string NumeroEmpleado { get; set; }
    }
}