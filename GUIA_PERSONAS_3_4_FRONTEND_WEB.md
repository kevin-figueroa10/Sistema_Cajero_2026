# 📕 GUÍA DE DESARROLLO - PERSONAS 3 Y 4: FRONTEND WEB

**Proyecto:** BANCO NEW SMART CAPITAL - Cajero Automático 2026  
**Personas:** 3 (Frontend - Views) y 4 (Frontend - Controllers)  
**Rama Asignada:** `feature-frontend-web`  
**Proyecto:** `Cajero.Web`  
**Fecha:** Enero 2026

---

## 📌 RESUMEN DE RESPONSABILIDADES

### Persona 3: **Frontend - Views** (Interfaz Gráfica)
Tu trabajo es crear todas las **vistas HTML/Razor** que los usuarios verán.

✅ Lo que debes hacer:
- ✅ Crear vistas de login (.cshtml)
- ✅ Crear menú principal
- ✅ Crear formularios (retiro, depósito, transferencia)
- ✅ Crear vista de detalles de cuenta
- ✅ Aplicar estilos y diseño
- ✅ Agregar validación de cliente (JavaScript)

❌ Lo que NO debes hacer:
- ❌ No tocar Controllers (eso es para Persona 4)
- ❌ No modificar la lógica de negocio
- ❌ No cambiar la rama main

---

### Persona 4: **Frontend - Controllers** (Lógica de Presentación)
Tu trabajo es crear todos los **controladores** que conectan vistas con el backend.

✅ Lo que debes hacer:
- ✅ Crear controlador de autenticación
- ✅ Crear controlador de operaciones
- ✅ Manejar sesiones de usuario
- ✅ Validar entrada de usuario
- ✅ Conectar con servicios del backend (Cajero.Core)

❌ Lo que NO debes hacer:
- ❌ No tocar vistas (eso es para Persona 3)
- ❌ No modificar modelos de datos
- ❌ No cambiar la rama main

---

## 🎯 ESTRUCTURA DEL PROYECTO

El proyecto es: **`Cajero.Web`**

```
Sistema Cajero/
├── Cajero.Web/                        ← VUESTRO PROYECTO
│   ├── Controllers/                   ← PERSONA 4
│   │   ├── AutenticacionController.cs
│   │   └── PrincipalController.cs
│   ├── Views/                         ← PERSONA 3
│   │   ├── Autenticacion/
│   │   │   └── Index.cshtml
│   │   ├── Principal/
│   │   │   ├── Index.cshtml
│   │   │   ├── ConsultarSaldo.cshtml
│   │   │   ├── Retiro.cshtml
│   │   │   ├── Deposito.cshtml
│   │   │   ├── Transferencia.cshtml
│   │   │   ├── Historial.cshtml
│   │   │   ├── MiCuenta.cshtml
│   │   │   └── CambiarPIN.cshtml
│   │   └── Shared/
│   │       ├── _Layout.cshtml
│   │       └── Error.cshtml
│   ├── wwwroot/
│   │   ├── css/
│   │   │   └── styles.css
│   │   ├── js/
│   │   │   └── validaciones.js
│   │   └── lib/
│   ├── Program.cs
│   └── Cajero.Web.csproj
├── Cajero.Core/
├── Cajero.Consola/
└── .git/
```

---

## 🚀 PASO 1: PREPARAR TU ENTORNO

### 1.1 Clonar el repositorio (si aún no lo has hecho)
```powershell
cd C:\Users\[TuNombre]\Documents
git clone https://github.com/kevin-figueroa10/Sistema_Cajero_2026.git
cd "Sistema Cajero"
```

### 1.2 Cambiar a rama asignada
```powershell
git checkout feature-frontend-web
```

### 1.3 Actualizar rama
```powershell
git pull origin feature-frontend-web
```

### 1.4 Verificar que estés en la rama correcta
```powershell
git branch
# Deberías ver: * feature-frontend-web
```

---

## 👥 COORDINACIÓN ENTRE PERSONAS 3 Y 4

Para evitar conflictos:

1. **Persona 4 crea el controlador primero**
   - Define qué vistas necesita
   - Que datos pasa a cada vista

2. **Persona 3 crea las vistas**
   - Basándose en lo que el controlador define
   - Usa los mismos nombres de propiedades

