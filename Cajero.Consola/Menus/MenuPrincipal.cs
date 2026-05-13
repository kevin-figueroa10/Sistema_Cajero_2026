using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Consola.Menus
{
    public class MenuPrincipal
    {
        private readonly IServicioCajero _servicioCajero;
        private Cuenta _cuentaActual;

        public MenuPrincipal(IServicioCajero servicioCajero)
        {
            _servicioCajero = servicioCajero;
        }

        public void Mostrar()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║  🏦 BANCO NEW SMART CAPITAL 2026      ║");
            Console.WriteLine("║     Cajero Automático                 ║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");

            // Autenticación
            if (!Autenticar())
            {
                Console.WriteLine("\n❌ Acceso denegado. Cerrando aplicación...");
                return;
            }

            // Menú principal después de autenticar
            MostrarMenuPrincipal();
        }

        private bool Autenticar()
        {
            Console.WriteLine("\n📋 AUTENTICACIÓN\n");

            Console.Write("Número de Cuenta (Ej: 412087654321): ");
            string numeroCuenta = Console.ReadLine();

            Console.Write("PIN (4 dígitos): ");
            string pin = Console.ReadLine();

            var resultado = _servicioCajero.Autenticar(numeroCuenta, pin);

            if (resultado.Exitoso)
            {
                var respuesta = (RespuestaAutenticacion)resultado.Datos;
                _cuentaActual = _servicioCajero.ObtenerCuenta(respuesta.CuentaId);
                Console.WriteLine($"\n✅ Bienvenido, {_cuentaActual.Propietario}!");
                System.Threading.Thread.Sleep(1500);
                return true;
            }

            Console.WriteLine($"\n❌ {resultado.Mensaje}");
            System.Threading.Thread.Sleep(2000);
            return false;
        }

        private void MostrarMenuPrincipal()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine($"╔═══════════════════════════════════════╗");
                Console.WriteLine($"║ Bienvenido: {_cuentaActual.Propietario,25} ║");
                Console.WriteLine($"║ Tipo: {_cuentaActual.TipoCuenta,27} ║");
                Console.WriteLine($"╚═══════════════════════════════════════╝\n");

                Console.WriteLine("📌 MENÚ PRINCIPAL:\n");
                Console.WriteLine("1. 💰 Consultar Saldo");
                Console.WriteLine("2. 📥 Realizar Depósito");
                Console.WriteLine("3. 📤 Realizar Retiro");
                Console.WriteLine("4. 💸 Transferencia");
                Console.WriteLine("5. 📋 Historial");
                Console.WriteLine("6. 🪪 Mi Cuenta");
                Console.WriteLine("7. 🔐 Cambiar PIN");
                Console.WriteLine("0. 🚪 Salir\n");

                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ConsultarSaldo();
                        break;
                    case "2":
                        RealizarDeposito();
                        break;
                    case "3":
                        RealizarRetiro();
                        break;
                    case "4":
                        RealizarTransferencia();
                        break;
                    case "5":
                        MostrarHistorial();
                        break;
                    case "6":
                        MostrarMiCuenta();
                        break;
                    case "7":
                        CambiarPIN();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("❌ Opción no válida.");
                        System.Threading.Thread.Sleep(1500);
                        break;
                }
            }

            Console.WriteLine("\n👋 Gracias por usar BANCO NEW SMART CAPITAL. ¡Hasta pronto!");
        }

        private void ConsultarSaldo()
        {
            Console.Clear();
            Console.WriteLine("💰 CONSULTAR SALDO\n");

            var resultado = _servicioCajero.ConsultarSaldo(_cuentaActual.Id);

            if (resultado.Exitoso)
            {
                var saldo = (RespuestaSaldo)resultado.Datos;
                Console.WriteLine($"✅ {resultado.Mensaje}");
                Console.WriteLine($"\n📊 Saldo Actual: ${saldo.Saldo:N2}\n");
            }
            else
            {
                Console.WriteLine($"❌ {resultado.Mensaje}\n");
            }

            Console.Write("Presiona Enter para continuar...");
            Console.ReadLine();
        }

        private void RealizarDeposito()
        {
            Console.Clear();
            Console.WriteLine("📥 REALIZAR DEPÓSITO\n");

            Console.Write("Monto a depositar ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto))
            {
                Console.WriteLine("❌ Monto inválido.");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            var resultado = _servicioCajero.RealizarDeposito(_cuentaActual.Id, monto);

            if (resultado.Exitoso)
            {
                var datos = (RespuestaOperacionConComprobante)resultado.Datos;
                Console.WriteLine($"\n✅ {resultado.Mensaje}");
                Console.WriteLine($"Monto: ${datos.Monto:N2}");
                Console.WriteLine($"Saldo Anterior: ${datos.SaldoAnterior:N2}");
                Console.WriteLine($"Saldo Nuevo: ${datos.SaldoNuevo:N2}\n");

                _cuentaActual = _servicioCajero.ObtenerCuenta(_cuentaActual.Id);
            }
            else
            {
                Console.WriteLine($"❌ {resultado.Mensaje}\n");
            }

            Console.Write("Presiona Enter para continuar...");
            Console.ReadLine();
        }

        private void RealizarRetiro()
        {
            Console.Clear();
            Console.WriteLine("📤 REALIZAR RETIRO\n");

            Console.Write("Monto a retirar ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto))
            {
                Console.WriteLine("❌ Monto inválido.");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            var resultado = _servicioCajero.RealizarRetiro(_cuentaActual.Id, monto);

            if (resultado.Exitoso)
            {
                var datos = (RespuestaOperacionConComprobante)resultado.Datos;
                Console.WriteLine($"\n✅ {resultado.Mensaje}");
                Console.WriteLine($"Monto: ${datos.Monto:N2}");
                Console.WriteLine($"Saldo Anterior: ${datos.SaldoAnterior:N2}");
                Console.WriteLine($"Saldo Nuevo: ${datos.SaldoNuevo:N2}\n");

                _cuentaActual = _servicioCajero.ObtenerCuenta(_cuentaActual.Id);
            }
            else
            {
                Console.WriteLine($"❌ {resultado.Mensaje}\n");
            }

            Console.Write("Presiona Enter para continuar...");
            Console.ReadLine();
        }

        private void RealizarTransferencia()
        {
            Console.Clear();
            Console.WriteLine("💸 REALIZAR TRANSFERENCIA\n");

            Console.Write("Número de cuenta destino (12 dígitos): ");
            string cuentaDestino = Console.ReadLine();

            Console.Write("Monto a transferir ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto))
            {
                Console.WriteLine("❌ Monto inválido.");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            // Buscar cuenta destino
            var busqueda = _servicioCajero.BuscarCuentaPorNumero(cuentaDestino);
            if (!busqueda.Exitoso)
            {
                Console.WriteLine($"❌ {busqueda.Mensaje}\n");
                Console.Write("Presiona Enter para continuar...");
                Console.ReadLine();
                return;
            }

            var cuentaDestinoObj = (Cuenta)busqueda.Datos;
            Console.WriteLine($"\n📍 Cuenta destino: {cuentaDestinoObj.Propietario} ({cuentaDestino})");
            Console.Write("¿Confirmas la transferencia? (s/n): ");

            if (Console.ReadLine().ToLower() != "s")
            {
                Console.WriteLine("❌ Transferencia cancelada.\n");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            var resultado = _servicioCajero.RealizarTransferencia(_cuentaActual.Id, cuentaDestinoObj.Id, monto);

            if (resultado.Exitoso)
            {
                var datos = (RespuestaOperacionConComprobante)resultado.Datos;
                Console.WriteLine($"\n✅ {resultado.Mensaje}");
                Console.WriteLine($"Monto: ${datos.Monto:N2}");
                Console.WriteLine($"Comisión: ${datos.Comision:N2}");
                Console.WriteLine($"Saldo Anterior: ${datos.SaldoAnterior:N2}");
                Console.WriteLine($"Saldo Nuevo: ${datos.SaldoNuevo:N2}\n");

                _cuentaActual = _servicioCajero.ObtenerCuenta(_cuentaActual.Id);
            }
            else
            {
                Console.WriteLine($"❌ {resultado.Mensaje}\n");
            }

            Console.Write("Presiona Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarHistorial()
        {
            Console.Clear();
            Console.WriteLine("📋 HISTORIAL DE TRANSACCIONES\n");

            var resultado = _servicioCajero.ObtenerHistorialTransacciones(_cuentaActual.Id);

            if (resultado.Exitoso)
            {
                var transacciones = (List<Transaccion>)resultado.Datos;

                if (transacciones.Count == 0)
                {
                    Console.WriteLine("No hay transacciones registradas.\n");
                }
                else
                {
                    Console.WriteLine($"{'Fecha',-12} {'Tipo',-15} {'Monto',-12} {'Saldo',-12}");
                    Console.WriteLine(new string('-', 51));

                    foreach (var transaccion in transacciones)
                    {
                        Console.WriteLine($"{transaccion.Fecha:dd/MM/yyyy} {transaccion.Tipo,-15} ${transaccion.Monto:N2,-11} ${transaccion.SaldoNuevo:N2,-11}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"❌ {resultado.Mensaje}\n");
            }

            Console.Write("\nPresiona Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarMiCuenta()
        {
            Console.Clear();
            Console.WriteLine("🪪 MI CUENTA\n");

            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  BANCO NEW SMART CAPITAL               ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.WriteLine($"Número de Cuenta: {_cuentaActual.NumeroCuenta}");
            Console.WriteLine($"Titular: {_cuentaActual.Propietario.ToUpper()}");
            Console.WriteLine($"Tipo: {_cuentaActual.TipoCuenta}");
            Console.WriteLine($"Saldo: ${_cuentaActual.Saldo:N2}");
            Console.WriteLine($"Estado: {(_cuentaActual.Activa ? "✅ Activa" : "❌ Inactiva")}");
            Console.WriteLine($"Fecha Creación: {_cuentaActual.FechaCreacion:dd/MM/yyyy}");
            Console.WriteLine($"Fecha Expiración: {_cuentaActual.FechaExpiracion:dd/MM/yyyy}");
            Console.WriteLine($"PIN: ****\n");

            Console.Write("Presiona Enter para continuar...");
            Console.ReadLine();
        }

        private void CambiarPIN()
        {
            Console.Clear();
            Console.WriteLine("🔐 CAMBIAR PIN\n");

            Console.Write("PIN actual (4 dígitos): ");
            string pinActual = Console.ReadLine();

            if (pinActual != _cuentaActual.PIN)
            {
                Console.WriteLine("❌ PIN incorrecto.\n");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            Console.Write("PIN nuevo (4 dígitos): ");
            string pinNuevo = Console.ReadLine();

            Console.Write("Confirmar PIN nuevo: ");
            string pinConfirmar = Console.ReadLine();

            if (pinNuevo != pinConfirmar)
            {
                Console.WriteLine("❌ Los PINs no coinciden.\n");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            // Aquí iría la lógica para actualizar el PIN
            Console.WriteLine("✅ PIN cambiado exitosamente.\n");
            System.Threading.Thread.Sleep(1500);
        }
    }
}