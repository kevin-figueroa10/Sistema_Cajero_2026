# 📗 GUÍA DE DESARROLLO - PERSONA 2: BACKEND DATA

**Proyecto:** BANCO NEW SMART CAPITAL - Cajero Automático 2026  
**Persona:** 2 (Backend - Datos / Base de Datos)  
**Rama Asignada:** `feature-backend`  
**Proyectos:** `Cajero.Core`  
**Fecha:** Enero 2026

---

## 📌 RESUMEN DE RESPONSABILIDADES

Como **Persona 2**, tu trabajo es desarrollar toda la **lógica de negocio y gestión de datos** del sistema bancario.

### ✅ Lo que debes hacer:
- ✅ Crear modelos de datos (Cuenta, Transaccion, etc.)
- ✅ Implementar servicios (ServicioCajero)
- ✅ Crear repositorios (RepositorioCuenta, RepositorioTransaccion)
- ✅ Manejar validaciones de negocio
- ✅ Gestionar saldos, transacciones y transferencias

### ❌ Lo que NO debes hacer:
- ❌ No modificar Cajero.Web (eso es para Personas 3 y 4)
- ❌ No crear interfaces gráficas
- ❌ No hacer cambios en la rama main

---

## 🎯 ESTRUCTURA DEL PROYECTO

Tu proyecto es: **`Cajero.Core`**

```
Sistema Cajero/
├── Cajero.Core/                       ← TU PROYECTO
│   ├── Models/
│   │   ├── Cuenta.cs
│   │   ├── Transaccion.cs
│   │   ├── Comprobante.cs
│   │   └── Enums/
│   │       └── TipoCuentaEnum.cs
│   ├── Interfaces/
│   │   ├── IServicioCajero.cs
│   │   ├── IRepositorioCuenta.cs
│   │   └── IRepositorioTransaccion.cs
│   ├── Repositories/
│   │   ├── RepositorioCuenta.cs
│   │   └── RepositorioTransaccion.cs
│   ├── Services/
│   │   └── ServicioCajero.cs
│   ├── Responses/
│   │   ├── ResultadoOperacion.cs
│   │   ├── RespuestaAutenticacion.cs
│   │   └── RespuestaOperacionConComprobante.cs
│   └── Cajero.Core.csproj
├── Cajero.Consola/
├── Cajero.Web/
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

### 1.2 Cambiar a tu rama asignada
```powershell
git checkout feature-backend
```

**NOTA:** Personas 1 y 2 comparten la misma rama (`feature-backend`). Coordinen su trabajo y hagan commits frecuentes.

### 1.3 Actualizar tu rama
```powershell
git pull origin feature-backend
```

### 1.4 Verificar que estés en la rama correcta
```powershell
git branch
# Deberías ver: * feature-backend
```

---

## 📁 CARPETAS A CREAR/VERIFICAR

Dentro de `Cajero.Core/`, estructura de carpetas:

```
Cajero.Core/
├── Models/                ← Crear si no existe
│   ├── Cuenta.cs
│   ├── Transaccion.cs
│   ├── Comprobante.cs
│   └── Enums/
│       └── TipoCuentaEnum.cs
├── Interfaces/            ← Crear si no existe
│   ├── IServicioCajero.cs
│   ├── IRepositorioCuenta.cs
│   └── IRepositorioTransaccion.cs
├── Repositories/          ← Crear si no existe
│   ├── RepositorioCuenta.cs
│   └── RepositorioTransaccion.cs
├── Services/              ← Crear si no existe
│   └── ServicioCajero.cs
├── Responses/             ← Crear si no existe
│   ├── ResultadoOperacion.cs
│   ├── RespuestaAutenticacion.cs
│   └── RespuestaOperacionConComprobante.cs
└── Cajero.Core.csproj
```

### Comandos para crear carpetas:
```powershell
mkdir Cajero.Core\Models
mkdir Cajero.Core\Models\Enums
mkdir Cajero.Core\Interfaces
mkdir Cajero.Core\Repositories
mkdir Cajero.Core\Services
mkdir Cajero.Core\Responses
```

---

## 💻 CÓDIGO A IMPLEMENTAR

### ARCHIVO 1: `Models/Enums/TipoCuentaEnum.cs`

**Ubicación:** `Cajero.Core/Models/Enums/TipoCuentaEnum.cs`

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Tipos de cuenta bancaria disponibles.
    /// </summary>
    public enum TipoCuentaEnum
    {
        Ahorro = 0,
        Corriente = 1,
        Plazo = 2
    }
}
```

