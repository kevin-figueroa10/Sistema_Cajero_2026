using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio principal del cajero.
    /// Orquesta todas las operaciones del sistema.
    /// </summary>
    public interface IServicioCajero
    {
        ResultadoOperacion Autenticar(string numeroCuenta, string pin);
        ResultadoOperacion ConsultarSaldo(int cuentaId);
        ResultadoOperacion RealizarRetiro(int cuentaId, decimal monto);
        ResultadoOperacion RealizarDeposito(int cuentaId, decimal monto);
        ResultadoOperacion RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto);
        ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId);
    }
}
