namespace Cajero.Core.Models
{
    /// <summary>
    /// Respuesta que incluye el comprobante de la operación realizada.
    /// </summary>
    public class RespuestaOperacionConComprobante
    {
        public Comprobante Comprobante { get; set; } = new Comprobante();
        public decimal NuevoSaldo { get; set; }
    }
}