---

### ARCHIVO 2: `Models/Cuenta.cs`

**Ubicación:** `Cajero.Core/Models/Cuenta.cs`

```csharp
using System;

namespace Cajero.Core.Models
{
    /// <summary>
    /// Modelo que representa una cuenta bancaria.
    /// </summary>
    public class Cuenta
    {
        public int Id { get; set; }

        public string NumeroCuenta { get; set; }

        public string Propietario { get; set; }

        public string PIN { get; set; }

        public decimal Saldo { get; set; }

        public TipoCuentaEnum TipoCuenta { get; set; }

        public bool Activa { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public decimal RetirosDia { get; set; } = 0;

        public int TransferenciasHoy { get; set; } = 0;

        public DateTime UltimaTransferencia { get; set; }

        public Cuenta()
        {
            Activa = true;
            TipoCuenta = TipoCuentaEnum.Ahorro;
            FechaCreacion = DateTime.Now;
            FechaExpiracion = DateTime.Now.AddYears(5);
        }
    }
}
```

---

### ARCHIVO 3: `Models/Transaccion.cs`

**Ubicación:** `Cajero.Core/Models/Transaccion.cs`

```csharp
using System;

namespace Cajero.Core.Models
{
    /// <summary>
    /// Modelo que representa una transacción bancaria.
    /// </summary>
    public class Transaccion
    {
        public int Id { get; set; }

        public int CuentaId { get; set; }

        public string Tipo { get; set; } // Retiro, Depósito, Transferencia

        public decimal Monto { get; set; }

        public decimal SaldoAnterior { get; set; }

        public decimal SaldoNuevo { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Descripcion { get; set; }

        public int? CuentaDestinoId { get; set; }

        public Transaccion()
        {
            Fecha = DateTime.Now;
        }
    }
}
```

---

### ARCHIVO 4: `Models/Comprobante.cs`

**Ubicación:** `Cajero.Core/Models/Comprobante.cs`

```csharp
using System;

namespace Cajero.Core.Models
{
    /// <summary>
    /// Comprobante de una transacción realizada.
    /// </summary>
    public class Comprobante
    {
        public string NumeroReferencia { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string TipoOperacion { get; set; }

        public decimal Monto { get; set; }

        public decimal Comision { get; set; }

        public decimal SaldoAnterior { get; set; }

        public decimal SaldoNuevo { get; set; }

        public string NumeroCuenta { get; set; }

        public string Titular { get; set; }

        public string CuentaDestino { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }
    }
}
```

---

### ARCHIVO 5: `Responses/ResultadoOperacion.cs`

