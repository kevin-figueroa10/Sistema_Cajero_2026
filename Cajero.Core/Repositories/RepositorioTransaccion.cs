using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio en memoria para gestionar transacciones.
    /// Implementa el patrón Repository para acceso a datos.
    /// </summary>
    public class RepositorioTransaccion : IRepositorioTransaccion
    {
        private static List<Transaccion> _transacciones = new();
        private static int _proximoId = 1;

        public void Registrar(Transaccion transaccion)
        {
            transaccion.Id = _proximoId++;
            _transacciones.Add(transaccion);
        }

        public IEnumerable<Transaccion> ObtenerPorCuenta(int cuentaId)
        {
            return _transacciones
                .Where(t => t.CuentaId == cuentaId)
                .OrderByDescending(t => t.Fecha)
                .ToList();
        }

        public IEnumerable<Transaccion> ObtenerTodas()
        {
            return _transacciones.OrderByDescending(t => t.Fecha).ToList();
        }
    }
}
