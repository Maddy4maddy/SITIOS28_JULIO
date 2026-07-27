using System.Runtime.Serialization;

namespace PROYECTO2_WEBService.Modelo
{
    [DataContract]
    public class CrearEmpleadoRequest
    {
        [DataMember]
        public string NumeroEmpleado { get; set; }

        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public string TipoIdentificacion { get; set; }

        [DataMember]
        public string NombreCompleto { get; set; }

        [DataMember]
        public string FechaNacimiento { get; set; }

        [DataMember]
        public string Correo { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public int IdPuesto { get; set; }

        [DataMember]
        public string FechaContratacion { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}