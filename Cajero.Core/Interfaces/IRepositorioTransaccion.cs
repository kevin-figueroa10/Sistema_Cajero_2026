using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de transacciones.
    /// Define las operaciones para registrar y consultar transacciones.
    /// </summary>
    public interface IRepositorioTransaccion
    {
        void Registrar(Transaccion transaccion);
        IEnumerable<Transaccion> ObtenerPorCuenta(int cuentaId);
        IEnumerable<Transaccion> ObtenerTodas();
    }
}
