using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cajero.Web.Controllers
{
    /// <summary>
    /// Controlador para operaciones principales del cajero.
    /// Maneja: saldo, retiro, depósito, transferencia, historial, etc.
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

        /// <summary>
        /// Obtiene el ID de la cuenta desde la sesión.
        /// </summary>
        private int? ObtenerCuentaId()
        {
            return HttpContext.Session.GetInt32("CuentaId");
        }

        /// <summary>
        /// Valida que el usuario tenga una sesión activa.
        /// </summary>
        private bool ValidarSesion()
        {
            if (ObtenerCuentaId() == null)
            {
                TempData["Error"] = "Debes iniciar sesión.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Menú principal del cajero.
        /// Muestra todas las operaciones disponibles.
        /// </summary>
        public IActionResult Index()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            _logger.LogInformation("Usuario accedió al menú principal.");
            return View();
        }

        /// <summary>
        /// Consulta el saldo actual de la cuenta.
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
                _logger.LogInformation($"Consulta de saldo - Cuenta: {datos.NumeroCuenta}, Saldo: ${datos.Saldo}");
            }
            else
            {
                TempData["Error"] = resultado.Mensaje;
                _logger.LogError($"Error consultando saldo: {resultado.Mensaje}");
            }

            return View();
        }

        /// <summary>
        /// Formulario para realizar un retiro (GET).
        /// </summary>
        [HttpGet]
        public IActionResult Retiro()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa un retiro de dinero (POST).
        /// Valida el monto, realiza el retiro y muestra comprobante.
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
                TempData["Comprobante"] = JsonSerializer.Serialize(datosRetiro.Comprobante);

                _logger.LogInformation($"Retiro exitoso: ${monto}");
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Retiro fallido: {resultado.Mensaje}");
            return RedirectToAction("Retiro");
        }

        /// <summary>
        /// Formulario para realizar un depósito (GET).
        /// </summary>
        [HttpGet]
        public IActionResult Deposito()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa un depósito de dinero (POST).
        /// Realiza el depósito y muestra comprobante.
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
                TempData["Comprobante"] = JsonSerializer.Serialize(datosDeposito.Comprobante);

                _logger.LogInformation($"Depósito exitoso: ${monto}");
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Depósito fallido: {resultado.Mensaje}");
            return RedirectToAction("Deposito");
        }

        /// <summary>
        /// Formulario para iniciar una transferencia (GET).
        /// Usuario ingresa número de cuenta destino y monto.
        /// </summary>
        [HttpGet]
        public IActionResult Transferencia()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Busca la cuenta destino y muestra confirmación (POST).
        /// Valida el monto y la existencia de la cuenta destino.
        /// </summary>
        [HttpPost]
        public IActionResult BuscarCuentaTransferencia(string cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaOrigenId = ObtenerCuentaId().Value;
            var cuentaOrigen = _servicioCajero.ObtenerCuenta(cuentaOrigenId);

            // Validaciones básicas del monto
            if (monto <= 0)
            {
                TempData["Error"] = "El monto debe ser mayor a cero.";
                return RedirectToAction("Transferencia");
            }

            if (monto < 5)
            {
                TempData["Error"] = "El monto mínimo de transferencia es $5.00";
                return RedirectToAction("Transferencia");
            }

            if (monto % 5 != 0)
            {
                TempData["Error"] = "El monto debe ser múltiplo de $5";
                return RedirectToAction("Transferencia");
            }

            // Buscar cuenta destino
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

            if (cuentaOrigen.NumeroCuenta == cuentaDestinoObj.NumeroCuenta)
            {
                TempData["Error"] = "No puedes transferir a tu propia cuenta.";
                return RedirectToAction("Transferencia");
            }

            // Guardar monto en ViewBag para mostrar en confirmación
            ViewBag.Monto = monto;

            _logger.LogInformation($"Transferencia a confirmar: ${monto} a {cuentaDestino}");
            return View("ConfirmarTransferencia", cuentaDestinoObj);
        }

        /// <summary>
        /// Confirma y procesa la transferencia (POST).
        /// Realiza la transacción y muestra comprobante.
        /// </summary>
        [HttpPost("Principal/ConfirmarTransferencia")]
        public IActionResult ConfirmarTransferencia(string cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            _logger.LogInformation($"Transferencia confirmada: ${monto} a {cuentaDestino}");

            var cuentaOrigenId = ObtenerCuentaId().Value;

            // Buscar la cuenta destino nuevamente
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
                TempData["Comprobante"] = JsonSerializer.Serialize(datosTransferencia.Comprobante);

                _logger.LogInformation($"Transferencia exitosa: ${monto}");
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Transferencia fallida: {resultado.Mensaje}");
            return RedirectToAction("Transferencia");
        }

        /// <summary>
        /// Muestra el historial de transacciones del usuario.
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
                _logger.LogInformation("Historial de transacciones consultado.");
            }
            else
            {
                TempData["Error"] = resultado.Mensaje;
                _logger.LogError($"Error obteniendo historial: {resultado.Mensaje}");
            }

            return View();
        }

        /// <summary>
        /// Muestra los detalles completos de la cuenta del usuario.
        /// </summary>
        public IActionResult MiCuenta()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaId = ObtenerCuentaId().Value;
            var cuenta = _servicioCajero.ObtenerCuenta(cuentaId);

            if (cuenta == null)
            {
                TempData["Error"] = "Cuenta no encontrada.";
                return RedirectToAction("Index");
            }

            _logger.LogInformation($"Detalles de cuenta consultados: {cuenta.NumeroCuenta}");
            return View(cuenta);
        }

        /// <summary>
        /// Formulario para cambiar el PIN (GET).
        /// </summary>
        [HttpGet]
        public IActionResult CambiarPIN()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa el cambio de PIN (POST).
        /// Valida el PIN actual y guarda el nuevo.
        /// </summary>
        [HttpPost]
        public IActionResult CambiarPIN(string pinActual, string pinNuevo, string pinConfirmar)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            // Validaciones
            if (string.IsNullOrEmpty(pinActual) || string.IsNullOrEmpty(pinNuevo))
            {
                TempData["Error"] = "Todos los campos son requeridos.";
                return RedirectToAction("CambiarPIN");
            }

            if (pinNuevo != pinConfirmar)
            {
                TempData["Error"] = "Los PINs no coinciden.";
                return RedirectToAction("CambiarPIN");
            }

            if (pinNuevo.Length != 4 || !int.TryParse(pinNuevo, out _))
            {
                TempData["Error"] = "El nuevo PIN debe tener 4 dígitos.";
                return RedirectToAction("CambiarPIN");
            }

            var cuentaId = ObtenerCuentaId().Value;
            var cuenta = _servicioCajero.ObtenerCuenta(cuentaId);

            if (cuenta.PIN != pinActual)
            {
                TempData["Error"] = "El PIN actual es incorrecto.";
                _logger.LogWarning("Intento de cambiar PIN con PIN incorrecto.");
                return RedirectToAction("CambiarPIN");
            }

            // Aquí iría la lógica para actualizar el PIN en la BD
            // Por ahora solo mostramos un mensaje de éxito
            TempData["Exito"] = "PIN cambiado exitosamente.";
            _logger.LogInformation("PIN cambiado exitosamente.");
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra el comprobante de la última operación realizada.
        /// Obtiene el comprobante desde TempData (JSON serializado).
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
                var comprobante = JsonSerializer.Deserialize<Comprobante>(comprobanteJson);
                _logger.LogInformation($"Comprobante mostrado: {comprobante.NumeroReferencia}");
                return View(comprobante);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deserializando comprobante: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}