using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using PROYECTO2_WEBService.Logica.Services;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService
{
    public class WEBServiceCORE1 : IWEBServiceCORE1
    {
        private readonly PuestoService _puestoService;

        public WEBServiceCORE1()
        {
            _puestoService = new PuestoService();
        }

        // Método existente
        public PuestoDTO ObtenerPuestoPorCodigo(string codigo)
        {
            try
            {
                return _puestoService.ObtenerPuestoPorCodigo(codigo);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener el puesto por código: {ex.Message}");
            }
        }

        // Método existente
        public List<PuestoDTO> ObtenerPuestosActivos()
        {
            try
            {
                return _puestoService.ObtenerPuestosActivos();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener puestos activos: {ex.Message}");
            }
        }

        // Obtener todos los puestos
        public List<PuestoDTO> ObtenerTodosLosPuestos()
        {
            try
            {
                return _puestoService.ObtenerTodosLosPuestos();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener todos los puestos: {ex.Message}");
            }
        }

        //  Obtener puestos por rango de salario
        public List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max)
        {
            try
            {
                return _puestoService.ObtenerPuestosPorSalario(min, max);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error al obtener puestos por salario: {ex.Message}");
            }
        }

        //  GetData
        public string GetData(int value)
        {
            return $"Has ingresado el valor: {value}";
        }

        //  GetDataUsingDataContract
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }

            if (composite.BoolValue)
            {
                composite.StringValue += " (modificado)";
            }

            return composite;
        }

        // Método Options existente
        public void Options()
        {
            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
        }
    }
}