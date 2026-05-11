namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa una transacción bancaria.
    /// </summary>
    public class Transaccion
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public string Tipo { get; set; } = string.Empty; // Retiro, Depósito, Transferencia
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaHora { get; set; }
        public string NumeroReferencia { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
