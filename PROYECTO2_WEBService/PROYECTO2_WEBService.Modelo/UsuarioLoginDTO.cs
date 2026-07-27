namespace PROYECTO2_WEBService.Modelo
{
    public class UsuarioLoginDTO
    {
        public int IdUsuario { get; set; }

        public string NombreCompleto { get; set; }

        public string Contrasena { get; set; }

        public int IntentosFallidos { get; set; }

        public bool Bloqueado { get; set; }

        public string Estado { get; set; }
    }
}