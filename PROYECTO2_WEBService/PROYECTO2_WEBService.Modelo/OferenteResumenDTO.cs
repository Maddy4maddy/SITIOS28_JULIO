using System;
using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [Serializable]
    [DataContract]
    public class OferenteResumenDTO
    {
        [DataMember(Name = "IdPostulacion")]
        public int IdPostulacion { get; set; }

        [DataMember(Name = "Identificacion")]
        public string Identificacion { get; set; }

        [DataMember(Name = "Nombre")]
        public string Nombre { get; set; }

        [DataMember(Name = "Apellido")]
        public string Apellido { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "Telefono")]
        public string Telefono { get; set; }

        [DataMember(Name = "Curriculum")]
        public string Curriculum { get; set; }

        [DataMember(Name = "FechaPostulacion")]
        public string FechaPostulacion { get; set; }
    }
}