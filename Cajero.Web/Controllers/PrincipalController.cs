using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cajero.Web.Controllers
{
    /// <summary>
    /// Controlador para las operaciones principales del cajero.
    /// Maneja saldos, retiros, depósitos y transferencias.
    /// </summary>
    public class PrincipalController : Controller
    {
        private readonly IServicioCajero _servicioCajero;
        private readonly ILogger<PrincipalController> _logger;

        public PrincipalController(IServicioCajero servicioCajero, ILogger<PrincipalController> logger)
        {
            _servicioCajero = servicioCajero;
            _logger = logger;
        }

        private int? ObtenerCuentaId()
        {
            return HttpContext.Session.GetInt32("CuentaId");
        }

        private bool ValidarSesion()
        {
            if (ObtenerCuentaId() == null)
            {
                TempData["Error"] = "Debe iniciar sesión.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Muestra el menú principal.
        /// </summary>
        public IActionResult Index()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Consulta el saldo de la cuenta.
        /// </summary>
        public IActionResult ConsultarSaldo()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.ConsultarSaldo(cuentaId);

            if (resultado.Exitoso)
            {
                var datos = (RespuestaSaldo)resultado.Datos;
                ViewData["Saldo"] = datos;
            }
            else
            {
                TempData["Error"] = resultado.Mensaje;
            }

            return View();
        }

        /// <summary>
        /// Muestra el formulario de retiro.
        /// </summary>
        [HttpGet]
        public IActionResult Retiro()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa un retiro.
        /// </summary>
        [HttpPost]
        public IActionResult Retiro(decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            _logger.LogInformation($"Retiro solicitado: ${monto}");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.RealizarRetiro(cuentaId, monto);

            if (resultado.Exitoso)
            {
                var datosRetiro = (RespuestaOperacionConComprobante)resultado.Datos;
                TempData["Exito"] = resultado.Mensaje;
                TempData["Comprobante"] = System.Text.Json.JsonSerializer.Serialize(datosRetiro.Comprobante);
                _logger.LogInformation($"Retiro exitoso: ${monto}");
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Retiro fallido: {resultado.Mensaje}");
            return RedirectToAction("Retiro");
        }

        /// <summary>
        /// Muestra el formulario de depósito.
        /// </summary>
        [HttpGet]
        public IActionResult Deposito()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa un depósito.
        /// </summary>
        [HttpPost]
        public IActionResult Deposito(decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            _logger.LogInformation($"Depósito solicitado: ${monto}");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.RealizarDeposito(cuentaId, monto);

            if (resultado.Exitoso)
            {
                var datosDeposito = (RespuestaOperacionConComprobante)resultado.Datos;
                TempData["Exito"] = resultado.Mensaje;
                TempData["Comprobante"] = System.Text.Json.JsonSerializer.Serialize(datosDeposito.Comprobante);
                _logger.LogInformation($"Depósito exitoso: ${monto}");
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Depósito fallido: {resultado.Mensaje}");
            return RedirectToAction("Deposito");
        }

        /// <summary>
        /// Muestra el formulario de transferencia.
        /// </summary>
        [HttpGet]
        public IActionResult Transferencia()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa una transferencia.
        /// </summary>
        [HttpPost]
        public IActionResult Transferencia(int cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            _logger.LogInformation($"Transferencia solicitada a cuenta: {cuentaDestino}, monto: ${monto}");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.RealizarTransferencia(cuentaId, cuentaDestino, monto);

            if (resultado.Exitoso)
            {
                var datosTransferencia = (RespuestaOperacionConComprobante)resultado.Datos;
                TempData["Exito"] = resultado.Mensaje;
                TempData["Comprobante"] = System.Text.Json.JsonSerializer.Serialize(datosTransferencia.Comprobante);
                _logger.LogInformation($"Transferencia exitosa: ${monto}");
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Transferencia fallida: {resultado.Mensaje}");
            return RedirectToAction("Transferencia");
        }

        /// <summary>
        /// Muestra el historial de transacciones.
        /// </summary>
        public IActionResult Historial()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.ObtenerHistorialTransacciones(cuentaId);

            if (resultado.Exitoso)
            {
                ViewData["Transacciones"] = resultado.Datos;
            }
            else
            {
                TempData["Error"] = resultado.Mensaje;
            }

            return View();
        }

        /// <summary>
        /// Muestra el comprobante de la operación realizada.
        /// </summary>
        public IActionResult Comprobante()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var comprobanteJson = TempData["Comprobante"] as string;
            if (string.IsNullOrEmpty(comprobanteJson))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var comprobante = System.Text.Json.JsonSerializer.Deserialize<Comprobante>(comprobanteJson);
                return View(comprobante);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
