using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Modelo;
using PROYECTO2_WEBService.Logica;

namespace PROYECTO2_WEBService
{

    public class WEBServiceCORE1 : IService1
    {
        private readonly PuestoService _puestoService =
            new PuestoService();
        // ========================================
        // CONFIGURAR CORS
        // ========================================

        private void ConfigurarCors()
        {

            WebOperationContext.Current
            .OutgoingResponse
            .Headers.Add(
                "Access-Control-Allow-Origin",
                "http://localhost:8000"
            );


            WebOperationContext.Current
            .OutgoingResponse
            .Headers.Add(
                "Access-Control-Allow-Methods",
                "GET, OPTIONS"
            );


            WebOperationContext.Current
            .OutgoingResponse
            .Headers.Add(
                "Access-Control-Allow-Headers",
                "Content-Type"
            );

        }


        // ========================================
        // PUESTOS ACTIVOS
        // ========================================

        public List<PuestoDTO> ObtenerPuestosActivos()
        {
            ConfigurarCors();

            try
            {
                return _puestoService.ObtenerPuestosActivos();
            }
            catch (Exception ex)
            {
                throw new FaultException(
                    $"Error al obtener puestos activos: {ex.Message}");
            }
        }


        // ========================================
        // PUESTO POR CODIGO
        // ========================================

        public PuestoDTO ObtenerPuestoPorCodigo(string codigo)
        {
            ConfigurarCors();

            try
            {
                return _puestoService
                    .ObtenerPuestoPorCodigo(codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException(
                    $"Error al obtener puesto: {ex.Message}");
            }
        }




        // ========================================
        // POR SALARIO
        // ========================================

        public List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max)
        {
            ConfigurarCors();

            try
            {
                return _puestoService.ObtenerPuestosPorSalario(min, max);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }




        // ========================================
        // TODOS LOS PUESTOS
        // ========================================

        public List<PuestoDTO> ObtenerTodosLosPuestos()
        {
            ConfigurarCors();

            try
            {
                return _puestoService.ObtenerTodosLosPuestos();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }




        // ========================================
        // OPTIONS CORS
        // ========================================

        public void Options()
        {

            WebOperationContext.Current
            .OutgoingResponse
            .StatusCode =
            System.Net.HttpStatusCode.OK;


            ConfigurarCors();

        }




        // ========================================
        // MÉTODOS ORIGINALES
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
                throw new ArgumentNullException(
                    "composite");
            }


            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }


            return composite;

        }

    }
}