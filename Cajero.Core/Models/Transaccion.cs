namespace Cajero.Core.Models
{
    /// <summary>
    /// Registra todas las transacciones realizadas en el sistema.
    /// </summary>
    public class Transaccion
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public string Tipo { get; set; } // Retiro, Depósito, Transferencia
        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int? CuentaDestinoId { get; set; } // Para transferencias

        public Transaccion()
        {
            Fecha = DateTime.Now;
        }
    }
}
