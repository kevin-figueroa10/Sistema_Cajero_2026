# INTERFACES - PERSONA 2 (CORE)

## Archivo 1: Cajero.Core/Interfaces/IRepositorioCuenta.cs

```csharp
using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de cuentas.
    /// Define operaciones CRUD para cuentas bancarias.
    /// </summary>
    public interface IRepositorioCuenta
    {
        Cuenta? ObtenerPorId(int id);
        Cuenta? ObtenerPorNumero(string numeroCuenta);
        List<Cuenta> ObtenerTodas();
        void Guardar(Cuenta cuenta);
        void Actualizar(Cuenta cuenta);
        void Eliminar(int id);
    }
}
```

---

## Archivo 2: Cajero.Core/Interfaces/IRepositorioTransaccion.cs

```csharp
using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de transacciones.
    /// Define operaciones CRUD para transacciones.
    /// </summary>
    public interface IRepositorioTransaccion
    {
        Transaccion? ObtenerPorId(int id);
        List<Transaccion> ObtenerPorCuenta(int cuentaId);
        List<Transaccion> ObtenerTodas();
        void Guardar(Transaccion transaccion);
        void Actualizar(Transaccion transaccion);
        void Eliminar(int id);
    }
}
```

---

## Archivo 3: Cajero.Core/Interfaces/IServicioCajero.cs

```csharp
using Cajero.Core.Models;

namespace Cajero.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio principal del cajero.
    /// Define operaciones de negocio (autenticación, operaciones, etc).
    /// </summary>
    public interface IServicioCajero
    {
        // Autenticación
        ResultadoOperacion Autenticar(string numeroCuenta, string pin);

        // Consultas
        Cuenta? ObtenerCuenta(int cuentaId);
        ResultadoOperacion ConsultarSaldo(int cuentaId);
        ResultadoOperacion ObtenerHistorialTransacciones(int cuentaId);
        ResultadoOperacion BuscarCuentaPorNumero(string numeroCuenta);

        // Operaciones
        ResultadoOperacion RealizarRetiro(int cuentaId, decimal monto);
        ResultadoOperacion RealizarDeposito(int cuentaId, decimal monto);
        ResultadoOperacion RealizarTransferencia(int cuentaIdOrigen, int cuentaIdDestino, decimal monto);

        // Configuración
        ResultadoOperacion ActualizarPIN(int cuentaId, string pinActual, string pinNuevo);
    }
}
```

---

## 📋 CÓMO COPIAR

Para cada interfaz:
1. Copia el código entre los bloques ```csharp
2. Crea un nuevo archivo en Visual Studio
3. Pega el contenido
4. Guarda con el nombre indicado
5. Sigue con la siguiente

**Orden:**
1. IRepositorioCuenta.cs
2. IRepositorioTransaccion.cs
3. IServicioCajero.cs