3. **Ambas hacen commits frecuentes**
   - No esperen días para sincronizar

---

## 💻 CÓDIGO A IMPLEMENTAR

### PARTE 1: CONTROLLERS (Persona 4)

---

### ARCHIVO 1: `Controllers/AutenticacionController.cs`

**Ubicación:** `Cajero.Web/Controllers/AutenticacionController.cs`

```csharp
using Cajero.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cajero.Web.Controllers
{
    /// <summary>
    /// Controlador para autenticación de usuarios.
    /// Maneja login y sesiones.
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
        /// Muestra pantalla de login.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Procesa el login del usuario.
        /// </summary>
        [HttpPost]
        public IActionResult Index(string numeroCuenta, string pin)
        {
            if (string.IsNullOrWhiteSpace(numeroCuenta) || string.IsNullOrWhiteSpace(pin))
            {
                ViewData["Error"] = "El número de cuenta y PIN son requeridos.";
                return View();
            }

            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            if (resultado.Exitoso)
            {
                var respuesta = (RespuestaAutenticacion)resultado.Datos;
                var cuenta = _servicioCajero.ObtenerCuenta(respuesta.CuentaId);

                // Guardar datos en sesión
                HttpContext.Session.SetInt32("CuentaId", cuenta.Id);
                HttpContext.Session.SetString("Propietario", cuenta.Propietario);
                HttpContext.Session.SetString("NumeroCuenta", cuenta.NumeroCuenta);
                HttpContext.Session.SetString("TipoCuenta", cuenta.TipoCuenta.ToString());
                HttpContext.Session.SetString("Saldo", cuenta.Saldo.ToString("F2"));
                HttpContext.Session.SetString("FechaCreacion", cuenta.FechaCreacion.ToString("dd/MM/yyyy"));

                _logger.LogInformation($"Usuario autenticado: {cuenta.Propietario}");
                return RedirectToAction("Index", "Principal");
            }

            ViewData["Error"] = resultado.Mensaje;
            _logger.LogWarning($"Intento de login fallido: {resultado.Mensaje}");
            return View();
        }

        /// <summary>
        /// Cierra la sesión del usuario.
        /// </summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
```

---

### ARCHIVO 2: `Controllers/PrincipalController.cs`

