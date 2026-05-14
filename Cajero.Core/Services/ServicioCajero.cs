using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Services
{
    /// <summary>
    /// Implementación del servicio principal del cajero.
    /// Contiene toda la lógica de negocio del sistema.
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
        /// Realiza un retiro de dinero de la cuenta.
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

            // Obtener configuración de la cuenta
            var config = ConfiguracionCuenta.ObtenerConfiguracion(cuenta.TipoCuenta);

            // Validaciones avanzadas
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

            // Validar múltiplos de 5
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
        /// Realiza un depósito de dinero a la cuenta.
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
        /// Realiza una transferencia entre dos cuentas.
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

            // Obtener configuración
            var config = ConfiguracionCuenta.ObtenerConfiguracion(cuentaOrigen.TipoCuenta);
            var comision = (config.ComisionTransferencia / 100) * monto;

            // Validaciones avanzadas
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

            // Registrar transacción en cuenta origen
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

            // Registrar transacción en cuenta destino
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
        /// Obtiene el historial de transacciones con filtros avanzados.
        /// </summary>
        public ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId)
        {
            var cuenta = _repositorioCuenta.ObtenerPorId(cuentaId);

            if (cuenta == null)
            {
                return ResultadoOperacion.Error("Cuenta no encontrada.", "CUENTA_NO_EXISTE");
            }

            var transacciones = _repositorioTransaccion.ObtenerPorCuenta(cuentaId);

            // Ordenar por fecha descendente (más reciente primero)
            transacciones = transacciones.OrderByDescending(t => t.Fecha).ToList();

            return ResultadoOperacion.Exito("Historial obtenido correctamente.", transacciones);
        }

        /// <summary>
        /// Obtiene los detalles completos de una cuenta.
        /// </summary>
        public Cuenta ObtenerCuenta(int cuentaId)
        {
            return _repositorioCuenta.ObtenerPorId(cuentaId);
        }

        /// <summary>
        /// Busca una cuenta por número de cuenta.
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
