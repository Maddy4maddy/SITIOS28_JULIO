using System;
using PROYECTO2_WEBService.AccesoDatos.Repositorios;
using PROYECTO2_WEBService.Modelo;

namespace PROYECTO2_WEBService.Logica.Services
{
    public class OferenteCORE8Service
    {
        private readonly OferenteCORE8Repositorio _oferenteRepositorio;

        public OferenteCORE8Service()
        {
            _oferenteRepositorio = new OferenteCORE8Repositorio();
        }

        public OferenteCORE8DTO ObtenerOferente(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código del oferente es requerido");

            return _oferenteRepositorio.ObtenerOferentePorCodigo(codigo);
        }
    }
}