**Ubicación:** `Cajero.Web/Controllers/PrincipalController.cs`

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cajero.Web.Controllers
{
    /// <summary>
    /// Controlador para operaciones principales del cajero.
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
                TempData["Error"] = "Debes iniciar sesión.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Menú principal.
        /// </summary>
        public IActionResult Index()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Consulta el saldo.
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
        /// Formulario de retiro (GET).
        /// </summary>
        [HttpGet]
        public IActionResult Retiro()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa el retiro (POST).
        /// </summary>
        [HttpPost]
        public IActionResult Retiro(decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.RealizarRetiro(cuentaId, monto);

            if (resultado.Exitoso)
            {
                var datosRetiro = (RespuestaOperacionConComprobante)resultado.Datos;
                TempData["Exito"] = resultado.Mensaje;
                TempData["Comprobante"] = System.Text.Json.JsonSerializer.Serialize(datosRetiro.Comprobante);
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            return RedirectToAction("Retiro");
        }

        /// <summary>
        /// Formulario de depósito (GET).
        /// </summary>
        [HttpGet]
        public IActionResult Deposito()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa el depósito (POST).
        /// </summary>
        [HttpPost]
        public IActionResult Deposito(decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaId = ObtenerCuentaId().Value;
            var resultado = _servicioCajero.RealizarDeposito(cuentaId, monto);

            if (resultado.Exitoso)
            {
                var datosDeposito = (RespuestaOperacionConComprobante)resultado.Datos;
                TempData["Exito"] = resultado.Mensaje;
                TempData["Comprobante"] = System.Text.Json.JsonSerializer.Serialize(datosDeposito.Comprobante);
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
            return RedirectToAction("Deposito");
        }

        /// <summary>
        /// Formulario de transferencia (GET).
        /// </summary>
        [HttpGet]
        public IActionResult Transferencia()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Busca cuenta destino para transferencia (POST).
        /// </summary>
        [HttpPost]
        public IActionResult BuscarCuentaTransferencia(string cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaOrigenId = ObtenerCuentaId().Value;

            // Validaciones
            if (monto <= 0 || monto < 5 || monto % 5 != 0)
            {
                TempData["Error"] = "Monto inválido. Debe ser múltiplo de $5.";
                return RedirectToAction("Transferencia");
            }

            var resultado = _servicioCajero.BuscarCuentaPorNumero(cuentaDestino);

            if (!resultado.Exitoso)
            {
                TempData["Error"] = resultado.Mensaje;
                return RedirectToAction("Transferencia");
            }

            var cuentaDestinoObj = (Cuenta)resultado.Datos;

            if (cuentaOrigenId == cuentaDestinoObj.Id)
            {
                TempData["Error"] = "No puedes transferir a tu propia cuenta.";
                return RedirectToAction("Transferencia");
            }

            ViewBag.Monto = monto;
            return View("ConfirmarTransferencia", cuentaDestinoObj);
        }

        /// <summary>
        /// Confirma y procesa la transferencia (POST).
        /// </summary>
        [HttpPost("Principal/ConfirmarTransferencia")]
        public IActionResult ConfirmarTransferencia(string cuentaDestino, decimal monto)
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            var cuentaOrigenId = ObtenerCuentaId().Value;

            var busqueda = _servicioCajero.BuscarCuentaPorNumero(cuentaDestino);
            if (!busqueda.Exitoso)
            {
                TempData["Error"] = "Cuenta destino no encontrada.";
                return RedirectToAction("Transferencia");
            }

            var cuentaDestinoObj = (Cuenta)busqueda.Datos;
            var resultado = _servicioCajero.RealizarTransferencia(cuentaOrigenId, cuentaDestinoObj.Id, monto);

            if (resultado.Exitoso)
            {
                var datosTransferencia = (RespuestaOperacionConComprobante)resultado.Datos;
                TempData["Exito"] = resultado.Mensaje;
                TempData["Comprobante"] = System.Text.Json.JsonSerializer.Serialize(datosTransferencia.Comprobante);
                return RedirectToAction("Comprobante");
            }

            TempData["Error"] = resultado.Mensaje;
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
        /// Muestra detalles de la cuenta.
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

            return View(cuenta);
        }

        /// <summary>
        /// Formulario para cambiar PIN (GET).
        /// </summary>
        [HttpGet]
        public IActionResult CambiarPIN()
        {
            if (!ValidarSesion())
                return RedirectToAction("Index", "Autenticacion");

            return View();
        }

        /// <summary>
        /// Procesa cambio de PIN (POST).
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

            // Aquí iría la lógica para actualizar el PIN
            TempData["Mensaje"] = "PIN cambiado exitosamente.";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra el comprobante de la transacción.
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
```

---

### PARTE 2: VIEWS (Persona 3)

---

### ARCHIVO 3: `Views/Autenticacion/Index.cshtml`

**Ubicación:** `Cajero.Web/Views/Autenticacion/Index.cshtml`

```razor
@{
    ViewData["Title"] = "Login - BANCO NEW SMART CAPITAL";
}

<div class="login-container">
    <div class="login-card">
        <!-- Header -->
        <div class="login-header">
            <h1>🏦</h1>
            <h2>BANCO NEW SMART CAPITAL</h2>
            <p>Sistema Cajero Automático 2026</p>
        </div>

        <!-- Mensaje de Error -->
        @if (ViewData["Error"] != null)
        {
            <div class="alert alert-danger">
                @ViewData["Error"]
            </div>
        }

        <!-- Formulario -->
        <form method="post" onsubmit="return validarLogin()">
            <div class="form-group">
                <label for="numeroCuenta">Número de Cuenta</label>
                <input type="text" id="numeroCuenta" name="numeroCuenta" 
                       class="form-control" placeholder="412087654321" required>
                <small class="form-text text-muted">12 dígitos</small>
            </div>

            <div class="form-group">
                <label for="pin">PIN</label>
                <input type="password" id="pin" name="pin" 
                       class="form-control" placeholder="••••" maxlength="4" required>
                <small class="form-text text-muted">4 dígitos</small>
            </div>

            <button type="submit" class="btn btn-primary btn-block">
                🔓 Iniciar Sesión
            </button>
        </form>

        <!-- Info -->
        <div class="login-info">
            <p><strong>Cuentas de Prueba:</strong></p>
            <ul>
                <li>Juan: 412087654321 / 8475</li>
                <li>María: 412087654322 / 5829</li>
                <li>Carlos: 412087654323 / 9403</li>
            </ul>
        </div>
    </div>
</div>

<script>
    function validarLogin() {
        var numeroCuenta = document.getElementById('numeroCuenta').value.trim();
        var pin = document.getElementById('pin').value;

        if (numeroCuenta.length !== 12 || !/^\d+$/.test(numeroCuenta)) {
            alert('El número de cuenta debe tener 12 dígitos.');
            return false;
        }

        if (pin.length !== 4 || !/^\d+$/.test(pin)) {
            alert('El PIN debe tener 4 dígitos.');
            return false;
        }

        return true;
    }
</script>

<style>
    .login-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    }

    .login-card {
        background: white;
        padding: 40px;
        border-radius: 10px;
        box-shadow: 0 10px 25px rgba(0,0,0,0.2);
        width: 100%;
        max-width: 400px;
    }

    .login-header {
        text-align: center;
        margin-bottom: 30px;
    }

    .login-header h1 {
        font-size: 48px;
        margin: 0;
    }

    .login-header h2 {
        color: #333;
        margin: 10px 0 5px;
        font-size: 24px;
    }

    .login-header p {
        color: #666;
        margin: 0;
        font-size: 14px;
    }

    .form-group {
        margin-bottom: 15px;
    }

    .form-control {
        width: 100%;
        padding: 10px;
        border: 1px solid #ddd;
        border-radius: 5px;
        font-size: 16px;
    }

    .btn {
        width: 100%;
        padding: 10px;
        background: #667eea;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
        margin-top: 10px;
    }

    .login-info {
        margin-top: 30px;
        padding-top: 20px;
        border-top: 1px solid #eee;
        font-size: 12px;
    }

    .login-info ul {
        list-style: none;
        padding: 0;
        margin: 10px 0 0;
    }

    .login-info li {
        padding: 5px 0;
        color: #666;
    }

    .alert {
        padding: 10px;
        margin-bottom: 20px;
        border-radius: 5px;
    }

    .alert-danger {
        background: #f8d7da;
        color: #721c24;
        border: 1px solid #f5c6cb;
    }
</style>
```

---

### ARCHIVO 4: `Views/Principal/Index.cshtml`

**Ubicación:** `Cajero.Web/Views/Principal/Index.cshtml`

```razor
@{
    ViewData["Title"] = "Menú Principal";
}

<div class="container">
    <div class="row mb-4">
        <div class="col-md-12">
            <div class="alert alert-primary text-center py-3">
                <h2 class="mb-0">🏦 BANCO NEW SMART CAPITAL</h2>
                <p class="mb-0 small">Sistema Cajero Automático 2026</p>
            </div>
        </div>
    </div>

    <div class="row mb-4">
        <div class="col-md-12">
            <h1 class="mb-2">Bienvenido, @Context.Session.GetString("Propietario")</h1>
            <p class="text-muted">
                Tipo de Cuenta: 
                <span class="badge badge-info">
                    @(Context.Session.GetString("TipoCuenta") ?? "Ahorro")
                </span>
            </p>
            <p class="text-muted">Selecciona la operación que deseas realizar</p>
        </div>
    </div>

    <div class="menu-grid">
        <a href="/Principal/ConsultarSaldo" class="menu-item">
            <div class="menu-item-icon">💰</div>
            <div class="menu-item-title">Consultar Saldo</div>
        </a>

        <a href="/Principal/Deposito" class="menu-item">
            <div class="menu-item-icon">📥</div>
            <div class="menu-item-title">Realizar Depósito</div>
        </a>

        <a href="/Principal/Retiro" class="menu-item">
            <div class="menu-item-icon">📤</div>
            <div class="menu-item-title">Realizar Retiro</div>
        </a>

        <a href="/Principal/Transferencia" class="menu-item">
            <div class="menu-item-icon">💸</div>
            <div class="menu-item-title">Transferencia</div>
        </a>

        <a href="/Principal/Historial" class="menu-item">
            <div class="menu-item-icon">📋</div>
            <div class="menu-item-title">Historial</div>
        </a>

        <a href="/Principal/MiCuenta" class="menu-item">
            <div class="menu-item-icon">🪪</div>
            <div class="menu-item-title">Mi Cuenta</div>
        </a>

        <a href="/Principal/CambiarPIN" class="menu-item">
            <div class="menu-item-icon">🔐</div>
            <div class="menu-item-title">Cambiar PIN</div>
        </a>
    </div>

    <div class="row mt-4">
        <div class="col-md-12">
            <a href="/Autenticacion/Logout" class="btn btn-danger btn-block">
                🚪 Cerrar Sesión
            </a>
        </div>
    </div>
</div>

<style>
    .menu-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
        gap: 15px;
        margin: 30px 0;
    }

    .menu-item {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        padding: 20px;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        color: white;
        border-radius: 10px;
        text-decoration: none;
        transition: transform 0.3s;
    }

    .menu-item:hover {
        transform: scale(1.05);
        color: white;
    }

    .menu-item-icon {
        font-size: 32px;
        margin-bottom: 10px;
    }

    .menu-item-title {
        font-size: 14px;
        text-align: center;
        font-weight: bold;
    }
