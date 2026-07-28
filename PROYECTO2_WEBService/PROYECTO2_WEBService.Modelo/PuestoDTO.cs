using System;
using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [Serializable]
    [DataContract]
    public class PuestoDTO
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "CodigoPuesto")]
        public string CodigoPuesto { get; set; }

        [DataMember(Name = "NombrePuesto")]
        public string NombrePuesto { get; set; }

        [DataMember(Name = "Salario")]
        public decimal Salario { get; set; }

        [DataMember(Name = "Estado")]
        public string Estado { get; set; }

        [DataMember(Name = "FechaCreacion")]
        public string FechaCreacion { get; set; }
    }
}