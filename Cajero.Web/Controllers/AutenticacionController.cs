using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cajero.Web.Controllers
{
    /// <summary>
    /// Controlador para autenticación de usuarios.
    /// Maneja login, logout y gestión de sesiones.
    /// </summary>
    public class AutenticacionController : Controller
    {
        private readonly IServicioCajero _servicioCajero;
        private readonly ILogger<AutenticacionController> _logger;

        public AutenticacionController(IServicioCajero servicioCajero, ILogger<AutenticacionController> logger)
        {
            _servicioCajero = servicioCajero;
            _logger = logger;
        }

        /// <summary>
        /// Muestra la pantalla de login.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            // Si ya está autenticado, redirige al menú
            if (HttpContext.Session.GetInt32("CuentaId") != null)
            {
                return RedirectToAction("Index", "Principal");
            }

            return View();
        }

        /// <summary>
        /// Procesa el login del usuario.
        /// Valida número de cuenta y PIN, crea la sesión.
        /// </summary>
        [HttpPost]
        public IActionResult Index(string numeroCuenta, string pin)
        {
            // Validar entrada
            if (string.IsNullOrWhiteSpace(numeroCuenta) || string.IsNullOrWhiteSpace(pin))
            {
                ViewData["Error"] = "El número de cuenta y PIN son requeridos.";
                _logger.LogWarning("Intento de login sin proporcionar datos.");
                return View();
            }

            // Intentar autenticar
            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            if (resultado.Exitoso)
            {
                // Obtener respuesta y datos de la cuenta
                var respuesta = (RespuestaAutenticacion)resultado.Datos;
                var cuenta = _servicioCajero.ObtenerCuenta(respuesta.CuentaId);

                // Guardar datos en sesión
                HttpContext.Session.SetInt32("CuentaId", cuenta.Id);
                HttpContext.Session.SetString("Propietario", cuenta.Propietario);
                HttpContext.Session.SetString("NumeroCuenta", cuenta.NumeroCuenta);
                HttpContext.Session.SetString("TipoCuenta", cuenta.TipoCuenta.ToString());
                HttpContext.Session.SetString("Saldo", cuenta.Saldo.ToString("F2"));
                HttpContext.Session.SetString("FechaCreacion", cuenta.FechaCreacion.ToString("dd/MM/yyyy"));

                _logger.LogInformation($"Usuario autenticado exitosamente: {cuenta.Propietario}");
                return RedirectToAction("Index", "Principal");
            }

            // Login fallido
            ViewData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Intento de login fallido: {resultado.Mensaje}");
            return View();
        }

        /// <summary>
        /// Cierra la sesión del usuario.
        /// Limpia todos los datos de sesión.
        /// </summary>
        public IActionResult Logout()
        {
            var propietario = HttpContext.Session.GetString("Propietario");
            HttpContext.Session.Clear();

            _logger.LogInformation($"Usuario cerró sesión: {propietario}");
            return RedirectToAction("Index");
        }
    }
}