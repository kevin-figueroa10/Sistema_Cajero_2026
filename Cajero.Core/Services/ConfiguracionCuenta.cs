using Cajero.Core.Models;

namespace Cajero.Core.Services
{
    /// <summary>
    /// Configuración de límites, comisiones y reglas por tipo de cuenta.
    /// </summary>
    public static class ConfiguracionCuenta
    {
        /// <summary>
        /// Obtiene la comisión por operación según el tipo de cuenta.
        /// </summary>
        public static decimal ObtenerComision(TipoCuenta tipo, string tipoOperacion)
        {
            return (tipo, tipoOperacion) switch
            {
                // Ahorros: 1.5% retiro, 0% depósito, 1.5% transferencia
                (TipoCuenta.Ahorros, "Retiro") => 0.015m,
                (TipoCuenta.Ahorros, "Deposito") => 0m,
                (TipoCuenta.Ahorros, "Transferencia") => 0.015m,

                // Corriente: 1% retiro, 0% depósito, 1% transferencia
                (TipoCuenta.Corriente, "Retiro") => 0.01m,
                (TipoCuenta.Corriente, "Deposito") => 0m,
                (TipoCuenta.Corriente, "Transferencia") => 0.01m,

                // Nómina: 0.5% retiro, 0% depósito, 0.5% transferencia
                (TipoCuenta.Nómina, "Retiro") => 0.005m,
                (TipoCuenta.Nómina, "Deposito") => 0m,
                (TipoCuenta.Nómina, "Transferencia") => 0.005m,

                _ => 0m
            };
        }

        /// <summary>
        /// Obtiene el límite máximo de retiro según el tipo de cuenta.
        /// </summary>
        public static decimal ObtenerLimiteRetiro(TipoCuenta tipo)
        {
            return tipo switch
            {
                TipoCuenta.Ahorros => 2000m,
                TipoCuenta.Corriente => 5000m,
                TipoCuenta.Nómina => 10000m,
                _ => 1000m
            };
        }

        /// <summary>
        /// Obtiene el límite máximo de transferencia según el tipo de cuenta.
        /// </summary>
        public static decimal ObtenerLimiteTransferencia(TipoCuenta tipo)
        {
            return tipo switch
            {
                TipoCuenta.Ahorros => 5000m,
                TipoCuenta.Corriente => 20000m,
                TipoCuenta.Nómina => 50000m,
                _ => 1000m
            };
        }
    }
}
