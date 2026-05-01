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
                        NumeroCuenta = "1001", 
                        Propietario = "Juan García", 
                        PIN = "1234", 
                        Saldo = 5000m,
                        Activa = true
                    },
                    new Cuenta 
                    { 
                        Id = 2, 
                        NumeroCuenta = "1002", 
                        Propietario = "María López", 
                        PIN = "5678", 
                        Saldo = 8500m,
                        Activa = true
                    },
                    new Cuenta 
                    { 
                        Id = 3, 
                        NumeroCuenta = "1003", 
                        Propietario = "Carlos Martínez", 
                        PIN = "9012", 
                        Saldo = 12000m,
                        Activa = true
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
