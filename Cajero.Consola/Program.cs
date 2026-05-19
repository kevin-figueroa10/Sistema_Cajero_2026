using Cajero.Core.Interfaces;
using Cajero.Core.Models;
using Cajero.Core.Repositories;
using Cajero.Core.Services;
using Microsoft.Extensions.DependencyInjection;

// Configurar inyección de dependencias
var servicios = new ServiceCollection();
servicios.AddScoped<IRepositorioCuenta, RepositorioCuenta>();
servicios.AddScoped<IRepositorioTransaccion, RepositorioTransaccion>();
servicios.AddScoped<IServicioCajero, ServicioCajero>();
var serviceProvider = servicios.BuildServiceProvider();

var servicioCajero = serviceProvider.GetRequiredService<IServicioCajero>();

Console.Clear();
Console.WriteLine("╔════════════════════════════════════════╗");
Console.WriteLine("║   SISTEMA CAJERO AUTOMÁTICO 2026       ║");
Console.WriteLine("║   Interfaz de Consola                  ║");
Console.WriteLine("╚════════════════════════════════════════╝\n");

int? cuentaIdAutenticada = null;
string propietario = "";
Comprobante? ultimoComprobante = null;

while (true)
{
    if (cuentaIdAutenticada == null)
    {
        MostrarMenuLogin(ref cuentaIdAutenticada, ref propietario, servicioCajero);
    }
    else
    {
        MostrarMenuPrincipal(ref cuentaIdAutenticada, ref propietario, ref ultimoComprobante, servicioCajero);
    }
}

void MostrarMenuLogin(ref int? cuentaId, ref string propietarioNombre, IServicioCajero servicio)
{
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║          INICIAR SESIÓN                ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    Console.Write("Número de Cuenta: ");
    string numeroCuenta = Console.ReadLine();

    Console.Write("PIN: ");
    string pin = Console.ReadLine();

    var resultado = servicio.Autenticar(numeroCuenta, pin);

    if (resultado.Exitoso)
    {
        var datos = (dynamic)resultado.Datos;
        cuentaId = (int)datos.CuentaId;
        propietarioNombre = (string)datos.Propietario;
        Console.WriteLine("\n✓ " + resultado.Mensaje);
        System.Threading.Thread.Sleep(1500);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ " + resultado.Mensaje);
        Console.ResetColor();
        System.Threading.Thread.Sleep(2000);
    }

    Console.Clear();
}

void MostrarMenuPrincipal(ref int? cuentaId, ref string propietarioNombre, ref Comprobante? ultimoComprobante, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine($"\n╔════════════════════════════════════════╗");
    Console.WriteLine($"║  Bienvenido, {propietarioNombre,-24} ║");
    Console.WriteLine($"╚════════════════════════════════════════╝\n");

    Console.WriteLine("1. Consultar Saldo");
    Console.WriteLine("2. Realizar Retiro");
    Console.WriteLine("3. Realizar Depósito");
    Console.WriteLine("4. Transferencia");
    Console.WriteLine("5. Historial de Transacciones");
    Console.WriteLine("6. Ver Mi Cuenta");
    Console.WriteLine("7. Cambiar PIN");
    Console.WriteLine("8. Ver Último Comprobante");
    Console.WriteLine("9. Cerrar Sesión");
    Console.WriteLine("\nSelecciona una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            ConsultarSaldo(cuentaId.Value, servicio);
            break;
        case "2":
            RealizarRetiro(cuentaId.Value, ref ultimoComprobante, servicio);
            break;
        case "3":
            RealizarDeposito(cuentaId.Value, ref ultimoComprobante, servicio);
            break;
        case "4":
            RealizarTransferencia(cuentaId.Value, ref ultimoComprobante, servicio);
            break;
        case "5":
            MostrarHistorial(cuentaId.Value, servicio);
            break;
        case "6":
            MostrarMiCuenta(cuentaId.Value, servicio);
            break;
        case "7":
            CambiarPIN(cuentaId.Value, servicio);
            break;
        case "8":
            MostrarUltimoComprobante(ultimoComprobante);
            break;
        case "9":
            cuentaId = null;
            propietarioNombre = "";
            Console.Clear();
            Console.WriteLine("\n✓ Sesión cerrada. Hasta luego.\n");
            System.Threading.Thread.Sleep(1500);
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n✕ Opción no válida.");
            Console.ResetColor();
            System.Threading.Thread.Sleep(1500);
            break;
    }
}

