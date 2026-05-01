# Guía de Desarrollo

## Instalación del Entorno de Desarrollo

### 1. Requisitos
- Visual Studio 2022 Community o Enterprise
- .NET 10 SDK
- Git

### 2. Configuración Inicial

```bash
# Clonar repositorio
git clone https://github.com/kevin-figueroa10/Sistema_Cajero_2026.git
cd "Sistema Cajero"

# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar tests (si existen)
dotnet test
```

### 3. Ejecutar en Desarrollo

```bash
# Desde la carpeta del proyecto
cd Cajero.Web
dotnet run
```

La aplicación estará disponible en:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

### 4. Estructura de Carpetas del Proyecto

```
Cajero.Core/
├── Models/          # Entidades del dominio
├── Interfaces/      # Contratos
├── Services/        # Lógica de negocio
└── Repositories/    # Acceso a datos

Cajero.Web/
├── Controllers/     # Manejadores de solicitudes
├── Views/          # Vistas Razor
├── wwwroot/        # Archivos estáticos
└── Program.cs      # Configuración principal

Cajero.Consola/
└── Program.cs      # Interfaz de consola
```

### 5. Flujo de Trabajo Git

```bash
# Crear rama de feature
git checkout -b feature/nueva-funcionalidad

# Hacer cambios y commits
git add .
git commit -m "Descripción clara del cambio"

# Subir cambios
git push origin feature/nueva-funcionalidad

# Crear Pull Request en GitHub
# Esperar revisión y merge a develop
```

### 6. Convenciones de Código

#### Nombres
- **Clases**: PascalCase (ej: `ServicioCajero`)
- **Métodos**: PascalCase (ej: `RealizarRetiro`)
- **Variables**: camelCase (ej: `numeroCuenta`)
- **Constantes**: UPPER_CASE (ej: `TIMEOUT_SESION`)

#### Comentarios XML (Documentation)
```csharp
/// <summary>
/// Autentica un usuario validando número de cuenta y PIN.
/// </summary>
public ResultadoOperacion Autenticar(string numeroCuenta, string pin)
{
    // implementación
}
```

#### Organización de Imports
1. System
2. System.* (LINQ, Collections, etc)
3. Cajero.*
4. Otras librerías

### 7. Testing Manual

**Casos de Prueba Esenciales:**

1. **Login**
   - [ ] Login con credenciales válidas
   - [ ] Login con cuenta no existe
   - [ ] Login con PIN incorrecto

2. **Operaciones**
   - [ ] Consultar saldo
   - [ ] Retiro con saldo suficiente
   - [ ] Retiro sin saldo suficiente
   - [ ] Depósito positivo
   - [ ] Transferencia exitosa
   - [ ] Transferencia a misma cuenta

3. **Historial**
   - [ ] Mostrar todas las transacciones
   - [ ] Orden descendente por fecha

4. **Session**
   - [ ] Session timeout después de 30 minutos
   - [ ] Logout exitoso
   - [ ] Redirect al login sin sesión

### 8. Estructura de Respuestas

Todas las operaciones retornan `ResultadoOperacion`:

**Exitosa:**
```json
{
  "exitoso": true,
  "mensaje": "Operación completada",
  "codigo": "SUCCESS",
  "datos": { /* objeto con datos */ }
}
```

**Error:**
```json
{
  "exitoso": false,
  "mensaje": "Saldo insuficiente",
  "codigo": "SALDO_INSUFICIENTE",
  "datos": null
}
```

### 9. Mejoras Recomendadas

#### Corto Plazo
- Agregar validación de formato de cuenta
- Implementar logging
- Agregar confirmación de transacciones

#### Mediano Plazo
- Migrar a SQL Server
- Implementar autenticación OAuth2
- Agregar pruebas unitarias (xUnit)

#### Largo Plazo
- API REST
- Mobile App
- Microservicios

### 10. Solución de Problemas Comunes

**Error: "El tipo o el nombre del espacio de nombres 'Core' no existe"**
- Verificar referencias en `.csproj`
- Ejecutar `dotnet restore`

**Error: "Connection refused en puerto 5001"**
- Verificar que el puerto esté disponible
- Cambiar puerto en `launchSettings.json`

**Error: "Session data is lost"**
- Verificar que Session esté configurado en `Program.cs`
- Comprobar que `@RenderBody()` esté en `_Layout.cshtml`

### 11. Performance

- **En Memoria**: Perfecto para desarrollo
- **Producción**: Usar SQL Server + caché distribuido
- **Escalabilidad**: Considerar arquitectura de microservicios

### 12. Documentación de Código

Toda clase pública debe tener documentación XML:
```csharp
/// <summary>Breve descripción</summary>
/// <param name="parametro">Descripción del parámetro</param>
/// <returns>Descripción del retorno</returns>
/// <exception cref="ArgumentNullException">Cuando parametro es null</exception>
public void MiMetodo(string parametro)
{
}
```

---

**Última actualización:** 2026
**Responsable:** Equipo de Desarrollo
