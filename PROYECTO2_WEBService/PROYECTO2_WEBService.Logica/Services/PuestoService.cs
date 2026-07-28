using System;
using System.Collections.Generic;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica.Services
{
    public class PuestoService
    {
        private readonly PuestoRepositorio _puestoRepositorio;

        public PuestoService()
        {
            _puestoRepositorio = new PuestoRepositorio();
        }

        public PuestoDTO ObtenerPuestoPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código del puesto es requerido");

            return _puestoRepositorio.ObtenerPuestoPorCodigo(codigo);
        }

        public List<PuestoDTO> ObtenerPuestosActivos()
        {
            return _puestoRepositorio.ObtenerPuestosActivos();
        }

        //  Obtener todos los puestos
        public List<PuestoDTO> ObtenerTodosLosPuestos()
        {
            return _puestoRepositorio.ObtenerTodosLosPuestos();
        }

        //  Obtener puestos por rango de salario
        public List<PuestoDTO> ObtenerPuestosPorSalario(decimal min, decimal max)
        {
            if (min < 0)
                throw new ArgumentException("El salario mínimo no puede ser negativo");

            if (max < min)
                throw new ArgumentException("El salario máximo debe ser mayor o igual al mínimo");

            return _puestoRepositorio.ObtenerPuestosPorSalario(min, max);
        }
    }
}