# SERVICES - PERSONA 2 (CORE)

## Archivo 1: Cajero.Core/Services/ConfiguracionCuenta.cs

```csharp
using Cajero.Core.Models;

namespace Cajero.Core.Services
{
    /// <summary>
    /// Configuración de límites, comisiones y reglas por tipo de cuenta.
    /// </summary>
    public static class ConfiguracionCuenta
    {
        /// <summary>
        /// Obtiene la comisión por operación según el tipo de cuenta.
        /// </summary>
        public static decimal ObtenerComision(TipoCuenta tipo, string tipoOperacion)
        {
            return (tipo, tipoOperacion) switch
            {
                // Ahorros: 1.5% retiro, 0% depósito, 1.5% transferencia
                (TipoCuenta.Ahorros, "Retiro") => 0.015m,
                (TipoCuenta.Ahorros, "Deposito") => 0m,
                (TipoCuenta.Ahorros, "Transferencia") => 0.015m,

                // Corriente: 1% retiro, 0% depósito, 1% transferencia
                (TipoCuenta.Corriente, "Retiro") => 0.01m,
                (TipoCuenta.Corriente, "Deposito") => 0m,
                (TipoCuenta.Corriente, "Transferencia") => 0.01m,

                // Nómina: 0.5% retiro, 0% depósito, 0.5% transferencia
                (TipoCuenta.Nómina, "Retiro") => 0.005m,
                (TipoCuenta.Nómina, "Deposito") => 0m,
                (TipoCuenta.Nómina, "Transferencia") => 0.005m,

                _ => 0m
            };
        }

        /// <summary>
        /// Obtiene el límite máximo de retiro según el tipo de cuenta.
        /// </summary>
        public static decimal ObtenerLimiteRetiro(TipoCuenta tipo)
        {
            return tipo switch
            {
                TipoCuenta.Ahorros => 2000m,
                TipoCuenta.Corriente => 5000m,
                TipoCuenta.Nómina => 10000m,
                _ => 1000m
            };
        }

        /// <summary>
        /// Obtiene el límite máximo de transferencia según el tipo de cuenta.
        /// </summary>
        public static decimal ObtenerLimiteTransferencia(TipoCuenta tipo)
        {
            return tipo switch
            {
                TipoCuenta.Ahorros => 5000m,
                TipoCuenta.Corriente => 20000m,
                TipoCuenta.Nómina => 50000m,
                _ => 1000m
            };
        }
    }
}
```

---

