using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cajero.Web.Controllers
{
    /// <summary>
    /// Controlador para autenticación de usuarios.
    /// Maneja login y logout del sistema.
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
        /// Muestra la página de login.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("CuentaId") != null)
            {
                return RedirectToAction("Index", "Principal");
            }

            return View();
        }

        /// <summary>
        /// Procesa el login del usuario.
        /// </summary>
        [HttpPost]
        public IActionResult Login(string numeroCuenta, string pin)
        {
            _logger.LogInformation($"Intento de login con cuenta: {numeroCuenta}");

            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            if (resultado.Exitoso)
            {
                var datos = (RespuestaAutenticacion)resultado.Datos;

                // Obtener información adicional de la cuenta
                var cuenta = _servicioCajero.ObtenerCuenta(datos.CuentaId);

                HttpContext.Session.SetInt32("CuentaId", datos.CuentaId);
                HttpContext.Session.SetString("Propietario", datos.Propietario);
                HttpContext.Session.SetString("NumeroCuenta", numeroCuenta);
                HttpContext.Session.SetString("TipoCuenta", cuenta.TipoCuenta.ToString());
                HttpContext.Session.SetString("Saldo", cuenta.Saldo.ToString("F2"));
                HttpContext.Session.SetString("FechaCreacion", cuenta.FechaCreacion.ToString("dd/MM/yyyy"));

                _logger.LogInformation($"Login exitoso para: {numeroCuenta}");
                return RedirectToAction("Index", "Principal");
            }

            _logger.LogWarning($"Login fallido: {resultado.Mensaje}");
            TempData["Error"] = resultado.Mensaje;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Cierra la sesión del usuario.
        /// </summary>
        [HttpPost]
        public IActionResult Logout()
        {
            var propietario = HttpContext.Session.GetString("Propietario");
            _logger.LogInformation($"Logout de: {propietario}");

            HttpContext.Session.Clear();
            TempData["Mensaje"] = "Sesión cerrada correctamente.";
            return RedirectToAction("Index");
        }
    }
}
