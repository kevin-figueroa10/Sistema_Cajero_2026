namespace Cajero.Core.Models
{
    /// <summary>
    /// Respuesta del proceso de autenticación.
    /// </summary>
    public class RespuestaAutenticacion
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public TipoCuenta TipoCuenta { get; set; }
    }
}
