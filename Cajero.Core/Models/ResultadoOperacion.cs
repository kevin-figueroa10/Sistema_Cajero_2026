namespace Cajero.Core.Models
{
    /// <summary>
    /// Resultado estandarizado de cualquier operación del sistema.
    /// </summary>
    public class ResultadoOperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public object Datos { get; set; }
        public string Codigo { get; set; } // Código de error/éxito

        public ResultadoOperacion()
        {
        }

        public ResultadoOperacion(bool exitoso, string mensaje, string codigo = null, object datos = null)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            Codigo = codigo ?? (exitoso ? "SUCCESS" : "ERROR");
            Datos = datos;
        }

        public static ResultadoOperacion Exito(string mensaje, object datos = null)
        {
            return new ResultadoOperacion(true, mensaje, "SUCCESS", datos);
        }

        public static ResultadoOperacion Error(string mensaje, string codigo = "ERROR")
        {
            return new ResultadoOperacion(false, mensaje, codigo);
        }
    }
}