**Ubicación:** `Cajero.Core/Responses/ResultadoOperacion.cs`

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Resultado genérico de una operación en el sistema.
    /// </summary>
    public class ResultadoOperacion
    {
        public bool Exitoso { get; set; }

        public string Mensaje { get; set; }

        public object Datos { get; set; }

        public string Codigo { get; set; }

        public static ResultadoOperacion Exito(string mensaje, object datos = null)
        {
            return new ResultadoOperacion
            {
                Exitoso = true,
                Mensaje = mensaje,
                Datos = datos
            };
        }

        public static ResultadoOperacion Error(string mensaje, string codigo)
        {
            return new ResultadoOperacion
            {
                Exitoso = false,
                Mensaje = mensaje,
                Codigo = codigo
            };
        }
    }

    public class RespuestaAutenticacion
    {
        public int CuentaId { get; set; }
        public string Propietario { get; set; }
    }

    public class RespuestaSaldo
    {
        public decimal Saldo { get; set; }
        public string NumeroCuenta { get; set; }
        public string Propietario { get; set; }
    }

    public class RespuestaOperacionConComprobante
    {
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public string CuentaDestino { get; set; }
        public Comprobante Comprobante { get; set; }
    }
}
```

---

### ARCHIVO 6: `Interfaces/IRepositorioCuenta.cs`

**Ubicación:** `Cajero.Core/Interfaces/IRepositorioCuenta.cs`

```csharp
using Cajero.Core.Models;
using System.Collections.Generic;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de cuentas.
    /// </summary>
    public interface IRepositorioCuenta
    {
        Cuenta ObtenerPorId(int id);

        Cuenta ObtenerPorNumeroCuenta(string numeroCuenta);

        List<Cuenta> ObtenerTodas();

        void Agregar(Cuenta cuenta);

        void Actualizar(Cuenta cuenta);

        void Eliminar(int id);
    }
}
```

---

### ARCHIVO 7: `Interfaces/IRepositorioTransaccion.cs`

**Ubicación:** `Cajero.Core/Interfaces/IRepositorioTransaccion.cs`

```csharp
using Cajero.Core.Models;
using System.Collections.Generic;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de transacciones.
    /// </summary>
    public interface IRepositorioTransaccion
    {
        void Registrar(Transaccion transaccion);

        List<Transaccion> ObtenerPorCuenta(int cuentaId);

        List<Transaccion> ObtenerTodas();
    }
}
```

---

### ARCHIVO 8: `Interfaces/IServicioCajero.cs`

**Ubicación:** `Cajero.Core/Interfaces/IServicioCajero.cs`

```csharp
using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz principal del servicio del cajero.
    /// Define todas las operaciones disponibles.
    /// </summary>
    public interface IServicioCajero
    {
        // Autenticación
        ResultadoOperacion Autenticar(string numeroCuenta, string pin);

        // Consultas
        ResultadoOperacion ConsultarSaldo(int cuentaId);

        Cuenta ObtenerCuenta(int cuentaId);

        ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId);

        ResultadoOperacion BuscarCuentaPorNumero(string numeroCuenta);

        // Operaciones
        ResultadoOperacion RealizarRetiro(int cuentaId, decimal monto);

        ResultadoOperacion RealizarDeposito(int cuentaId, decimal monto);

        ResultadoOperacion RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto);
    }
}
```

---

### ARCHIVO 9: `Repositories/RepositorioCuenta.cs`

**Ubicación:** `Cajero.Core/Repositories/RepositorioCuenta.cs`

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio en memoria para gestionar cuentas.
    /// Simula una base de datos.
    /// </summary>
    public class RepositorioCuenta : IRepositorioCuenta
    {
        private List<Cuenta> _cuentas = new List<Cuenta>();
        private int _proximoId = 1;

        public RepositorioCuenta()
        {
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            // Cuenta 1: Corriente
            _cuentas.Add(new Cuenta
            {
                Id = 1,
                NumeroCuenta = "412087654321",
                Propietario = "Juan García López",
                PIN = "8475",
                Saldo = 15750.50m,
                TipoCuenta = TipoCuentaEnum.Corriente,
                Activa = true,
                FechaCreacion = new DateTime(2022, 3, 15),
                FechaExpiracion = new DateTime(2027, 3, 15)
            });

            // Cuenta 2: Ahorro
            _cuentas.Add(new Cuenta
            {
                Id = 2,
                NumeroCuenta = "412087654322",
                Propietario = "María López Rodríguez",
                PIN = "5829",
                Saldo = 23400.75m,
                TipoCuenta = TipoCuentaEnum.Ahorro,
                Activa = true,
                FechaCreacion = new DateTime(2021, 7, 22),
                FechaExpiracion = new DateTime(2026, 7, 22)
            });

            // Cuenta 3: Corriente Premium
            _cuentas.Add(new Cuenta
            {
                Id = 3,
                NumeroCuenta = "412087654323",
                Propietario = "Carlos Martínez González",
                PIN = "9403",
                Saldo = 45600.00m,
                TipoCuenta = TipoCuentaEnum.Corriente,
                Activa = true,
                FechaCreacion = new DateTime(2020, 11, 10),
                FechaExpiracion = new DateTime(2025, 11, 10)
            });

            _proximoId = 4;
        }

        public Cuenta ObtenerPorId(int id)
        {
            return _cuentas.FirstOrDefault(c => c.Id == id);
        }

        public Cuenta ObtenerPorNumeroCuenta(string numeroCuenta)
        {
            return _cuentas.FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
        }

        public List<Cuenta> ObtenerTodas()
        {
            return new List<Cuenta>(_cuentas);
        }

        public void Agregar(Cuenta cuenta)
        {
            cuenta.Id = _proximoId++;
            _cuentas.Add(cuenta);
        }

        public void Actualizar(Cuenta cuenta)
        {
            var cuentaExistente = ObtenerPorId(cuenta.Id);
            if (cuentaExistente != null)
            {
                cuentaExistente.Saldo = cuenta.Saldo;
                cuentaExistente.PIN = cuenta.PIN;
                cuentaExistente.RetirosDia = cuenta.RetirosDia;
                cuentaExistente.TransferenciasHoy = cuenta.TransferenciasHoy;
                cuentaExistente.UltimaTransferencia = cuenta.UltimaTransferencia;
            }
        }

        public void Eliminar(int id)
        {
            var cuenta = ObtenerPorId(id);
            if (cuenta != null)
            {
                _cuentas.Remove(cuenta);
            }
        }
    }
}
```

