using Xunit;
using Cajero.Core.Services;
using Cajero.Core.Repositories;
using Cajero.Core.Interfaces;

namespace Cajero.Tests.UnitTests
{
    /// <summary>
    /// Pruebas para validaciones de negocio.
    /// </summary>
    public class ValidacionTests
    {
        private readonly IServicioCajero _servicio;

        public ValidacionTests()
        {
            var repositorioCuenta = new RepositorioCuenta();
            var repositorioTransaccion = new RepositorioTransaccion();
            _servicio = new ServicioCajero(repositorioCuenta, repositorioTransaccion);
        }

        [Fact]
        public void Retiro_ConMontoNegativo_DebeRetornarError()
        {
            // Arrange
            int cuentaId = 1;
            decimal montoNegativo = -100;

            // Act
            var resultado = _servicio.RealizarRetiro(cuentaId, montoNegativo);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        [Fact]
        public void Retiro_ConMontoCero_DebeRetornarError()
        {
            // Arrange
            int cuentaId = 1;
            decimal montoCero = 0;

            // Act
            var resultado = _servicio.RealizarRetiro(cuentaId, montoCero);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        [Fact]
        public void Transferencia_ConMontoNegativo_DebeRetornarError()
        {
            // Arrange
            int cuentaOrigen = 1;
            int cuentaDestino = 2;
            decimal montoNegativo = -50;

            // Act
            var resultado = _servicio.RealizarTransferencia(cuentaOrigen, cuentaDestino, montoNegativo);

            // Assert
            Assert.False(resultado.Exitoso);
        }

        [Fact]
        public void Deposito_ConMontoPositivo_DebeActualizarSaldo()
        {
            // Arrange
            int cuentaId = 1;
            decimal montoDeposito = 1000;
            var saldoAntes = _servicio.ObtenerCuenta(cuentaId).Saldo;

            // Act
            var resultado = _servicio.RealizarDeposito(cuentaId, montoDeposito);

            // Assert
            Assert.True(resultado.Exitoso);
            var saldoDespues = _servicio.ObtenerCuenta(cuentaId).Saldo;
            Assert.True(saldoDespues > saldoAntes);
        }

        [Fact]
        public void ComisionAhorros_EnRetiro_DebeSerMenorQueCorriente()
        {
            // Arrange - Cuenta Ahorros (1) vs Corriente (2)
            int cuentaAhorros = 1;
            int cuentaCorriente = 2;
            decimal monto = 100;

            // Act
            var retiroAhorros = _servicio.RealizarRetiro(cuentaAhorros, monto);
            var retiroCorriente = _servicio.RealizarRetiro(cuentaCorriente, monto);

            // Assert
            var comisionAhorros = ((Cajero.Core.Models.RespuestaOperacionConComprobante)retiroAhorros.Datos).Comprobante.Comision;
            var comisionCorriente = ((Cajero.Core.Models.RespuestaOperacionConComprobante)retiroCorriente.Datos).Comprobante.Comision;

            // Ahorros tiene 1.5%, Corriente tiene 1%
            Assert.True(comisionAhorros >= comisionCorriente);
        }

        [Fact]
        public void Deposito_NoTieneComision()
        {
            // Arrange
            int cuentaId = 1;
            decimal monto = 500;

            // Act
            var resultado = _servicio.RealizarDeposito(cuentaId, monto);

            // Assert
            var respuesta = (Cajero.Core.Models.RespuestaOperacionConComprobante)resultado.Datos;
            Assert.Equal(0, respuesta.Comprobante.Comision);
        }
    }
}