using Xunit;
using Cajero.Core.Interfaces;
using Cajero.Core.Repositories;
using Cajero.Core.Services;
using Cajero.Core.Models;

namespace Cajero.Tests
{
    /// <summary>
    /// Suite completa de pruebas para el Sistema Cajero Automático
    /// Valida todas las operaciones principales del sistema
    /// </summary>
    public class ServicioCajeroTests
    {
        private readonly IServicioCajero _servicioCajero;
        private readonly IRepositorioCuenta _repositorioCuenta;
        private readonly IRepositorioTransaccion _repositorioTransaccion;

        public ServicioCajeroTests()
        {
            _repositorioCuenta = new RepositorioCuenta();
            _repositorioTransaccion = new RepositorioTransaccion();
            _servicioCajero = new ServicioCajero(_repositorioCuenta, _repositorioTransaccion);
        }

        #region Pruebas de Autenticación

        [Fact(DisplayName = "Autenticación - Login exitoso con credenciales válidas")]
        public void Autenticar_ConCredencialesValidas_DebeSerExitoso()
        {
            // Arrange
            var numeroCuenta = "412087654321";
            var pin = "8475";

            // Act
            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            // Assert
            Assert.True(resultado.Exitoso);
            Assert.NotNull(resultado.Datos);
        }

        [Fact(DisplayName = "Autenticación - PIN incorrecto")]
        public void Autenticar_ConPINIncorrecto_DebeFallar()
        {
            // Arrange
            var numeroCuenta = "412087654321";
            var pin = "9999";

            // Act
            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("PIN", resultado.Mensaje);
        }

        [Fact(DisplayName = "Autenticación - Cuenta no existe")]
        public void Autenticar_ConCuentaInexistente_DebeFallar()
        {
            // Arrange
            var numeroCuenta = "999999999999";
            var pin = "1234";

            // Act
            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("no encontrada", resultado.Mensaje);
        }

        #endregion

        #region Pruebas de Consulta de Saldo

        [Fact(DisplayName = "Saldo - Consulta correcta")]
        public void ConsultarSaldo_ConCuentaValida_DebeRetornarSaldo()
        {
            // Arrange
            var cuentaId = 1;

            // Act
            var resultado = _servicioCajero.ConsultarSaldo(cuentaId);

            // Assert
            Assert.True(resultado.Exitoso);
            Assert.NotNull(resultado.Datos);
            var respuesta = (RespuestaSaldo)resultado.Datos;
            Assert.True(respuesta.Saldo > 0);
        }

