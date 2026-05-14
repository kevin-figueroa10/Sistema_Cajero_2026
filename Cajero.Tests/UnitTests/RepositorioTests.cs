using Xunit;
using Cajero.Core.Repositories;
using Cajero.Core.Models;

namespace Cajero.Tests.UnitTests
{
    /// <summary>
    /// Pruebas unitarias para los repositorios de datos.
    /// </summary>
    public class RepositorioTests
    {
        [Fact]
        public void RepositorioCuenta_ObtenerPorNumero_ConCuentaValida_DebeRetornarCuenta()
        {
            // Arrange
            var repositorio = new RepositorioCuenta();
            string numeroCuenta = "100000000001";

            // Act
            var resultado = repositorio.ObtenerPorNumeroCuenta(numeroCuenta);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("100000000001", resultado.NumeroCuenta);
            Assert.Equal("Juan Pérez", resultado.Propietario);
        }

        [Fact]
        public void RepositorioCuenta_ObtenerPorNumero_ConCuentaInvalida_DebeRetornarNull()
        {
            // Arrange
            var repositorio = new RepositorioCuenta();
            string numeroCuenta = "999999999999";

            // Act
            var resultado = repositorio.ObtenerPorNumeroCuenta(numeroCuenta);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void RepositorioCuenta_ObtenerPorId_ConIdValido_DebeRetornarCuenta()
        {
            // Arrange
            var repositorio = new RepositorioCuenta();
            int id = 1;

            // Act
            var resultado = repositorio.ObtenerPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
        }

        [Fact]
        public void RepositorioCuenta_ObtenerTodas_DebeRetornarAlMenosTresCuentas()
        {
            // Arrange
            var repositorio = new RepositorioCuenta();

            // Act
            var resultado = repositorio.ObtenerTodas();

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Count() >= 3, "Debería haber al menos 3 cuentas de prueba");
        }

        [Fact]
        public void RepositorioCuenta_Actualizar_DebeActualizarSaldo()
        {
            // Arrange
            var repositorio = new RepositorioCuenta();
            var cuenta = repositorio.ObtenerPorId(1);
            var saldoOriginal = cuenta.Saldo;
            cuenta.Saldo = 10000;

            // Act
            repositorio.Actualizar(cuenta);
            var cuentaActualizada = repositorio.ObtenerPorId(1);

            // Assert
            Assert.Equal(10000, cuentaActualizada.Saldo);
            Assert.NotEqual(saldoOriginal, cuentaActualizada.Saldo);
        }

        [Fact]
        public void RepositorioTransaccion_ObtenerPorCuenta_DebeRetornarTransacciones()
        {
            // Arrange
            var repositorio = new RepositorioTransaccion();
            int cuentaId = 1;

            // Act
            var resultado = repositorio.ObtenerPorCuenta(cuentaId);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Transaccion>>(resultado);
        }
    }
}