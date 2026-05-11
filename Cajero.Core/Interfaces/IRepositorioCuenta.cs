using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de cuentas.
    /// Define operaciones CRUD para cuentas bancarias.
    /// </summary>
    public interface IRepositorioCuenta
    {
        Cuenta? ObtenerPorId(int id);
        Cuenta? ObtenerPorNumero(string numeroCuenta);
        List<Cuenta> ObtenerTodas();
        void Guardar(Cuenta cuenta);
        void Actualizar(Cuenta cuenta);
        void Eliminar(int id);
    }
}