        [Fact(DisplayName = "Saldo - Cuenta inválida")]
        public void ConsultarSaldo_ConCuentaInvalida_DebeFallar()
        {
            // Arrange
            var cuentaId = 9999;

            // Act
            var resultado = _servicioCajero.ConsultarSaldo(cuentaId);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        #endregion

        #region Pruebas de Retiro

        [Fact(DisplayName = "Retiro - Retiro exitoso")]
        public void RealizarRetiro_ConMontoValido_DebeSerExitoso()
        {
            // Arrange
            var cuentaId = 1;
            var montoInicial = _servicioCajero.ConsultarSaldo(cuentaId);
            var saldoAntes = ((RespuestaSaldo)montoInicial.Datos).Saldo;
            var monto = 100m;

            // Act
            var resultado = _servicioCajero.RealizarRetiro(cuentaId, monto);

            // Assert
            Assert.True(resultado.Exitoso);
            var saldoActual = _servicioCajero.ConsultarSaldo(cuentaId);
            var nuevoSaldo = ((RespuestaSaldo)saldoActual.Datos).Saldo;
            Assert.Equal(saldoAntes - monto, nuevoSaldo);
        }

        [Fact(DisplayName = "Retiro - Saldo insuficiente")]
        public void RealizarRetiro_ConSaldoInsuficiente_DebeFallar()
        {
            // Arrange
            var cuentaId = 1;
            var monto = 1000000m; // Monto muy alto

            // Act
            var resultado = _servicioCajero.RealizarRetiro(cuentaId, monto);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.NotNull(resultado.Mensaje);
        }

        [Fact(DisplayName = "Retiro - Monto negativo")]
        public void RealizarRetiro_ConMontoNegativo_DebeFallar()
        {
            // Arrange
            var cuentaId = 1;
            var monto = -50m;

            // Act
            var resultado = _servicioCajero.RealizarRetiro(cuentaId, monto);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        #endregion

        #region Pruebas de Depósito

        [Fact(DisplayName = "Depósito - Depósito exitoso")]
        public void RealizarDeposito_ConMontoValido_DebeSerExitoso()
        {
            // Arrange
            var cuentaId = 1;
            var saldoAntes = _servicioCajero.ConsultarSaldo(cuentaId);
            var monto = 500m;

            // Act
            var resultado = _servicioCajero.RealizarDeposito(cuentaId, monto);

            // Assert
            Assert.True(resultado.Exitoso);
            var saldoActual = _servicioCajero.ConsultarSaldo(cuentaId);
            var nuevoSaldo = ((RespuestaSaldo)saldoActual.Datos).Saldo;
            var saldoOriginal = ((RespuestaSaldo)saldoAntes.Datos).Saldo;
            Assert.Equal(saldoOriginal + monto, nuevoSaldo);
        }

        [Fact(DisplayName = "Depósito - Monto negativo")]
        public void RealizarDeposito_ConMontoNegativo_DebeFallar()
        {
            // Arrange
            var cuentaId = 1;
            var monto = -100m;

            // Act
            var resultado = _servicioCajero.RealizarDeposito(cuentaId, monto);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        #endregion

        #region Pruebas de Transferencia

        [Fact(DisplayName = "Transferencia - Transferencia exitosa")]
        public void RealizarTransferencia_EntreCuentas_DebeSerExitosa()
        {
            // Arrange
            var cuentaOrigenId = 1;
            var cuentaDestinoId = 2;
            var monto = 100m;

            // Act
            var resultado = _servicioCajero.RealizarTransferencia(cuentaOrigenId, cuentaDestinoId, monto);

            // Assert
            Assert.True(resultado.Exitoso);
        }

        [Fact(DisplayName = "Transferencia - Misma cuenta")]
        public void RealizarTransferencia_AMismaCuenta_DebeFallar()
        {
            // Arrange
            var cuentaId = 1;
            var monto = 100m;

            // Act
            var resultado = _servicioCajero.RealizarTransferencia(cuentaId, cuentaId, monto);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("misma", resultado.Mensaje, System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "Transferencia - Saldo insuficiente")]
        public void RealizarTransferencia_ConSaldoInsuficiente_DebeFallar()
        {
            // Arrange
            var cuentaOrigenId = 1;
            var cuentaDestinoId = 2;
            var monto = 999999999m;

            // Act
            var resultado = _servicioCajero.RealizarTransferencia(cuentaOrigenId, cuentaDestinoId, monto);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("Saldo", resultado.Mensaje);
        }

        #endregion

        #region Pruebas de Historial

        [Fact(DisplayName = "Historial - Obtener historial de transacciones")]
        public void ObtenerHistorial_ConCuentaValida_DebeRetornarTransacciones()
        {
            // Arrange
            var cuentaId = 1;

            // Act
            var resultado = _servicioCajero.ObtenerHistorialTransacciones(cuentaId);

            // Assert
            Assert.True(resultado.Exitoso);
            Assert.NotNull(resultado.Datos);
        }

        #endregion

        #region Pruebas de Búsqueda de Cuenta

        [Fact(DisplayName = "Búsqueda - Encontrar cuenta por número")]
        public void BuscarCuenta_PorNumeroCorrecto_DebeEncontrar()
        {
            // Arrange
            var numeroCuenta = "412087654321";

            // Act
            var resultado = _servicioCajero.BuscarCuentaPorNumero(numeroCuenta);

            // Assert
            Assert.True(resultado.Exitoso);
            Assert.NotNull(resultado.Datos);
        }

        [Fact(DisplayName = "Búsqueda - Cuenta inexistente")]
        public void BuscarCuenta_PorNumeroBuscado_NoEncontrar()
        {
            // Arrange
            var numeroCuenta = "000000000000";

            // Act
            var resultado = _servicioCajero.BuscarCuentaPorNumero(numeroCuenta);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        #endregion
    }

    /// <summary>
    /// Pruebas para validación de modelos
    /// </summary>
    public class ModelosTests
    {
        [Fact(DisplayName = "Cuenta - Crear cuenta válida")]
        public void CrearCuenta_ConDatosValidos_DebeCrearse()
        {
            // Arrange & Act
            var cuenta = new Cuenta
            {
                Id = 1,
                NumeroCuenta = "412087654321",
                Propietario = "Test Usuario",
                PIN = "1234",
                Saldo = 1000m,
                TipoCuenta = TipoCuentaEnum.Corriente,
                Activa = true,
                FechaCreacion = DateTime.Now
            };

            // Assert
            Assert.NotNull(cuenta);
            Assert.Equal("412087654321", cuenta.NumeroCuenta);
            Assert.True(cuenta.Activa);
            Assert.Equal(1000m, cuenta.Saldo);
        }

        [Fact(DisplayName = "ResultadoOperacion - Crear resultado exitoso")]
        public void ResultadoOperacion_Exito_DebeCrearse()
        {
            // Arrange & Act
            var resultado = ResultadoOperacion.Exito("Operación completada", new { test = "data" });

            // Assert
            Assert.True(resultado.Exitoso);
            Assert.Equal("Operación completada", resultado.Mensaje);
            Assert.NotNull(resultado.Datos);
        }

        [Fact(DisplayName = "ResultadoOperacion - Crear resultado error")]
        public void ResultadoOperacion_Error_DebeCrearse()
        {
            // Arrange & Act
            var resultado = ResultadoOperacion.Error("Operación fallida", "ERROR_CODE");

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Equal("Operación fallida", resultado.Mensaje);
            Assert.Equal("ERROR_CODE", resultado.Codigo);
        }
    }
}
