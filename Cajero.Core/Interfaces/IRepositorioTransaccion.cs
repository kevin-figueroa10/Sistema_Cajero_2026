using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de transacciones.
    /// Define operaciones CRUD para transacciones.
    /// </summary>
    public interface IRepositorioTransaccion
    {
        Transaccion? ObtenerPorId(int id);
        List<Transaccion> ObtenerPorCuenta(int cuentaId);
        List<Transaccion> ObtenerTodas();
        void Guardar(Transaccion transaccion);
        void Actualizar(Transaccion transaccion);
        void Eliminar(int id);
    }
}
