namespace Cajero.Core.Models
{
    /// <summary>
    /// Modelo para la respuesta de autenticación.
    /// </summary>
    public class RespuestaAutenticacion
    {
        public int CuentaId { get; set; }
        public string Propietario { get; set; }
    }

    /// <summary>
    /// Modelo para la consulta de saldo.
    /// </summary>
    public class RespuestaSaldo
    {
        public decimal Saldo { get; set; }
        public string NumeroCuenta { get; set; }
        public string Propietario { get; set; }
    }

    /// <summary>
    /// Modelo para operaciones de retiro y depósito.
    /// </summary>
    public class RespuestaOperacionMonetaria
    {
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public decimal Monto { get; set; }
    }

    /// <summary>
    /// Modelo para respuesta de transferencia.
    /// </summary>
    public class RespuestaTransferencia
    {
        public decimal SaldoOrigen { get; set; }
        public decimal SaldoDestino { get; set; }
        public decimal Monto { get; set; }
        public string CuentaDestino { get; set; }
    }
}
