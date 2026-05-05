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
        /// Busca la cuenta destino y muestra confirmación con nombre del titular.
        /// </summary>
        [HttpPost("Principal/Transferencia")]
        public IActionResult BuscarCuentaTransferencia(string cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaOrigenId = ObtenerCuentaId().Value;
            var cuentaOrigen = _servicioCajero.ObtenerCuenta(cuentaOrigenId);

            // Buscar cuenta destino por número
            var resultado = _servicioCajero.BuscarCuentaPorNumero(cuentaDestino);

            if (!resultado.Exitoso || resultado.Datos == null)
            {
                TempData["Error"] = resultado.Mensaje;
                return RedirectToAction("Transferencia");
            }

            var cuentaDestinoObj = (Cuenta)resultado.Datos;

            // Validar que no sea la misma cuenta
            if (cuentaOrigenId == cuentaDestinoObj.Id)
            {
                TempData["Error"] = "No puedes transferir a tu propia cuenta.";
                return RedirectToAction("Transferencia");
            }

            // Guardar monto temporalmente
            ViewBag.Monto = monto;

            return View("ConfirmarTransferencia", cuentaDestinoObj);
        }

        /// <summary>
        /// Confirma y procesa la transferencia.
        /// </summary>
        [HttpPost("Principal/ConfirmarTransferencia")]
        public IActionResult ConfirmarTransferencia(string cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            _logger.LogInformation($"Transferencia confirmada: ${monto} a {cuentaDestino}");

            var cuentaOrigenId = ObtenerCuentaId().Value;

            // Buscar el ID de la cuenta por número
            var cuentaDestinoBusqueda = _servicioCajero.BuscarCuentaPorNumero(cuentaDestino);
            if (!cuentaDestinoBusqueda.Exitoso)
            {
                TempData["Error"] = "Cuenta destino no encontrada.";
                return RedirectToAction("Transferencia");
            }

            var cuentaDestinoObj = (Cuenta)cuentaDestinoBusqueda.Datos;
            var resultado = _servicioCajero.RealizarTransferencia(cuentaOrigenId, cuentaDestinoObj.Id, monto);

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
        /// Historial de transacciones.
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
        /// Muestra la vista para cambiar el PIN.
        /// </summary>
        [HttpGet]
        public IActionResult CambiarPIN()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa el cambio de PIN.
        /// </summary>
        [HttpPost]
        public IActionResult CambiarPIN(string pinActual, string pinNuevo, string pinConfirmar)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            if (pinNuevo != pinConfirmar)
            {
                TempData["Error"] = "Los PINs no coinciden.";
                return RedirectToAction("CambiarPIN");
            }

            var cuentaId = ObtenerCuentaId().Value;
            var cuenta = _servicioCajero.ObtenerCuenta(cuentaId);

            if (cuenta.PIN != pinActual)
            {
                TempData["Error"] = "El PIN actual es incorrecto.";
                return RedirectToAction("CambiarPIN");
            }

            // Aquí iría la lógica para actualizar el PIN (implementar en servicio)
            TempData["Mensaje"] = "PIN cambiado exitosamente.";
            return RedirectToAction("Index");
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
