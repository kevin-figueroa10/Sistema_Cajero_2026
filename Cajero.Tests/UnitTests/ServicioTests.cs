using Xunit;
using Cajero.Core.Services;
using Cajero.Core.Repositories;
using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Tests.UnitTests
{
    /// <summary>
    /// Pruebas unitarias para el servicio principal del cajero.
    /// </summary>
    public class ServicioTests
    {
        private readonly IServicioCajero _servicio;

        public ServicioTests()
        {
            var repositorioCuenta = new RepositorioCuenta();
            var repositorioTransaccion = new RepositorioTransaccion();
            _servicio = new ServicioCajero(repositorioCuenta, repositorioTransaccion);
        }

        [Fact]
        public void Autenticacion_ConCredencialesValidas_DebeRetornarExitoso()
        {
            // Arrange
            string cuenta = "100000000001";
            string pin = "1234";

            // Act
            var resultado = _servicio.Autenticar(cuenta, pin);

            // Assert
            Assert.True(resultado.Exitoso);
            Assert.NotNull(resultado.Datos);
        }

        [Fact]
        public void Autenticacion_ConPinIncorrecto_DebeRetornarError()
        {
            // Arrange
            string cuenta = "100000000001";
            string pinIncorrecto = "9999";

            // Act
            var resultado = _servicio.Autenticar(cuenta, pinIncorrecto);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("PIN", resultado.Mensaje);
        }

        [Fact]
        public void Autenticacion_ConCuentaNoExistente_DebeRetornarError()
        {
            // Arrange
            string cuentaInvalida = "999999999999";
            string pin = "1234";

            // Act
            var resultado = _servicio.Autenticar(cuentaInvalida, pin);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("no encontrada", resultado.Mensaje);
        }

        [Fact]
        public void ConsultarSaldo_ConCuentaValida_DebeRetornarSaldo()
        {
            // Arrange
            int cuentaId = 1;

            // Act
            var resultado = _servicio.ConsultarSaldo(cuentaId);

            // Assert
            Assert.True(resultado.Exitoso);
            var respuesta = (RespuestaSaldo)resultado.Datos;
            Assert.NotNull(respuesta);
            Assert.True(respuesta.Saldo > 0);
        }

        [Fact]
        public void RealizarRetiro_ConSaldoSuficiente_DebeRetornarExitoso()
        {
            // Arrange
            int cuentaId = 1;
            decimal monto = 100;

            // Act
            var resultado = _servicio.RealizarRetiro(cuentaId, monto);

            // Assert
            Assert.True(resultado.Exitoso);
            var respuesta = (RespuestaOperacionConComprobante)resultado.Datos;
            Assert.NotNull(respuesta.Comprobante);
        }

        [Fact]
        public void RealizarRetiro_SinSaldoSuficiente_DebeRetornarError()
        {
            // Arrange
            int cuentaId = 1;
            decimal montoExcesivo = 999999;

            // Act
            var resultado = _servicio.RealizarRetiro(cuentaId, montoExcesivo);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("insuficiente", resultado.Mensaje.ToLower());
        }

        [Fact]
        public void RealizarDeposito_ConMontoValido_DebeRetornarExitoso()
        {
            // Arrange
            int cuentaId = 1;
            decimal monto = 500;

            // Act
            var resultado = _servicio.RealizarDeposito(cuentaId, monto);

            // Assert
            Assert.True(resultado.Exitoso);
            var respuesta = (RespuestaOperacionConComprobante)resultado.Datos;
            Assert.Equal(monto, respuesta.Comprobante.Monto);
        }

        [Fact]
        public void RealizarTransferencia_ACtasDiferentes_DebeRetornarExitoso()
        {
            // Arrange
            int cuentaOrigen = 1;
            int cuentaDestino = 2;
            decimal monto = 100;

            // Act
            var resultado = _servicio.RealizarTransferencia(cuentaOrigen, cuentaDestino, monto);

            // Assert
            Assert.True(resultado.Exitoso);
            var respuesta = (RespuestaOperacionConComprobante)resultado.Datos;
            Assert.NotNull(respuesta.Comprobante);
        }

        [Fact]
        public void RealizarTransferencia_AMismaCuenta_DebeRetornarError()
        {
            // Arrange
            int cuentaId = 1;
            decimal monto = 100;

            // Act
            var resultado = _servicio.RealizarTransferencia(cuentaId, cuentaId, monto);

            // Assert
            Assert.False(resultado.Exitoso);
            Assert.Contains("propia", resultado.Mensaje.ToLower());
        }

        [Fact]
        public void ObtenerHistorial_ConCuentaValida_DebeRetornarLista()
        {
            // Arrange
            int cuentaId = 1;

            // Act
            var resultado = _servicio.ObtenerHistorialTransacciones(cuentaId);

            // Assert
            Assert.True(resultado.Exitoso);
            var transacciones = (List<Transaccion>)resultado.Datos;
            Assert.NotNull(transacciones);
        }
    }
}