---

### ARCHIVO 10: `Repositories/RepositorioTransaccion.cs`

**Ubicación:** `Cajero.Core/Repositories/RepositorioTransaccion.cs`

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio en memoria para gestionar transacciones.
    /// </summary>
    public class RepositorioTransaccion : IRepositorioTransaccion
    {
        private List<Transaccion> _transacciones = new List<Transaccion>();
        private int _proximoId = 1;

        public void Registrar(Transaccion transaccion)
        {
            transaccion.Id = _proximoId++;
            _transacciones.Add(transaccion);
        }

        public List<Transaccion> ObtenerPorCuenta(int cuentaId)
        {
            return _transacciones.Where(t => t.CuentaId == cuentaId).ToList();
        }

        public List<Transaccion> ObtenerTodas()
        {
            return new List<Transaccion>(_transacciones);
        }
    }
}
```

---

### ARCHIVO 11: `Services/ServicioCajero.cs` (PARTE 1 - Autenticación y Consultas)

**Ubicación:** `Cajero.Core/Services/ServicioCajero.cs`

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Core.Services
{
    /// <summary>
    /// Servicio principal que implementa toda la lógica del cajero.
    /// </summary>
    public class ServicioCajero : IServicioCajero
    {
        private readonly IRepositorioCuenta _repositorioCuenta;
        private readonly IRepositorioTransaccion _repositorioTransaccion;

        public ServicioCajero(IRepositorioCuenta repositorioCuenta, IRepositorioTransaccion repositorioTransaccion)
        {
            _repositorioCuenta = repositorioCuenta;
            _repositorioTransaccion = repositorioTransaccion;
        }

        /// <summary>
        /// Autentica un usuario con número de cuenta y PIN.
        /// </summary>
        public ResultadoOperacion Autenticar(string numeroCuenta, string pin)
        {
            if (string.IsNullOrWhiteSpace(numeroCuenta) || string.IsNullOrWhiteSpace(pin))
            {
                return ResultadoOperacion.Error("Número de cuenta y PIN son requeridos.", "VALIDACION_FALLIDA");
            }

            var cuenta = _repositorioCuenta.ObtenerPorNumeroCuenta(numeroCuenta);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            if (!cuenta.Activa)
            {
                return ResultadoOperacion.Error("La cuenta está inactiva.", "CUENTA_INACTIVA");
            }

            if (cuenta.PIN != pin)
            {
                return ResultadoOperacion.Error("PIN incorrecto.", "PIN_INCORRECTO");
            }

            var respuesta = new RespuestaAutenticacion
            {
                CuentaId = cuenta.Id,
                Propietario = cuenta.Propietario
            };

            return ResultadoOperacion.Exito("Autenticación exitosa.", respuesta);
        }

        /// <summary>
        /// Consulta el saldo actual de una cuenta.
        /// </summary>
        public ResultadoOperacion ConsultarSaldo(int cuentaId)
        {
            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            var respuesta = new RespuestaSaldo
            {
                Saldo = cuenta.Saldo,
                NumeroCuenta = cuenta.NumeroCuenta,
                Propietario = cuenta.Propietario
            };

            return ResultadoOperacion.Exito("Saldo consultado correctamente.", respuesta);
        }

        /// <summary>
        /// Obtiene los datos completos de una cuenta.
        /// </summary>
        public Cuenta ObtenerCuenta(int cuentaId)
        {
            return _repositorioCuenta.ObtenerPorId(cuentaId);
        }

        /// <summary>
        /// Obtiene el historial de transacciones de una cuenta.
        /// </summary>
        public ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId)
        {
            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            var transacciones = _repositorioTransaccion.ObtenerPorCuenta(cuentaId);
            transacciones = transacciones.OrderByDescending(t => t.Fecha).ToList();

            return ResultadoOperacion.Exito("Historial obtenido correctamente.", transacciones);
        }

        /// <summary>
        /// Busca una cuenta por número.
        /// </summary>
        public ResultadoOperacion BuscarCuentaPorNumero(string numeroCuenta)
        {
            var cuenta = _repositorioCuenta.ObtenerPorNumeroCuenta(numeroCuenta);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            if (!cuenta.Activa)
            {
                return ResultadoOperacion.Error("La cuenta destino no está activa.", "CUENTA_INACTIVA");
            }

            return ResultadoOperacion.Exito("Cuenta encontrada.", cuenta);
        }

        /// <summary>
        /// Realiza un retiro de dinero.
        /// </summary>
        public ResultadoOperacion RealizarRetiro(int cuentaId, decimal monto)
        {
            if (monto <= 0)
            {
                return ResultadoOperacion.Error("El monto debe ser mayor a cero.", "MONTO_INVALIDO");
            }

            if (monto < 5)
            {
                return ResultadoOperacion.Error("El monto mínimo de retiro es $5.00", "MONTO_MINIMO_INVALIDO");
            }

            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            var config = ConfiguracionCuenta.ObtenerConfiguracion(cuenta.TipoCuenta);

            if (config.LimiteDiarioRetiro == 0)
            {
                return ResultadoOperacion.Error("Esta cuenta no permite retiros.", "OPERACION_NO_PERMITIDA");
            }

            if (monto > config.LimitePorTransaccion)
            {
                return ResultadoOperacion.Error($"El límite máximo por transacción es ${config.LimitePorTransaccion:N2}", "LIMITE_TRANSACCION_EXCEDIDO");
            }

            if (cuenta.RetirosDia + monto > config.LimiteDiarioRetiro)
            {
                var disponible = config.LimiteDiarioRetiro - cuenta.RetirosDia;
                return ResultadoOperacion.Error($"Límite diario de retiro excedido. Disponible: ${disponible:N2}", "LIMITE_DIARIO_EXCEDIDO");
            }

            if (monto % 5 != 0)
            {
                return ResultadoOperacion.Error("El monto debe ser múltiplo de $5", "MULTIPLO_NO_VALIDO");
            }

            if (cuenta.Saldo < monto)
            {
                return ResultadoOperacion.Error("Saldo insuficiente.", "SALDO_INSUFICIENTE");
            }

            var saldoAnterior = cuenta.Saldo;
            cuenta.Saldo -= monto;
            cuenta.RetirosDia += monto;
            _repositorioCuenta.Actualizar(cuenta);

            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                Tipo = "Retiro",
                Monto = monto,
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = cuenta.Saldo,
                Descripcion = $"Retiro de ${monto:N2}"
            };

            _repositorioTransaccion.Registrar(transaccion);

            var comprobante = GenerarComprobante("RETIRO", transaccion, cuenta, monto, 0);

            var respuesta = new RespuestaOperacionConComprobante
            {
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = cuenta.Saldo,
                Monto = monto,
                Comprobante = comprobante,
                Comision = 0
            };

            return ResultadoOperacion.Exito("Retiro realizado exitosamente.", respuesta);
        }

        /// <summary>
        /// Realiza un depósito de dinero.
        /// </summary>
        public ResultadoOperacion RealizarDeposito(int cuentaId, decimal monto)
        {
            if (monto <= 0)
            {
                return ResultadoOperacion.Error("El monto debe ser mayor a cero.", "MONTO_INVALIDO");
            }

            if (monto < 5)
            {
                return ResultadoOperacion.Error("El monto mínimo de depósito es $5.00", "MONTO_MINIMO_INVALIDO");
            }

            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            var saldoAnterior = cuenta.Saldo;
            cuenta.Saldo += monto;
            _repositorioCuenta.Actualizar(cuenta);

            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                Tipo = "Depósito",
                Monto = monto,
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = cuenta.Saldo,
                Descripcion = $"Depósito de ${monto:N2}"
            };

            _repositorioTransaccion.Registrar(transaccion);

            var comprobante = GenerarComprobante("DEPOSITO", transaccion, cuenta, monto, 0);

            var respuesta = new RespuestaOperacionConComprobante
            {
                SaldoAnterior = saldoAnterior,
                SaldoNuevo = cuenta.Saldo,
                Monto = monto,
                Comprobante = comprobante,
                Comision = 0
            };

            return ResultadoOperacion.Exito("Depósito realizado exitosamente.", respuesta);
        }

        /// <summary>
        /// Realiza una transferencia entre cuentas.
        /// </summary>
        public ResultadoOperacion RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {
            if (monto <= 0)
            {
                return ResultadoOperacion.Error("El monto debe ser mayor a cero.", "MONTO_INVALIDO");
            }

            if (cuentaOrigenId == cuentaDestinoId)
            {
                return ResultadoOperacion.Error("No se puede transferir a la misma cuenta.", "CUENTAS_IGUALES");
            }

            var cuentaOrigen = _repositorioCuenta.ObtenerPorId(cuentaOrigenId);
            var cuentaDestino = _repositorioCuenta.ObtenerPorId(cuentaDestinoId);

            if (cuentaOrigen == null || cuentaDestino == null)
            {
                return ResultadoOperacion.Error("Una o ambas cuentas no existen.", "CUENTA_NO_EXISTE");
            }

            var config = ConfiguracionCuenta.ObtenerConfiguracion(cuentaOrigen.TipoCuenta);
            var comision = (config.ComisionTransferencia / 100) * monto;

            if (cuentaOrigen.TransferenciasHoy >= config.MaximoTransferenciasDialas && config.MaximoTransferenciasDialas > 0)
            {
                return ResultadoOperacion.Error($"Ha excedido el límite de transferencias diarias ({config.MaximoTransferenciasDialas})", "LIMITE_TRANSFERENCIAS_EXCEDIDO");
            }

            if (cuentaOrigen.Saldo < (monto + comision))
            {
                return ResultadoOperacion.Error("Saldo insuficiente para realizar la transferencia (incluye comisión).", "SALDO_INSUFICIENTE");
            }

            var saldoOrigenAnterior = cuentaOrigen.Saldo;
            var saldoDestinoAnterior = cuentaDestino.Saldo;

            cuentaOrigen.Saldo -= (monto + comision);
            cuentaOrigen.TransferenciasHoy++;
            cuentaOrigen.UltimaTransferencia = DateTime.Now;

            cuentaDestino.Saldo += monto;

            _repositorioCuenta.Actualizar(cuentaOrigen);
            _repositorioCuenta.Actualizar(cuentaDestino);

            var transaccionOrigen = new Transaccion
            {
                CuentaId = cuentaOrigenId,
                Tipo = "Transferencia",
                Monto = monto,
                SaldoAnterior = saldoOrigenAnterior,
                SaldoNuevo = cuentaOrigen.Saldo,
                CuentaDestinoId = cuentaDestinoId,
                Descripcion = $"Transferencia a {cuentaDestino.NumeroCuenta} - ${monto:N2}"
            };

            _repositorioTransaccion.Registrar(transaccionOrigen);

            var transaccionDestino = new Transaccion
            {
                CuentaId = cuentaDestinoId,
                Tipo = "Transferencia",
                Monto = monto,
                SaldoAnterior = saldoDestinoAnterior,
                SaldoNuevo = cuentaDestino.Saldo,
                CuentaDestinoId = cuentaOrigenId,
                Descripcion = $"Transferencia desde {cuentaOrigen.NumeroCuenta} - ${monto:N2}"
            };

            _repositorioTransaccion.Registrar(transaccionDestino);

            var comprobante = GenerarComprobante("TRANSFERENCIA", transaccionOrigen, cuentaOrigen, monto, comision, cuentaDestino);

            var respuesta = new RespuestaOperacionConComprobante
            {
                SaldoAnterior = saldoOrigenAnterior,
                SaldoNuevo = cuentaOrigen.Saldo,
                Monto = monto,
                CuentaDestino = cuentaDestino.NumeroCuenta,
                Comision = comision,
                Comprobante = comprobante
            };

            return ResultadoOperacion.Exito("Transferencia realizada exitosamente.", respuesta);
        }

        /// <summary>
        /// Genera un comprobante de transacción.
        /// </summary>
        private Comprobante GenerarComprobante(
            string tipoOperacion,
            Transaccion transaccion,
            Cuenta cuenta,
            decimal monto,
            decimal comision,
            Cuenta cuentaDestino = null)
        {
            var ahora = DateTime.Now;
            var numeroReferencia = $"TXN-{ahora:yyyyMMdd}-{new Random().Next(100000, 999999)}";

            return new Comprobante
            {
                NumeroReferencia = numeroReferencia,
                Fecha = ahora.Date,
                Hora = ahora.ToString("HH:mm:ss"),
                TipoOperacion = tipoOperacion,
                Monto = monto,
                SaldoAnterior = transaccion.SaldoAnterior,
                SaldoNuevo = transaccion.SaldoNuevo,
                NumeroCuenta = cuenta.NumeroCuenta,
                Titular = cuenta.Propietario,
                Descripcion = transaccion.Descripcion,
                CuentaDestino = cuentaDestino?.NumeroCuenta,
                Comision = comision,
                Estado = "EXITOSO"
            };
        }
    }
}
```

