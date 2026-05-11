using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio de transacciones con almacenamiento en memoria.
    /// Simula una base de datos para propósitos de demostración.
    /// </summary>
    public class RepositorioTransaccion : IRepositorioTransaccion
    {
        private static List<Transaccion> _transacciones = new List<Transaccion>();
        private static int _proximoId = 1;

        public Transaccion? ObtenerPorId(int id)
        {
            return _transacciones.FirstOrDefault(t => t.Id == id);
        }

        public List<Transaccion> ObtenerPorCuenta(int cuentaId)
        {
            return _transacciones
                .Where(t => t.CuentaId == cuentaId)
                .OrderByDescending(t => t.FechaHora)
                .ToList();
        }

        public List<Transaccion> ObtenerTodas()
        {
            return _transacciones.ToList();
        }

        public void Guardar(Transaccion transaccion)
        {
            transaccion.Id = _proximoId++;
            _transacciones.Add(transaccion);
        }

        public void Actualizar(Transaccion transaccion)
        {
            var transaccionExistente = _transacciones.FirstOrDefault(t => t.Id == transaccion.Id);
            if (transaccionExistente != null)
            {
                transaccionExistente.Tipo = transaccion.Tipo;
                transaccionExistente.Monto = transaccion.Monto;
                transaccionExistente.Comision = transaccion.Comision;
                transaccionExistente.MontoTotal = transaccion.MontoTotal;
                transaccionExistente.Descripcion = transaccion.Descripcion;
            }
        }

        public void Eliminar(int id)
        {
            var transaccion = _transacciones.FirstOrDefault(t => t.Id == id);
            if (transaccion != null)
            {
                _transacciones.Remove(transaccion);
            }
        }
    }
}