</style>
```

---

### ARCHIVO 5: `Views/Principal/Retiro.cshtml`

**Ubicación:** `Cajero.Web/Views/Principal/Retiro.cshtml`

```razor
@{
    ViewData["Title"] = "Realizar Retiro";
}

<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-lg-6">
            <div class="card shadow-lg">
                <div class="card-header bg-danger text-white">
                    <h4 class="mb-0">📤 REALIZAR RETIRO</h4>
                </div>
                <div class="card-body p-4">
                    @if (TempData["Error"] != null)
                    {
                        <div class="alert alert-danger">@TempData["Error"]</div>
                    }

                    <form method="post">
                        <div class="form-group">
                            <label for="monto"><strong>Monto a Retirar ($)</strong></label>
                            <input type="number" class="form-control form-control-lg" id="monto" name="monto" 
                                   step="5" min="5" placeholder="0.00" required onblur="validarMonto()">
                            <small class="form-text text-muted">Mínimo: $5 | Múltiplos de $5</small>
                            <div id="errorMonto" class="alert alert-danger mt-2" style="display:none;"></div>
                        </div>

                        <div class="d-grid gap-2">
                            <button type="submit" class="btn btn-danger btn-lg">
                                ✓ Confirmar Retiro
                            </button>
                            <a href="/Principal/" class="btn btn-secondary btn-lg">
                                ✕ Cancelar
                            </a>
                        </div>
                    </form>

                    <hr>

                    <div class="alert alert-info">
                        <strong>📋 Límites:</strong>
                        <ul class="mb-0 mt-2">
                            <li>Máximo por transacción: $1,000</li>
                            <li>Límite diario: $3,000</li>
                            <li>Múltiplos de: $5</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    function validarMonto() {
        var monto = parseFloat(document.getElementById('monto').value);
        var errorDiv = document.getElementById('errorMonto');

        errorDiv.style.display = 'none';

        if (monto < 5) {
            errorDiv.textContent = '❌ El monto mínimo es $5';
            errorDiv.style.display = 'block';
            return false;
        }

        if (monto % 5 !== 0) {
            errorDiv.textContent = '❌ El monto debe ser múltiplo de $5';
            errorDiv.style.display = 'block';
            return false;
        }

        if (monto > 1000) {
            errorDiv.textContent = '❌ El monto máximo es $1,000';
            errorDiv.style.display = 'block';
            return false;
        }

        return true;
    }
