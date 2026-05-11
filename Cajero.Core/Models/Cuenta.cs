namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa una cuenta bancaria.
    /// </summary>
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public string PIN { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public List<int> TransaccionIds { get; set; } = new List<int>();
    }
}
