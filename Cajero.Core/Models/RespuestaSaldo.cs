namespace Cajero.Core.Models
{
    /// <summary>
    /// Respuesta de consulta de saldo.
    /// </summary>
    public class RespuestaSaldo
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
