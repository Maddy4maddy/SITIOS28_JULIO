using System;
using System.Collections.Generic;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica.Services
{
    public class OferenteService
    {
        private readonly OferenteRepositorio _oferenteRepositorio;

        public OferenteService()
        {
            _oferenteRepositorio = new OferenteRepositorio();
        }

        public List<OferenteResumenDTO> ObtenerOferentesPorPuesto(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new ArgumentException("El código del puesto es requerido");

            int idPuesto = _oferenteRepositorio.ObtenerIdPuestoPorCodigo(codigoPuesto);
            return _oferenteRepositorio.ObtenerOferentesPorIdPuesto(idPuesto);
        }

        public OferenteDetalleDTO ObtenerDetalleOferente(string idPostulacion)
        {
            if (string.IsNullOrWhiteSpace(idPostulacion))
                throw new ArgumentException("El ID de postulación es requerido");

            int id = Convert.ToInt32(idPostulacion);
            return _oferenteRepositorio.ObtenerDetalleOferente(id);
        }
    }
}