</script>
```

---

### ARCHIVO 6: `Views/Principal/Deposito.cshtml`

**Ubicación:** `Cajero.Web/Views/Principal/Deposito.cshtml`

```razor
@{
    ViewData["Title"] = "Realizar Depósito";
}

<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-lg-6">
            <div class="card shadow-lg">
                <div class="card-header bg-success text-white">
                    <h4 class="mb-0">📥 REALIZAR DEPÓSITO</h4>
                </div>
                <div class="card-body p-4">
                    @if (TempData["Error"] != null)
                    {
                        <div class="alert alert-danger">@TempData["Error"]</div>
                    }

                    <form method="post">
                        <div class="form-group">
                            <label for="monto"><strong>Monto a Depositar ($)</strong></label>
                            <input type="number" class="form-control form-control-lg" id="monto" name="monto" 
                                   step="0.01" min="5" placeholder="0.00" required>
                            <small class="form-text text-muted">Mínimo: $5.00 | Sin límite máximo</small>
                        </div>

                        <div class="d-grid gap-2">
                            <button type="submit" class="btn btn-success btn-lg">
                                ✓ Confirmar Depósito
                            </button>
                            <a href="/Principal/" class="btn btn-secondary btn-lg">
                                ✕ Cancelar
                            </a>
                        </div>
                    </form>

                    <hr>

                    <div class="alert alert-info">
                        <strong>📋 Información:</strong>
                        <ul class="mb-0 mt-2">
                            <li>Mínimo: $5</li>
                            <li>Sin límite máximo</li>
                            <li>Sin comisiones</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