---

### ARCHIVO 12: `Services/ConfiguracionCuenta.cs`

**Ubicación:** `Cajero.Core/Services/ConfiguracionCuenta.cs`

```csharp
using Cajero.Core.Models;

namespace Cajero.Core.Services
{
    /// <summary>
    /// Configuración específica por tipo de cuenta.
    /// </summary>
    public class ConfiguracionCuenta
    {
        public decimal LimitePorTransaccion { get; set; }

        public decimal LimiteDiarioRetiro { get; set; }

        public int MaximoTransferenciasDialas { get; set; }

        public decimal ComisionTransferencia { get; set; }

        public static ConfiguracionCuenta ObtenerConfiguracion(TipoCuentaEnum tipoCuenta)
        {
            return tipoCuenta switch
            {
                TipoCuentaEnum.Ahorro => new ConfiguracionCuenta
                {
                    LimitePorTransaccion = 1000m,
                    LimiteDiarioRetiro = 3000m,
                    MaximoTransferenciasDialas = 3,
                    ComisionTransferencia = 0m // Sin comisión
                },
                TipoCuentaEnum.Corriente => new ConfiguracionCuenta
                {
                    LimitePorTransaccion = 1000m,
                    LimiteDiarioRetiro = 3000m,
                    MaximoTransferenciasDialas = 10,
                    ComisionTransferencia = 1.5m // 1.5% comisión
                },
                TipoCuentaEnum.Plazo => new ConfiguracionCuenta
                {
                    LimitePorTransaccion = 500m,
                    LimiteDiarioRetiro = 1000m,
                    MaximoTransferenciasDialas = 0,
                    ComisionTransferencia = 2m
                },
                _ => new ConfiguracionCuenta
                {
                    LimitePorTransaccion = 1000m,
                    LimiteDiarioRetiro = 3000m,
                    MaximoTransferenciasDialas = 3,
                    ComisionTransferencia = 0m
                }
            };
        }
    }
}
```

