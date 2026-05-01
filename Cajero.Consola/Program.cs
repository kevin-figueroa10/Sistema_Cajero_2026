using Cajero.Core.Interfaces;
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

while (true)
{
    if (cuentaIdAutenticada == null)
    {
        MostrarMenuLogin(ref cuentaIdAutenticada, ref propietario, servicioCajero);
    }
    else
    {
        MostrarMenuPrincipal(ref cuentaIdAutenticada, ref propietario, servicioCajero);
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
        cuentaId = (int)datos.cuentaId;
        propietarioNombre = (string)datos.propietario;
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

void MostrarMenuPrincipal(ref int? cuentaId, ref string propietarioNombre, IServicioCajero servicio)
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
    Console.WriteLine("6. Cerrar Sesión");
    Console.WriteLine("\nSelecciona una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            ConsultarSaldo(cuentaId.Value, servicio);
            break;
        case "2":
            RealizarRetiro(cuentaId.Value, servicio);
            break;
        case "3":
            RealizarDeposito(cuentaId.Value, servicio);
            break;
        case "4":
            RealizarTransferencia(cuentaId.Value, servicio);
            break;
        case "5":
            MostrarHistorial(cuentaId.Value, servicio);
            break;
        case "6":
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
        Console.WriteLine($"Saldo Disponible: ${datos.saldo:N2}");
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

void RealizarRetiro(int cuentaId, IServicioCajero servicio)
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {resultado.Mensaje}");
            var datos = (dynamic)resultado.Datos;
            Console.WriteLine($"Nuevo Saldo: ${datos.saldoNuevo:N2}");
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

void RealizarDeposito(int cuentaId, IServicioCajero servicio)
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ {resultado.Mensaje}");
            var datos = (dynamic)resultado.Datos;
            Console.WriteLine($"Nuevo Saldo: ${datos.saldoNuevo:N2}");
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

void RealizarTransferencia(int cuentaId, IServicioCajero servicio)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════════╗");
    Console.WriteLine("║     REALIZAR TRANSFERENCIA             ║");
    Console.WriteLine("╚════════════════════════════════════════╝\n");

    Console.WriteLine("Cuentas disponibles:");
    Console.WriteLine("1. Cuenta 1001 - Juan García");
    Console.WriteLine("2. Cuenta 1002 - María López");
    Console.WriteLine("3. Cuenta 1003 - Carlos Martínez");
    Console.Write("\nSelecciona cuenta destino: ");

    if (int.TryParse(Console.ReadLine(), out int cuentaDestino) && cuentaDestino >= 1 && cuentaDestino <= 3)
    {
        Console.Write("Monto a Transferir ($): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal monto))
        {
            var resultado = servicio.RealizarTransferencia(cuentaId, cuentaDestino, monto);

            if (resultado.Exitoso)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✓ {resultado.Mensaje}");
                var datos = (dynamic)resultado.Datos;
                Console.WriteLine($"Nuevo Saldo: ${datos.saldoOrigen:N2}");
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
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n✕ Selección inválida.");
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
        var transacciones = (IEnumerable<Cajero.Core.Models.Transaccion>)resultado.Datos;

        if (transacciones.Any())
        {
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
