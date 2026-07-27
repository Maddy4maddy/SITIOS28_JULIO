using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [DataContract]
    public class PuestoDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CodigoPuesto { get; set; }

        [DataMember]
        public string NombrePuesto { get; set; }

        [DataMember]
        public decimal Salario { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string FechaCreacion { get; set; }
    }
}