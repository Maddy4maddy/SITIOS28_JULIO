using System;
using System.Collections.Generic;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica
{
    public class PuestoService
    {
        private readonly PuestoRepositorio _puestoRepositorio;

        public PuestoService()
        {
            _puestoRepositorio = new PuestoRepositorio();
        }

        public List<PuestoDTO> ObtenerPuestosActivos()
        {
            return _puestoRepositorio.ObtenerPuestosActivos();
        }
        public PuestoDTO ObtenerPuestoPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException(
                    "Debe indicar el código del puesto.");
            }

            return _puestoRepositorio
                .ObtenerPuestoPorCodigo(codigo);
        }
        public List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max)
        {
            if (min > max)
                throw new ArgumentException(
                    "El salario mínimo no puede ser mayor al máximo.");

            return _puestoRepositorio.ObtenerPuestosPorSalario(min, max);
        }
        public List<PuestoDTO> ObtenerTodosLosPuestos()
        {
            return _puestoRepositorio.ObtenerTodosLosPuestos();
        }
    }
}