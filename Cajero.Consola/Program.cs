using Cajero.Core.Interfaces;
using Cajero.Core.Repositories;
using Cajero.Core.Services;
using Cajero.Consola.Menus;

namespace Cajero.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configurar inyección de dependencias
            var repositorioCuenta = new RepositorioCuenta();
            var repositorioTransaccion = new RepositorioTransaccion();
            var servicioCajero = new ServicioCajero(repositorioCuenta, repositorioTransaccion);

            // Crear y mostrar menú principal
            var menuPrincipal = new MenuPrincipal(servicioCajero);
            menuPrincipal.Mostrar();
        }
    }
}
