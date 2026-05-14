namespace Cajero.Core.Models
{
    /// <summary>
    /// Tipos de cuenta disponibles en el sistema
    /// </summary>
    public enum TipoCuentaEnum
    {
        Ahorro,
        Corriente,
        Plazo
    }

    /// <summary>
    /// Modelo de comprobante de transacción
    /// </summary>
    public class Comprobante
    {
        public string NumeroReferencia { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string TipoOperacion { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public string NumeroCuenta { get; set; }
        public string Titular { get; set; }
        public string Descripcion { get; set; }
        public string CuentaDestino { get; set; }
        public decimal Comision { get; set; }
        public string Estado { get; set; } = "EXITOSO";
    }

    /// <summary>
    /// Modelo para respuesta de operación con comprobante
    /// </summary>
    public class RespuestaOperacionConComprobante
    {
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public decimal Monto { get; set; }
        public Comprobante Comprobante { get; set; }
        public string CuentaDestino { get; set; }
        public decimal Comision { get; set; }
    }

    /// <summary>
    /// Modelo para filtrado de historial
    /// </summary>
    public class FiltroHistorial
    {
        public string TipoOperacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? MontoMinimo { get; set; }
        public decimal? MontoMaximo { get; set; }
        public int Pagina { get; set; } = 1;
        public int ResultadosPorPagina { get; set; } = 10;
    }

    /// <summary>
    /// Modelo para respuesta paginada de historial
    /// </summary>
    public class RespuestaHistorialPaginado
    {
        public List<Transaccion> Transacciones { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaActual { get; set; }
    }

    /// <summary>
    /// Configuración de límites por tipo de cuenta
    /// </summary>
    public class ConfiguracionCuenta
    {
        public TipoCuentaEnum Tipo { get; set; }
        public decimal LimiteDiarioRetiro { get; set; }
        public decimal LimitePorTransaccion { get; set; }
        public List<decimal> MultiplosPermitidos { get; set; }
        public int MaximoTransferenciasDialas { get; set; }
        public decimal ComisionTransferencia { get; set; }

        public static ConfiguracionCuenta ObtenerConfiguracion(TipoCuentaEnum tipo)
        {
            return tipo switch
            {
                TipoCuentaEnum.Ahorro => new ConfiguracionCuenta
                {
                    Tipo = TipoCuentaEnum.Ahorro,
                    LimiteDiarioRetiro = 3000m,
                    LimitePorTransaccion = 1000m,
                    MultiplosPermitidos = new List<decimal> { 5, 10, 20, 50, 100 },
                    MaximoTransferenciasDialas = 3,
                    ComisionTransferencia = 0m
                },
                TipoCuentaEnum.Corriente => new ConfiguracionCuenta
                {
                    Tipo = TipoCuentaEnum.Corriente,
                    LimiteDiarioRetiro = 3000m,
                    LimitePorTransaccion = 1000m,
                    MultiplosPermitidos = new List<decimal> { 5, 10, 20, 50, 100, 200 },
                    MaximoTransferenciasDialas = 10,
                    ComisionTransferencia = 1.5m
                },
                TipoCuentaEnum.Plazo => new ConfiguracionCuenta
                {
                    Tipo = TipoCuentaEnum.Plazo,
                    LimiteDiarioRetiro = 0m,
                    LimitePorTransaccion = 0m,
                    MultiplosPermitidos = new List<decimal>(),
                    MaximoTransferenciasDialas = 0,
                    ComisionTransferencia = 0m
                },
                _ => throw new ArgumentException("Tipo de cuenta no válido")
            };
        }
    }
}