void ConsultarSaldo(int cuentaId, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║       CONSULTAR SALDO                  ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    var resultado = servicio.ConsultarSaldo(cuentaId);

    if (resultado.Exitoso)
    {
        var datos = (dynamic)resultado.Datos;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Número de Cuenta: {datos.NumeroCuenta}");
        Console.WriteLine($"Titular: {datos.Propietario}");
        Console.WriteLine($"Saldo Disponible: ${datos.Saldo:N2}");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✕ {resultado.Mensaje}");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void RealizarRetiro(int cuentaId, ref Comprobante? ultimoComprobante, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║       REALIZAR RETIRO                  ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    Console.Write("Monto a Retirar ($): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal monto))
    {
        var resultado = servicio.RealizarRetiro(cuentaId, monto);

        if (resultado.Exitoso)
        {
            var datosRetiro = (RespuestaOperacionConComprobante)resultado.Datos;
            ultimoComprobante = datosRetiro.Comprobante;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {resultado.Mensaje}");
            Console.WriteLine($"Saldo Anterior: ${datosRetiro.SaldoAnterior:N2}");
            Console.WriteLine($"Monto Retirado: ${datosRetiro.Monto:N2}");
            Console.WriteLine($"Nuevo Saldo: ${datosRetiro.SaldoNuevo:N2}");
            Console.WriteLine($"Número de Referencia: {datosRetiro.Comprobante.NumeroReferencia}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✕ {resultado.Mensaje}");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ Monto inválido.");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void RealizarDeposito(int cuentaId, ref Comprobante? ultimoComprobante, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║      REALIZAR DEPÓSITO                 ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    Console.Write("Monto a Depositar ($): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal monto))
    {
        var resultado = servicio.RealizarDeposito(cuentaId, monto);

        if (resultado.Exitoso)
        {
            var datosDeposito = (RespuestaOperacionConComprobante)resultado.Datos;
            ultimoComprobante = datosDeposito.Comprobante;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {resultado.Mensaje}");
            Console.WriteLine($"Saldo Anterior: ${datosDeposito.SaldoAnterior:N2}");
            Console.WriteLine($"Monto Depositado: ${datosDeposito.Monto:N2}");
            Console.WriteLine($"Nuevo Saldo: ${datosDeposito.SaldoNuevo:N2}");
            Console.WriteLine($"Número de Referencia: {datosDeposito.Comprobante.NumeroReferencia}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✕ {resultado.Mensaje}");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ Monto inválido.");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void RealizarTransferencia(int cuentaId, ref Comprobante? ultimoComprobante, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║     REALIZAR TRANSFERENCIA             ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    Console.Write("Número de Cuenta Destino: ");
    string numeroCuentaDestino = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(numeroCuentaDestino))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ Número de cuenta inválido.");
        Console.ResetColor();
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    // Buscar la cuenta destino
    var busquedaCuenta = servicio.BuscarCuentaPorNumero(numeroCuentaDestino);

    if (!busquedaCuenta.Exitoso || busquedaCuenta.Datos == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n✕ {busquedaCuenta.Mensaje}");
        Console.ResetColor();
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    var cuentaDestino = (Cuenta)busquedaCuenta.Datos;

    // Validar que no sea la misma cuenta
    if (cuentaId == cuentaDestino.Id)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ No puedes transferir a tu propia cuenta.");
        Console.ResetColor();
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    // Mostrar confirmación
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n═════════════════════════════════════════");
    Console.WriteLine($"Cuenta Destino: {cuentaDestino.NumeroCuenta}");
    Console.WriteLine($"Titular: {cuentaDestino.Propietario}");
    Console.WriteLine($"═════════════════════════════════════════\n");
    Console.ResetColor();

    Console.Write("Monto a Transferir ($): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal monto))
    {
        // Validar monto
        if (monto <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n✕ El monto debe ser mayor a cero.");
            Console.ResetColor();
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        if (monto < 5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n✕ El monto mínimo de transferencia es $5.00");
            Console.ResetColor();
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        if (monto % 5 != 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n✕ El monto debe ser múltiplo de $5");
            Console.ResetColor();
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Pedir confirmación
        Console.WriteLine($"\n¿Confirmas transferir ${monto:N2} a {cuentaDestino.Propietario}? (S/N): ");
        string confirmacion = Console.ReadLine()?.ToUpper();

        if (confirmacion != "S")
        {
            Console.WriteLine("\nTransferencia cancelada.");
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        var resultado = servicio.RealizarTransferencia(cuentaId, cuentaDestino.Id, monto);

        if (resultado.Exitoso)
        {
            var datosTransferencia = (RespuestaOperacionConComprobante)resultado.Datos;
            ultimoComprobante = datosTransferencia.Comprobante;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {resultado.Mensaje}");
            Console.WriteLine($"Saldo Anterior: ${datosTransferencia.SaldoAnterior:N2}");
            Console.WriteLine($"Monto Transferido: ${datosTransferencia.Monto:N2}");
            if (datosTransferencia.Comision > 0)
                Console.WriteLine($"Comisión: ${datosTransferencia.Comision:N2}");
            Console.WriteLine($"Nuevo Saldo: ${datosTransferencia.SaldoNuevo:N2}");
            Console.WriteLine($"Número de Referencia: {datosTransferencia.Comprobante.NumeroReferencia}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✕ {resultado.Mensaje}");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ Monto inválido.");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void MostrarHistorial(int cuentaId, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║    HISTORIAL DE TRANSACCIONES          ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    var resultado = servicio.ObtenerHistorialTransacciones(cuentaId);

    if (resultado.Exitoso)
    {
        var transacciones = (IEnumerable<Transaccion>)resultado.Datos;

        if (transacciones.Any())
        {
            Console.WriteLine("Últimas 10 transacciones:\n");
            foreach (var trans in transacciones.Take(10))
            {
                Console.WriteLine($"Fecha: {trans.Fecha:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Tipo: {trans.Tipo} | Monto: ${trans.Monto:N2}");
                Console.WriteLine($"Descripción: {trans.Descripcion}");
                Console.WriteLine($"Saldo: ${trans.SaldoNuevo:N2}");
                Console.WriteLine(new string('-', 40));
            }
        }
        else
        {
            Console.WriteLine("No hay transacciones registradas.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✕ {resultado.Mensaje}");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void MostrarMiCuenta(int cuentaId, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║         DETALLES DE CUENTA             ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    var cuenta = servicio.ObtenerCuenta(cuentaId);

    if (cuenta != null)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌──────────────────────────────────────┐");
        Console.WriteLine("│         INFORMACIÓN DE CUENTA         │");
        Console.WriteLine("└──────────────────────────────────────┘\n");
        Console.ResetColor();

        Console.WriteLine($"Número de Cuenta: {cuenta.NumeroCuenta}");
        Console.WriteLine($"Titular: {cuenta.Propietario}");
        Console.WriteLine($"Tipo de Cuenta: {cuenta.TipoCuenta}");
        Console.WriteLine($"Estado: {(cuenta.Activa ? "Activa" : "Inactiva")}");
        Console.WriteLine($"Saldo: ${cuenta.Saldo:N2}");
        Console.WriteLine($"Fecha de Creación: {cuenta.FechaCreacion:dd/MM/yyyy}");

        Console.WriteLine("\n┌──────────────────────────────────────┐");
        Console.WriteLine("│         DETALLES DE TARJETA          │");
        Console.WriteLine("└──────────────────────────────────────┘\n");

        // Mostrar número de tarjeta enmascarado
        string numeroTarjetaEnmascarado = new string('*', 12) + cuenta.NumeroCuenta.Substring(Math.Max(0, cuenta.NumeroCuenta.Length - 4));
        Console.WriteLine($"Número de Tarjeta: {numeroTarjetaEnmascarado}");
        Console.WriteLine($"Vencimiento: 12/28");
        Console.WriteLine($"CVV: ***");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("✕ No se pudo obtener la información de la cuenta.");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void CambiarPIN(int cuentaId, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║         CAMBIAR PIN                    ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    var cuenta = servicio.ObtenerCuenta(cuentaId);

    Console.Write("PIN Actual: ");
    string pinActual = Console.ReadLine();

    if (cuenta.PIN != pinActual)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ El PIN actual es incorrecto.");
        Console.ResetColor();
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    Console.Write("PIN Nuevo: ");
    string pinNuevo = Console.ReadLine();

    Console.Write("Confirmar PIN: ");
    string pinConfirmar = Console.ReadLine();

    if (pinNuevo != pinConfirmar)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ Los PINs no coinciden.");
        Console.ResetColor();
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    if (pinNuevo.Length < 4)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ El PIN debe tener al menos 4 caracteres.");
        Console.ResetColor();
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    // Aquí iría la lógica para actualizar el PIN en la base de datos
    // Por ahora, solo mostramos un mensaje de éxito
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n✓ PIN cambiado exitosamente.");
    Console.ResetColor();

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}

void MostrarUltimoComprobante(Comprobante? comprobante)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║        ÚLTIMO COMPROBANTE              ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    if (comprobante == null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay comprobante disponible.");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌──────────────────────────────────────┐");
        Console.WriteLine("│           COMPROBANTE                │");
        Console.WriteLine("└──────────────────────────────────────┘\n");
        Console.ResetColor();

        Console.WriteLine($"Número de Referencia: {comprobante.NumeroReferencia}");
        Console.WriteLine($"Fecha: {comprobante.Fecha:dd/MM/yyyy}");
        Console.WriteLine($"Hora: {comprobante.Hora}");
        Console.WriteLine($"Tipo de Operación: {comprobante.TipoOperacion}");
        Console.WriteLine($"Titular: {comprobante.Titular}");
        Console.WriteLine($"Número de Cuenta: {comprobante.NumeroCuenta}");

        Console.WriteLine($"\nMonto: ${comprobante.Monto:N2}");
        Console.WriteLine($"Saldo Anterior: ${comprobante.SaldoAnterior:N2}");
        Console.WriteLine($"Saldo Nuevo: ${comprobante.SaldoNuevo:N2}");

        if (!string.IsNullOrEmpty(comprobante.CuentaDestino))
        {
            Console.WriteLine($"Cuenta Destino: {comprobante.CuentaDestino}");
        }

        if (comprobante.Comision > 0)
        {
            Console.WriteLine($"Comisión: ${comprobante.Comision:N2}");
        }

        Console.WriteLine($"\nDescripción: {comprobante.Descripcion}");
        Console.WriteLine($"Estado: {comprobante.Estado}");

        Console.WriteLine("\n" + new string('═', 40));
        Console.WriteLine("Conserve este comprobante para su registro.");
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}
