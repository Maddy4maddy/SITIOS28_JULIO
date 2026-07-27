using PROYECTO2_WEBService.Logica;
using PROYECTO2_WEBService.Modelo;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PROYECTO2_WEBService
{
    public class WEBSERVICEcore4 : IWEBSERVICEcore4
    {
        private void ConfigurarCors()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add(
                "Access-Control-Allow-Origin",
                "http://localhost:8000");

            WebOperationContext.Current.OutgoingResponse.Headers.Add(
                "Access-Control-Allow-Methods",
                "POST, OPTIONS");

            WebOperationContext.Current.OutgoingResponse.Headers.Add(
                "Access-Control-Allow-Headers",
                "Content-Type");
        }


        // ========================================
        // LOGIN
        // ========================================

        private readonly LoginService _loginService = new LoginService();

        public LoginResponse Login(LoginRequest request)
        {
            ConfigurarCors();

            try
            {
                return _loginService.Login(request);
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Exito = false,
                    Mensaje = ex.Message
                };
            }
        }

        public void Options()
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode =
                System.Net.HttpStatusCode.OK;

            ConfigurarCors();
        }

        // ========================================
        // MÉTODOS BASE DEL WCF
        // ========================================

        public string GetData(int value)
        {
            return string.Format(
                "You entered: {0}",
                value);
        }


        public CompositeType GetDataUsingDataContract(
            CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }


            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }


            return composite;
        }


    }
}