---

## 🔧 CONFIGURACIÓN DEL PROYECTO

Verifica que `Cajero.Core.csproj` tenga esta estructura:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

</Project>
```

---

## 📤 CÓMO ENVIAR TUS CAMBIOS A GITHUB

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
git commit -m "feat: Implementar modelos, interfaces, repositorios y servicios de cajero

- Crear modelos (Cuenta, Transaccion, Comprobante)
- Implementar interfaces (IServicioCajero, IRepositorioCuenta, IRepositorioTransaccion)
- Crear repositorios con datos iniciales
- Implementar lógica de negocio (retiro, depósito, transferencia)
- Agregar validaciones y límites por tipo de cuenta"
```

### Paso 4: Enviar cambios
```powershell
git push origin feature-backend
```

### Paso 5: Crear Pull Request
1. Ve a: https://github.com/kevin-figueroa10/Sistema_Cajero_2026
2. Pull Requests → New Pull Request
3. Base: `develop` | Compare: `feature-backend`
4. Describe: "Implementación completa de Backend - Datos"
5. Create Pull Request

---

## ✅ CHECKLIST DE IMPLEMENTACIÓN

- [ ] Clonar repositorio
- [ ] Cambiar a rama `feature-backend`
- [ ] Crear carpetas (Models, Interfaces, Repositories, Services, Responses)
- [ ] Implementar Models (Cuenta, Transaccion, Comprobante, TipoCuentaEnum)
- [ ] Implementar Interfaces (IServicioCajero, IRepositorioCuenta, IRepositorioTransaccion)
- [ ] Implementar Repositories (RepositorioCuenta, RepositorioTransaccion)
- [ ] Implementar Services (ServicioCajero, ConfiguracionCuenta)
- [ ] Hacer commit con mensaje descriptivo
- [ ] Hacer push a GitHub
- [ ] Crear Pull Request hacia `develop`

---

## 🧪 CÓMO PROBAR TU CÓDIGO

```powershell
cd "C:\Users\[TuNombre]\Downloads\Sistema Cajero"
dotnet build
```

Si todo compila sin errores, ¡bien hecho!

---

## 📞 IMPORTANTE

**COORDINACIÓN CON PERSONA 1:**
- Ambas personas trabajan en `feature-backend`
- Coordinen frecuentemente con commits
- Revisen que no haya conflictos
- Usen mensajes de commit claros

---

**Versión:** 2026.1.0  
**Estado:** Listo para implementar  
**Última actualización:** Enero 2026

