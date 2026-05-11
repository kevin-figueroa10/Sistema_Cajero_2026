# MODELOS - PERSONA 2 (CORE)

## Archivo 1: Cajero.Core/Models/TipoCuenta.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Enumeración de tipos de cuenta disponibles.
    /// </summary>
    public enum TipoCuenta
    {
        Ahorros = 1,
        Corriente = 2,
        Nómina = 3
    }
}
```

---

## Archivo 2: Cajero.Core/Models/Cuenta.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa una cuenta bancaria.
    /// </summary>
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public string PIN { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public List<int> TransaccionIds { get; set; } = new List<int>();
    }
}
```

---

## Archivo 3: Cajero.Core/Models/Transaccion.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa una transacción bancaria.
    /// </summary>
    public class Transaccion
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public string Tipo { get; set; } = string.Empty; // Retiro, Depósito, Transferencia
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaHora { get; set; }
        public string NumeroReferencia { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
```

---

## Archivo 4: Cajero.Core/Models/Comprobante.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Representa el comprobante de una transacción.
    /// </summary>
    public class Comprobante
    {
        public string NumeroReferencia { get; set; } = string.Empty;
        public string TipoOperacion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaHora { get; set; }
        public string CuentaOrigen { get; set; } = string.Empty;
        public string? CuentaDestino { get; set; }
        public string Titular { get; set; } = string.Empty;
    }
}
```

---

## Archivo 5: Cajero.Core/Models/ResultadoOperacion.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Clase genérica que representa el resultado de cualquier operación.
    /// Contiene información sobre éxito, mensaje y datos retornados.
    /// </summary>
    public class ResultadoOperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public object? Datos { get; set; }

        public static ResultadoOperacion Éxito(string mensaje, object? datos = null)
        {
            return new ResultadoOperacion { Exitoso = true, Mensaje = mensaje, Datos = datos };
        }

        public static ResultadoOperacion Error(string mensaje)
        {
            return new ResultadoOperacion { Exitoso = false, Mensaje = mensaje };
        }
    }
}
```

---

## Archivo 6: Cajero.Core/Models/RespuestaAutenticacion.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Respuesta del proceso de autenticación.
    /// </summary>
    public class RespuestaAutenticacion
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public TipoCuenta TipoCuenta { get; set; }
    }
}
```

---

## Archivo 7: Cajero.Core/Models/RespuestaSaldo.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Respuesta de consulta de saldo.
    /// </summary>
    public class RespuestaSaldo
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
```

---

## Archivo 8: Cajero.Core/Models/RespuestaOperacionConComprobante.cs

```csharp
namespace Cajero.Core.Models
{
    /// <summary>
    /// Respuesta que incluye el comprobante de la operación realizada.
    /// </summary>
    public class RespuestaOperacionConComprobante
    {
        public Comprobante Comprobante { get; set; } = new Comprobante();
        public decimal NuevoSaldo { get; set; }
    }
}
```

---

## 📋 CÓMO COPIAR

Para cada archivo:
1. Copia el código que está en los bloques de código (entre `)
2. Crea un nuevo archivo en Visual Studio
3. Pega el contenido
4. Guarda con el nombre indicado
5. Sigue con el siguiente

**Orden recomendado:**
1. TipoCuenta.cs
2. Cuenta.cs
3. Transaccion.cs
4. Comprobante.cs
5. ResultadoOperacion.cs
6. RespuestaAutenticacion.cs
7. RespuestaSaldo.cs
8. RespuestaOperacionConComprobante.cs
