namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa una cuenta bancaria en el sistema.
    /// </summary>
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string Propietario { get; set; }
        public string PIN { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activa { get; set; }

        // Nuevas propiedades para funcionalidades avanzadas
        public TipoCuentaEnum TipoCuenta { get; set; } = TipoCuentaEnum.Ahorro;
        public decimal RetirosDia { get; set; }
        public DateTime? UltimaTransferencia { get; set; }
        public int TransferenciasHoy { get; set; }

        public Cuenta()
        {
            FechaCreacion = DateTime.Now;
            Activa = true;
            TipoCuenta = TipoCuentaEnum.Ahorro;
            RetirosDia = 0m;
            TransferenciasHoy = 0;
        }
    }
}
