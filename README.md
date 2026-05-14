# 💰 Sistema Cajero 2026

**Aplicación de Gestión Bancaria en .NET 10**

---

## 📋 Descripción

Sistema Cajero 2026 es una aplicación completa de gestión bancaria desarrollada en **ASP.NET Core MVC** con **.NET 10**, que proporciona funcionalidades avanzadas para operaciones bancarias, autenticación de usuarios y gestión de transacciones.

El proyecto incluye tres interfaces:
- **Web (MVC)**: Interfaz web moderna con Bootstrap 5
- **Consola (CLI)**: Aplicación de línea de comandos interactiva
- **Backend (Core)**: Lógica de negocio reutilizable

---

## ✨ Características Principales

### 🔐 Autenticación
- Login seguro con número de cuenta y PIN
- Gestión de sesiones
- Logout automático

### 💳 Operaciones Bancarias
- **Retiro**: Con cálculo de comisiones según tipo de cuenta
- **Depósito**: Sin comisiones
- **Transferencia**: Entre cuentas diferentes
- **Consulta de Saldo**: Actualización en tiempo real
- **Historial**: Registro completo de transacciones

### 🏦 Tipos de Cuenta
1. **Ahorros** (1.5% comisión, límite $2,000)
2. **Corriente** (1% comisión, límite $5,000)
3. **Nómina** (0.5% comisión, límite $10,000)

### 📊 Comprobantes
- Generación automática de comprobantes
- Números de referencia únicos
- Detalles completos de cada operación

### ✅ Validaciones
- Montos mínimos y máximos
- Límites por tipo de cuenta
- Verificación de saldo disponible
- Prevención de operaciones inválidas

---

## 🏗️ Arquitectura

### Estructura del Proyecto

```
Sistema Cajero/
├── Cajero.Core/                      # Backend - Lógica de negocio
│   ├── Models/                       # Modelos de datos
│   │   ├── Cuenta.cs
│   │   ├── Transaccion.cs
│   │   ├── Comprobante.cs
│   │   └── ...
│   ├── Interfaces/                   # Contratos
│   │   ├── IRepositorioCuenta.cs
│   │   ├── IRepositorioTransaccion.cs
│   │   └── IServicioCajero.cs
│   ├── Repositories/                 # Acceso a datos
│   │   ├── RepositorioCuenta.cs
│   │   └── RepositorioTransaccion.cs
│   └── Services/                     # Servicios de negocio
│       ├── ServicioCajero.cs
│       └── ConfiguracionCuenta.cs
│
├── Cajero.Web/                       # Frontend - ASP.NET Core MVC
│   ├── Controllers/                  # Controladores
│   │   ├── AutenticacionController.cs
│   │   ├── PrincipalController.cs
│   │   └── HomeController.cs
│   ├── Views/                        # Vistas Razor
│   │   ├── Autenticacion/
│   │   ├── Principal/
│   │   └── Shared/
│   ├── wwwroot/                      # Recursos estáticos
│   └── Program.cs                    # Configuración
│
└── Cajero.Consola/                   # Frontend - Aplicación CLI
    ├── Program.cs                    # Punto de entrada
    ├── MenuPrincipal.cs              # Menú interactivo
    └── Cajero.Consola.csproj
```

### Patrones Implementados

- **Repository Pattern**: Abstracción de datos
- **Service Layer**: Lógica de negocio separada
- **Dependency Injection**: Configuración automática de dependencias
- **MVC Pattern**: Separación de responsabilidades en web
- **DTO Pattern**: Transferencia de datos tipada

---

## 🚀 Requisitos

- **.NET 10 SDK** o superior
- **Visual Studio 2022/2026** (Recomendado)
- **SQL Server** (Opcional - Actualmente usa en-memoria)

---

## 📦 Instalación

### 1. Clonar Repositorio

```bash
git clone https://github.com/kevin-figueroa10/Sistema_Cajero_2026.git
cd "Sistema Cajero"
```

### 2. Restaurar Dependencias

```powershell
dotnet restore
```

### 3. Compilar Proyecto

```powershell
dotnet build
```

---

## ▶️ Uso

### Ejecutar Aplicación Web

```powershell
cd Cajero.Web
dotnet run
```

Accede a: `https://localhost:7000`

### Ejecutar Aplicación Consola

```powershell
cd Cajero.Consola
dotnet run
```

---

## 👥 Cuentas de Prueba

| Número | Nombre | PIN | Saldo | Tipo |
|--------|--------|-----|-------|------|
| 100000000001 | Juan Pérez | 1234 | $5,000 | Ahorros |
| 100000000002 | María García | 5678 | $12,500 | Corriente |
| 100000000003 | Carlos López | 9012 | $25,000 | Nómina |

---

## 🎨 Interfaz Web

### Tecnologías Frontend

- **Bootstrap 5**: Framework CSS responsive
- **Font Awesome**: Iconografía
- **jQuery**: Interactividad
- **HTML5/CSS3**: Marcado y estilos

### Vistas Principales

- **Login**: Autenticación segura
- **Dashboard**: Menú principal
- **Retiro**: Operación de extracción
- **Depósito**: Operación de ingreso
- **Transferencia**: Entre cuentas
- **Historial**: Registro de transacciones
- **Mi Cuenta**: Información de usuario

---

## 🧪 Testing

### Pruebas Unitarias

```powershell
dotnet test
```

### Pruebas Manuales

Usar cuentas de prueba para validar:
- Autenticación
- Operaciones bancarias
- Cálculo de comisiones
- Límites de operación
- Validaciones

---

## 📝 Características Avanzadas

### Seguridad
- Validación de sesiones
- Encriptación de datos sensibles
- Prevención de inyección SQL
- CORS configurado

### Logging
- Registro de operaciones
- Auditoría de transacciones
- Manejo de excepciones

### Rendimiento
- Caché de operaciones
- Optimización de consultas
- Compresión de respuestas

---

## 🔧 Configuración

### appsettings.json (Cajero.Web)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

---

## 📚 Documentación

Para información adicional sobre:
- Guías de desarrollo
- Arquitectura detallada
- API endpoints
- Procedimientos de testing

Consulte la rama `develop` o documentación interna del proyecto.

---

## 🤝 Contribuciones

Este proyecto es parte de un sistema educativo de desarrollo en equipo.

**Equipos:**
- **Persona 1**: Backend Consola
- **Persona 2**: Backend Core
- **Persona 3**: Frontend Views
- **Persona 4**: Frontend Controllers
- **Persona 5**: QA & Testing

---

## 📄 Licencia

Proyecto académico - 2026

---

## 👤 Autor

**Kevin Figueroa**

---

## 📞 Contacto

Para preguntas o sugerencias sobre el proyecto, contacte al desarrollo.

---

## 🎯 Estado del Proyecto

| Componente | Estado |
|-----------|--------|
| Backend Core | ✅ Completo |
| Frontend Web | ✅ Completo |
| Frontend Consola | ✅ Completo |
| Testing | ✅ Implementado |
| Documentación | ✅ Actualizada |

---

## 🚀 Próximas Mejoras

- [ ] Migración a SQL Server
- [ ] Autenticación OAuth2
- [ ] API REST completa
- [ ] Aplicación móvil
- [ ] Reportes avanzados

---

**Última actualización:** Julio 2026

