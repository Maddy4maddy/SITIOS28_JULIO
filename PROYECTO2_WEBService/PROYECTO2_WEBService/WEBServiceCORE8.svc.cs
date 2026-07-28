using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Logica.Services;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    public class WEBServiceCORE8 : IWEBServiceCORE8
    {
        private readonly OferenteCORE8Service _oferenteService = new OferenteCORE8Service();

        public OferenteCORE8DTO ObtenerOferente(string codigo)
        {
            try
            {
                return _oferenteService.ObtenerOferente(codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener el oferente: {ex.Message}");
            }
        }

        public void Options()
        {
            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
        }
    }
}