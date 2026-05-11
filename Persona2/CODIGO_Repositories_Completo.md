# REPOSITORIES - PERSONA 2 (CORE)

## Archivo 1: Cajero.Core/Repositories/RepositorioCuenta.cs

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio de cuentas con almacenamiento en memoria.
    /// Simula una base de datos para propósitos de demostración.
    /// </summary>
    public class RepositorioCuenta : IRepositorioCuenta
    {
        private static List<Cuenta> _cuentas = new List<Cuenta>();
        private static int _proximoId = 1;

        static RepositorioCuenta()
        {
            // Inicializar con cuentas de prueba
            _cuentas = new List<Cuenta>
            {
                new Cuenta
                {
                    Id = _proximoId++,
                    NumeroCuenta = "100000000001",
                    Propietario = "Juan Pérez",
                    PIN = "1234",
                    Saldo = 5000.00m,
                    TipoCuenta = TipoCuenta.Ahorros,
                    FechaCreacion = DateTime.Now.AddYears(-2),
                    FechaExpiracion = DateTime.Now.AddYears(3)
                },
                new Cuenta
                {
                    Id = _proximoId++,
                    NumeroCuenta = "100000000002",
                    Propietario = "María García",
                    PIN = "5678",
                    Saldo = 12500.00m,
                    TipoCuenta = TipoCuenta.Corriente,
                    FechaCreacion = DateTime.Now.AddYears(-1),
                    FechaExpiracion = DateTime.Now.AddYears(4)
                },
                new Cuenta
                {
                    Id = _proximoId++,
                    NumeroCuenta = "100000000003",
                    Propietario = "Carlos López",
                    PIN = "9012",
                    Saldo = 25000.00m,
                    TipoCuenta = TipoCuenta.Nómina,
                    FechaCreacion = DateTime.Now.AddMonths(-6),
                    FechaExpiracion = DateTime.Now.AddYears(2)
                }
            };
        }

        public Cuenta? ObtenerPorId(int id)
        {
            return _cuentas.FirstOrDefault(c => c.Id == id);
        }

        public Cuenta? ObtenerPorNumero(string numeroCuenta)
        {
            return _cuentas.FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
        }

        public List<Cuenta> ObtenerTodas()
        {
            return _cuentas.ToList();
        }

        public void Guardar(Cuenta cuenta)
        {
            cuenta.Id = _proximoId++;
            _cuentas.Add(cuenta);
        }

        public void Actualizar(Cuenta cuenta)
        {
            var cuentaExistente = _cuentas.FirstOrDefault(c => c.Id == cuenta.Id);
            if (cuentaExistente != null)
            {
                cuentaExistente.NumeroCuenta = cuenta.NumeroCuenta;
                cuentaExistente.Propietario = cuenta.Propietario;
                cuentaExistente.PIN = cuenta.PIN;
                cuentaExistente.Saldo = cuenta.Saldo;
                cuentaExistente.TipoCuenta = cuenta.TipoCuenta;
            }
        }

        public void Eliminar(int id)
        {
            var cuenta = _cuentas.FirstOrDefault(c => c.Id == id);
            if (cuenta != null)
            {
                _cuentas.Remove(cuenta);
            }
        }
    }
}
```

---

## Archivo 2: Cajero.Core/Repositories/RepositorioTransaccion.cs

```csharp
using Cajero.Core.Interfaces;
using Cajero.Core.Models;

namespace Cajero.Core.Repositories
{
    /// <summary>
    /// Repositorio de transacciones con almacenamiento en memoria.
    /// Simula una base de datos para propósitos de demostración.
    /// </summary>
    public class RepositorioTransaccion : IRepositorioTransaccion
    {
        private static List<Transaccion> _transacciones = new List<Transaccion>();
        private static int _proximoId = 1;

        public Transaccion? ObtenerPorId(int id)
        {
            return _transacciones.FirstOrDefault(t => t.Id == id);
        }

        public List<Transaccion> ObtenerPorCuenta(int cuentaId)
        {
            return _transacciones
                .Where(t => t.CuentaId == cuentaId)
                .OrderByDescending(t => t.FechaHora)
                .ToList();
        }

        public List<Transaccion> ObtenerTodas()
        {
            return _transacciones.ToList();
        }

        public void Guardar(Transaccion transaccion)
        {
            transaccion.Id = _proximoId++;
            _transacciones.Add(transaccion);
        }

        public void Actualizar(Transaccion transaccion)
        {
            var transaccionExistente = _transacciones.FirstOrDefault(t => t.Id == transaccion.Id);
            if (transaccionExistente != null)
            {
                transaccionExistente.Tipo = transaccion.Tipo;
                transaccionExistente.Monto = transaccion.Monto;
                transaccionExistente.Comision = transaccion.Comision;
                transaccionExistente.MontoTotal = transaccion.MontoTotal;
                transaccionExistente.Descripcion = transaccion.Descripcion;
            }
        }

        public void Eliminar(int id)
        {
            var transaccion = _transacciones.FirstOrDefault(t => t.Id == id);
            if (transaccion != null)
            {
                _transacciones.Remove(transaccion);
            }
        }
    }
}
```

---

## 📋 CÓMO COPIAR

Para cada repositorio:
1. Copia el código entre los bloques ```csharp
2. Crea un nuevo archivo en Visual Studio
3. Pega el contenido
4. Guarda con el nombre indicado
5. Sigue con el siguiente

**Orden:**
1. RepositorioCuenta.cs
2. RepositorioTransaccion.cs
