using System;
using System.Security.Cryptography;
using System.Text;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica
{
    public class LoginService
    {
        private readonly LoginRepositorio _loginRepositorio;

        public LoginService()
        {
            _loginRepositorio = new LoginRepositorio();
        }

        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse respuesta = new LoginResponse();

            if (request == null ||
                string.IsNullOrWhiteSpace(request.Usuario) ||
                string.IsNullOrWhiteSpace(request.Contrasena))
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "Usuario y/o contraseña incorrectos.";
                return respuesta;
            }

            UsuarioLoginDTO usuario =
                _loginRepositorio.ObtenerUsuarioPorNombre(
                    request.Usuario);

            if (usuario == null)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "Usuario y/o contraseña incorrectos.";
                return respuesta;
            }

            if (usuario.Estado.ToLower() != "activo")
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "El usuario se encuentra inactivo.";
                return respuesta;
            }

            if (usuario.Bloqueado)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "El usuario se encuentra bloqueado.";
                return respuesta;
            }

            string contrasena =
                EncriptarSHA256(request.Contrasena);

            if (contrasena != usuario.Contrasena)
            {
                usuario.IntentosFallidos++;

                _loginRepositorio.ActualizarIntentosFallidos(
                    usuario.IdUsuario,
                    usuario.IntentosFallidos);

                if (usuario.IntentosFallidos >= 3)
                {
                    _loginRepositorio.BloquearUsuario(
                        usuario.IdUsuario);

                    respuesta.Exito = false;
                    respuesta.Mensaje =
                        "Usuario bloqueado por exceder el número de intentos.";

                    return respuesta;
                }

                respuesta.Exito = false;
                respuesta.Mensaje =
                    "Usuario y/o contraseña incorrectos.";

                return respuesta;
            }

            _loginRepositorio.ReiniciarIntentos(
                usuario.IdUsuario);

            respuesta.Exito = true;
            respuesta.IdUsuario = usuario.IdUsuario;
            respuesta.Nombre = usuario.NombreCompleto;
            respuesta.Mensaje = "Inicio de sesión exitoso.";

            return respuesta;
        }

        private string EncriptarSHA256(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes =
                    sha256.ComputeHash(
                        Encoding.UTF8.GetBytes(contrasena));

                StringBuilder builder =
                    new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}