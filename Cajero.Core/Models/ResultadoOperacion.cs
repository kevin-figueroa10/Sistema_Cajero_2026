namespace Cajero.Core.Models
{
    /// <summary>
    /// Clase genérica que representa el resultado de cualquier operación.
    /// Contiene información sobre éxito, mensaje y datos retornados.
    /// </summary>
    public class ResultadoOperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public object? Datos { get; set; }

        public static ResultadoOperacion Éxito(string mensaje, object? datos = null)
        {
            return new ResultadoOperacion { Exitoso = true, Mensaje = mensaje, Datos = datos };
        }

        public static ResultadoOperacion Error(string mensaje)
        {
            return new ResultadoOperacion { Exitoso = false, Mensaje = mensaje };
        }
    }
}