```

---

### ARCHIVO 7: `Views/Shared/_Layout.cshtml`

**Ubicación:** `Cajero.Web/Views/Shared/_Layout.cshtml`

```razor
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - BANCO NEW SMART CAPITAL</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" rel="stylesheet">

    <style>
        body {
            background: #f5f5f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .container {
            max-width: 1200px;
        }

        .navbar {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        }

        footer {
            background: #333;
            color: white;
            padding: 20px 0;
            margin-top: 40px;
            text-align: center;
        }

        .alert {
            margin: 20px 0;
        }

        .btn-primary {
            background: #667eea;
            border-color: #667eea;
        }

        .btn-primary:hover {
            background: #764ba2;
            border-color: #764ba2;
        }

        .badge-info {
            background: #17a2b8;
        }

        .badge-success {
            background: #28a745;
        }

        .badge-danger {
            background: #dc3545;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container">
            <a class="navbar-brand" href="/">🏦 BANCO NEW SMART CAPITAL</a>
            @if (Context.Session.GetString("Propietario") != null)
            {
                <span class="navbar-text text-white ml-auto">
                    Hola, @Context.Session.GetString("Propietario")
                    <a href="/Autenticacion/Logout" class="btn btn-sm btn-outline-light ml-2">Salir</a>
                </span>
            }
        </div>
    </nav>

    <div class="container">
        @RenderBody()
    </div>

    <footer>
        <p>&copy; 2026 BANCO NEW SMART CAPITAL - Sistema Cajero Automático</p>
    </footer>

    <!-- Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
```

---

## 🔧 CONFIGURACIÓN DEL PROYECTO

Verifica que `Cajero.Web.csproj` tenga:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Cajero.Core\Cajero.Core.csproj" />
  </ItemGroup>

</Project>
```

---

## 📤 CÓMO ENVIAR CAMBIOS A GITHUB

### Paso 1: Ver cambios
```powershell
cd "C:\Users\[TuNombre]\Downloads\Sistema Cajero"
git status
```

### Paso 2: Agregar cambios
```powershell
git add .
```

### Paso 3: Crear commit
```powershell
git commit -m "feat: Implementar interfaz web con views y controllers

- Crear controladores de autenticación y operaciones
- Diseñar vistas de login, menú, retiro, depósito
- Implementar validaciones de cliente
- Agregar estilos Bootstrap y CSS personalizado
- Manejar sesiones y autenticación"
```

### Paso 4: Enviar cambios
```powershell
git push origin feature-frontend-web
```

### Paso 5: Crear Pull Request
1. Ve a: https://github.com/kevin-figueroa10/Sistema_Cajero_2026
2. Pull Requests → New
3. Base: `develop` | Compare: `feature-frontend-web`
4. Describe: "Implementación completa de Frontend Web"
5. Create

---

## ✅ CHECKLIST DE IMPLEMENTACIÓN

- [ ] Clonar repositorio
- [ ] Cambiar a rama `feature-frontend-web`
- [ ] **Persona 4:** Crear controladores (Autenticacion, Principal)
- [ ] **Persona 3:** Crear vistas (Login, Menú, Retiro, Depósito, etc.)
- [ ] **Persona 3:** Agregar Layout.cshtml y estilos
- [ ] **Ambos:** Hacer commits frecuentes
- [ ] **Ambos:** Hacer push a GitHub
- [ ] **Ambos:** Crear Pull Request

---

## 🧪 CÓMO PROBAR

```powershell
cd "C:\Users\[TuNombre]\Downloads\Sistema Cajero"
dotnet run --project Cajero.Web
```

Accede a: `http://localhost:5000`

---

**Versión:** 2026.1.0  
**Estado:** Listo para implementar  
**Última actualización:** Enero 2026

