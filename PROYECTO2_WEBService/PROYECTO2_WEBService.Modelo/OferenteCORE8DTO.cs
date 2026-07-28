using System;
using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [Serializable]
    [DataContract]
    public class OferenteCORE8DTO
    {
        [DataMember(Name = "CodigoOferente")]
        public string CodigoOferente { get; set; }

        [DataMember(Name = "Identificacion")]
        public string Identificacion { get; set; }

        [DataMember(Name = "TipoIdentificacion")]
        public string TipoIdentificacion { get; set; }

        [DataMember(Name = "NombreCompleto")]
        public string NombreCompleto { get; set; }

        [DataMember(Name = "FechaNacimiento")]
        public string FechaNacimiento { get; set; }

        [DataMember(Name = "Correo")]
        public string Correo { get; set; }

        [DataMember(Name = "Telefono")]
        public string Telefono { get; set; }
    }
}