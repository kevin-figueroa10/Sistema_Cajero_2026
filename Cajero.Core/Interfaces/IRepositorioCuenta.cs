using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de cuentas.
    /// Define las operaciones CRUD para cuentas.
    /// </summary>
    public interface IRepositorioCuenta
    {
        Cuenta ObtenerPorNumeroCuenta(string numeroCuenta);
        Cuenta ObtenerPorId(int id);
        void Guardar(Cuenta cuenta);
        void Actualizar(Cuenta cuenta);
        IEnumerable<Cuenta> ObtenerTodas();
        bool Existe(string numeroCuenta);
    }
}
