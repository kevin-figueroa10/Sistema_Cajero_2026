namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa el comprobante de una transacción.
    /// </summary>
    public class Comprobante
    {
        public string NumeroReferencia { get; set; } = string.Empty;
        public string TipoOperacion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaHora { get; set; }
        public string CuentaOrigen { get; set; } = string.Empty;
        public string? CuentaDestino { get; set; }
        public string Titular { get; set; } = string.Empty;
    }
}