## Archivo 2: Cajero.Core/Services/ServicioCajero.cs

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Services
{
    /// <summary>
    /// Servicio principal que orquesta operaciones del cajero.
    /// Contiene la lógica de negocio para autenticación y operaciones bancarias.
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
        /// Autentica un usuario validando número de cuenta y PIN.
        /// </summary>
        public ResultadoOperacion Autenticar(string numeroCuenta, string pin)
        {
            if (string.IsNullOrWhiteSpace(numeroCuenta) || string.IsNullOrWhiteSpace(pin))
            {
                return ResultadoOperacion.Error("El número de cuenta y PIN son requeridos.");
            }

            var cuenta = _repositorioCuenta.ObtenerPorNumero(numeroCuenta);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Número de cuenta no encontrado.");
            }

            if (cuenta.PIN != pin)
            {
                return ResultadoOperacion.Error("PIN incorrecto.");
            }

            var respuesta = new RespuestaAutenticacion
            {
                CuentaId = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                Propietario = cuenta.Propietario,
                TipoCuenta = cuenta.TipoCuenta
            };

            return ResultadoOperacion.Éxito("Autenticación exitosa.", respuesta);
        }

        /// <summary>
        /// Obtiene una cuenta por su ID.
        /// </summary>
        public Cuenta? ObtenerCuenta(int cuentaId)
        {
            return _repositorioCuenta.ObtenerPorId(cuentaId);
        }

        /// <summary>
        /// Consulta el saldo actual de una cuenta.
        /// </summary>
        public ResultadoOperacion ConsultarSaldo(int cuentaId)
        {
            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.");
            }

            var respuesta = new RespuestaSaldo
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                Propietario = cuenta.Propietario,
                Saldo = cuenta.Saldo,
                FechaConsulta = DateTime.Now
            };

            return ResultadoOperacion.Éxito("Saldo consultado exitosamente.", respuesta);
        }

        /// <summary>
        /// Obtiene el historial de transacciones de una cuenta.
        /// </summary>
        public ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId)
        {
            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.");
            }

            var transacciones = _repositorioTransaccion.ObtenerPorCuenta(cuentaId);
            return ResultadoOperacion.Éxito("Historial obtenido.", transacciones);
        }

        /// <summary>
        /// Busca una cuenta por su número.
        /// </summary>
        public ResultadoOperacion BuscarCuentaPorNumero(string numeroCuenta)
        {
            var cuenta = _repositorioCuenta.ObtenerPorNumero(numeroCuenta);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.");
            }

            return ResultadoOperacion.Éxito("Cuenta encontrada.", cuenta);
        }

        /// <summary>
        /// Realiza un retiro de dinero.
        /// Valida saldo suficiente y límites de retiro.
        /// </summary>
        public ResultadoOperacion RealizarRetiro(int cuentaId, decimal monto)
        {
            // Validaciones básicas
            if (monto <= 0)
            {
                return ResultadoOperacion.Error("El monto debe ser mayor a cero.");
            }

            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);
            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.");
            }

            // Validar límite de retiro
            var limiteRetiro = ConfiguracionCuenta.ObtenerLimiteRetiro(cuenta.TipoCuenta);
            if (monto > limiteRetiro)
            {
                return ResultadoOperacion.Error($"El límite de retiro es ${limiteRetiro}.");
            }

            // Calcular comisión
            var comisionPorcentaje = ConfiguracionCuenta.ObtenerComision(cuenta.TipoCuenta, "Retiro");
            var comision = Math.Round(monto * comisionPorcentaje, 2);
            var montoTotal = monto + comision;

            // Validar saldo suficiente
            if (cuenta.Saldo < montoTotal)
            {
                return ResultadoOperacion.Error($"Saldo insuficiente. Necesitas ${montoTotal:N2}, tienes ${cuenta.Saldo:N2}");
            }

            // Realizar operación
            cuenta.Saldo -= montoTotal;
            _repositorioCuenta.Actualizar(cuenta);

            // Registrar transacción
            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                Tipo = "Retiro",
                Monto = monto,
                Comision = comision,
                MontoTotal = montoTotal,
                FechaHora = DateTime.Now,
                NumeroReferencia = GenerarNumeroReferencia(),
                Descripcion = $"Retiro de ${monto:N2}"
            };

            _repositorioTransaccion.Guardar(transaccion);
            cuenta.TransaccionIds.Add(transaccion.Id);

            var comprobante = new Comprobante
            {
                NumeroReferencia = transaccion.NumeroReferencia,
                TipoOperacion = "Retiro",
                Monto = monto,
                Comision = comision,
                Total = montoTotal,
                FechaHora = transaccion.FechaHora,
                CuentaOrigen = cuenta.NumeroCuenta,
                Titular = cuenta.Propietario
            };

            var respuesta = new RespuestaOperacionConComprobante
            {
                Comprobante = comprobante,
                NuevoSaldo = cuenta.Saldo
            };

            return ResultadoOperacion.Éxito("Retiro realizado exitosamente.", respuesta);
        }

        /// <summary>
        /// Realiza un depósito de dinero.
        /// </summary>
        public ResultadoOperacion RealizarDeposito(int cuentaId, decimal monto)
        {
            if (monto <= 0)
            {
                return ResultadoOperacion.Error("El monto debe ser mayor a cero.");
            }

            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);
            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.");
            }

            // Depósitos no tienen comisión
            var montoTotal = monto;
            cuenta.Saldo += montoTotal;
            _repositorioCuenta.Actualizar(cuenta);

            var transaccion = new Transaccion
            {
                CuentaId = cuentaId,
                Tipo = "Deposito",
                Monto = monto,
                Comision = 0,
                MontoTotal = montoTotal,
                FechaHora = DateTime.Now,
                NumeroReferencia = GenerarNumeroReferencia(),
                Descripcion = $"Depósito de ${monto:N2}"
            };

            _repositorioTransaccion.Guardar(transaccion);
            cuenta.TransaccionIds.Add(transaccion.Id);

            var comprobante = new Comprobante
            {
                NumeroReferencia = transaccion.NumeroReferencia,
                TipoOperacion = "Deposito",
                Monto = monto,
                Comision = 0,
                Total = montoTotal,
                FechaHora = transaccion.FechaHora,
                CuentaOrigen = cuenta.NumeroCuenta,
                Titular = cuenta.Propietario
            };

            var respuesta = new RespuestaOperacionConComprobante
            {
                Comprobante = comprobante,
                NuevoSaldo = cuenta.Saldo
            };

            return ResultadoOperacion.Éxito("Depósito realizado exitosamente.", respuesta);
        }

        /// <summary>
        /// Realiza una transferencia entre cuentas.
        /// Valida que no sea la misma cuenta y que exista saldo suficiente.
        /// </summary>
        public ResultadoOperacion RealizarTransferencia(int cuentaIdOrigen, int cuentaIdDestino, decimal monto)
        {
            // Validaciones básicas
            if (monto <= 0)
            {
                return ResultadoOperacion.Error("El monto debe ser mayor a cero.");
            }

            if (cuentaIdOrigen == cuentaIdDestino)
            {
                return ResultadoOperacion.Error("No puedes transferir a tu propia cuenta.");
            }

            var cuentaOrigen = _repositorioCuenta.ObtenerPorId(cuentaIdOrigen);
            var cuentaDestino = _repositorioCuenta.ObtenerPorId(cuentaIdDestino);

            if (cuentaOrigen == null || cuentaDestino == null)
            {
                return ResultadoOperacion.Error("Una o ambas cuentas no existen.");
            }

            // Validar límite de transferencia
            var limiteTransferencia = ConfiguracionCuenta.ObtenerLimiteTransferencia(cuentaOrigen.TipoCuenta);
            if (monto > limiteTransferencia)
            {
                return ResultadoOperacion.Error($"El límite de transferencia es ${limiteTransferencia}.");
            }

            // Calcular comisión
            var comisionPorcentaje = ConfiguracionCuenta.ObtenerComision(cuentaOrigen.TipoCuenta, "Transferencia");
            var comision = Math.Round(monto * comisionPorcentaje, 2);
            var montoTotal = monto + comision;

            // Validar saldo suficiente
            if (cuentaOrigen.Saldo < montoTotal)
            {
                return ResultadoOperacion.Error($"Saldo insuficiente. Necesitas ${montoTotal:N2}, tienes ${cuentaOrigen.Saldo:N2}");
            }

            // Realizar operación
            cuentaOrigen.Saldo -= montoTotal;
            cuentaDestino.Saldo += monto; // Destino recibe solo el monto sin comisión

            _repositorioCuenta.Actualizar(cuentaOrigen);
            _repositorioCuenta.Actualizar(cuentaDestino);

            // Registrar transacción en origen
            var transaccion = new Transaccion
            {
                CuentaId = cuentaIdOrigen,
                Tipo = "Transferencia",
                Monto = monto,
                Comision = comision,
                MontoTotal = montoTotal,
                FechaHora = DateTime.Now,
                NumeroReferencia = GenerarNumeroReferencia(),
                Descripcion = $"Transferencia a {cuentaDestino.NumeroCuenta}"
            };

            _repositorioTransaccion.Guardar(transaccion);
            cuentaOrigen.TransaccionIds.Add(transaccion.Id);

            // Registrar transacción en destino
            var transaccionDestino = new Transaccion
            {
                CuentaId = cuentaIdDestino,
                Tipo = "Transferencia Recibida",
                Monto = monto,
                Comision = 0,
                MontoTotal = monto,
                FechaHora = DateTime.Now,
                NumeroReferencia = transaccion.NumeroReferencia,
                Descripcion = $"Transferencia desde {cuentaOrigen.NumeroCuenta}"
            };

            _repositorioTransaccion.Guardar(transaccionDestino);
            cuentaDestino.TransaccionIds.Add(transaccionDestino.Id);

            var comprobante = new Comprobante
            {
                NumeroReferencia = transaccion.NumeroReferencia,
                TipoOperacion = "Transferencia",
                Monto = monto,
                Comision = comision,
                Total = montoTotal,
                FechaHora = transaccion.FechaHora,
                CuentaOrigen = cuentaOrigen.NumeroCuenta,
                CuentaDestino = cuentaDestino.NumeroCuenta,
                Titular = cuentaOrigen.Propietario
            };

            var respuesta = new RespuestaOperacionConComprobante
            {
                Comprobante = comprobante,
                NuevoSaldo = cuentaOrigen.Saldo
            };

            return ResultadoOperacion.Éxito("Transferencia realizada exitosamente.", respuesta);
        }

        /// <summary>
        /// Actualiza el PIN de una cuenta.
        /// </summary>
        public ResultadoOperacion ActualizarPIN(int cuentaId, string pinActual, string pinNuevo)
        {
            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.");
            }

            if (cuenta.PIN != pinActual)
            {
                return ResultadoOperacion.Error("PIN actual incorrecto.");
            }

            if (string.IsNullOrWhiteSpace(pinNuevo) || pinNuevo.Length != 4)
            {
                return ResultadoOperacion.Error("El nuevo PIN debe tener 4 dígitos.");
            }

            cuenta.PIN = pinNuevo;
            _repositorioCuenta.Actualizar(cuenta);

            return ResultadoOperacion.Éxito("PIN actualizado exitosamente.");
        }

        /// <summary>
        /// Genera un número de referencia único para transacciones.
        /// </summary>
        private string GenerarNumeroReferencia()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999).ToString();
        }
    }
}
```

---

## 📋 CÓMO COPIAR

Para cada servicio:
1. Copia el código entre los bloques ```csharp
2. Crea un nuevo archivo en Visual Studio
3. Pega el contenido
4. Guarda con el nombre indicado
5. Sigue con el siguiente

**Orden:**
1. ConfiguracionCuenta.cs
2. ServicioCajero.cs
