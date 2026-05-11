using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio principal del cajero.
    /// Define operaciones de negocio (autenticación, operaciones, etc).
    /// </summary>
    public interface IServicioCajero
    {
        // Autenticación
        ResultadoOperacion Autenticar(string numeroCuenta, string pin);

        // Consultas
        Cuenta? ObtenerCuenta(int cuentaId);
        ResultadoOperacion ConsultarSaldo(int cuentaId);
        ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId);
        ResultadoOperacion BuscarCuentaPorNumero(string numeroCuenta);

        // Operaciones
        ResultadoOperacion RealizarRetiro(int cuentaId, decimal monto);
        ResultadoOperacion RealizarDeposito(int cuentaId, decimal monto);
        ResultadoOperacion RealizarTransferencia(int cuentaIdOrigen, int cuentaIdDestino, decimal monto);

        // Configuración
        ResultadoOperacion ActualizarPIN(int cuentaId, string pinActual, string pinNuevo);
    }
}
