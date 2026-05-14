using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio en memoria para gestionar cuentas.
    /// Implementa el patrón Repository para acceso a datos.
    /// </summary>
    public class RepositorioCuenta : IRepositorioCuenta
    {
        private static List<Cuenta> _cuentas = new();
        private static int _proximoId = 1;

        public RepositorioCuenta()
        {
            // Inicializar con datos de prueba
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            if (_cuentas.Count == 0)
            {
                _cuentas = new List<Cuenta>
                {
                    new Cuenta 
                    { 
                        Id = 1, 
                        NumeroCuenta = "412087654321", 
                        Propietario = "Juan García López", 
                        PIN = "8475", 
                        Saldo = 15750.50m,
                        TipoCuenta = TipoCuentaEnum.Corriente,
                        Activa = true,
                        FechaCreacion = new DateTime(2022, 3, 15)
                    },
                    new Cuenta 
                    { 
                        Id = 2, 
                        NumeroCuenta = "412087654322", 
                        Propietario = "María López Rodríguez", 
                        PIN = "5829", 
                        Saldo = 23400.75m,
                        TipoCuenta = TipoCuentaEnum.Ahorro,
                        Activa = true,
                        FechaCreacion = new DateTime(2021, 7, 22)
                    },
                    new Cuenta 
                    { 
                        Id = 3, 
                        NumeroCuenta = "412087654323", 
                        Propietario = "Carlos Martínez González", 
                        PIN = "9403", 
                        Saldo = 45600.00m,
                        TipoCuenta = TipoCuentaEnum.Corriente,
                        Activa = true,
                        FechaCreacion = new DateTime(2020, 11, 10)
                    }
                };
                _proximoId = 4;
            }
        }

        public Cuenta ObtenerPorNumeroCuenta(string numeroCuenta)
        {
            return _cuentas.FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
        }

        public Cuenta ObtenerPorId(int id)
        {
            return _cuentas.FirstOrDefault(c => c.Id == id);
        }

        public void Guardar(Cuenta cuenta)
        {
            if (cuenta.Id == 0)
            {
                cuenta.Id = _proximoId++;
            }
            _cuentas.Add(cuenta);
        }

        public void Actualizar(Cuenta cuenta)
        {
            var cuentaExistente = _cuentas.FirstOrDefault(c => c.Id == cuenta.Id);
            if (cuentaExistente != null)
            {
                cuentaExistente.Saldo = cuenta.Saldo;
                cuentaExistente.PIN = cuenta.PIN;
                cuentaExistente.Activa = cuenta.Activa;
            }
        }

        public IEnumerable<Cuenta> ObtenerTodas()
        {
            return _cuentas.ToList();
        }

        public bool Existe(string numeroCuenta)
        {
            return _cuentas.Any(c => c.NumeroCuenta == numeroCuenta);
        }
    }
}
