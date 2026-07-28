using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Logica.Services;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    public class WEBServiceCORE7 : IWEBServiceCORE7
    {
        private readonly OferenteService _oferenteService = new OferenteService();

        public List<OferenteResumenDTO> ObtenerOferentesPorPuesto(string codigoPuesto)
        {
            try
            {
                return _oferenteService.ObtenerOferentesPorPuesto(codigoPuesto);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener oferentes por puesto: {ex.Message}");
            }
        }

        public OferenteDetalleDTO ObtenerDetalleOferente(string idPostulacion)
        {
            try
            {
                return _oferenteService.ObtenerDetalleOferente(idPostulacion);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener detalle del oferente: {ex.Message}");
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