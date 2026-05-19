# 🏦 Sistema Cajero Automático - New Smart Capital

## Descripción

Sistema completo de cajero automático desarrollado en **.NET 10** con arquitectura en capas. Proporciona funcionalidades de autenticación, consulta de saldos, retiros, depósitos, transferencias, historial de transacciones y más.

## 🏗️ Arquitectura

El proyecto está organizado en 4 componentes principales:

### 1. **Cajero.Core** - Capa de Lógica de Negocio
- **Interfaces**: Contratos de servicios y repositorios
- **Models**: Modelos de dominio (Cuenta, Transacción, etc.)
- **Services**: Implementación de la lógica del cajero
- **Repositories**: Acceso a datos en memoria

### 2. **Cajero.Web** - Aplicación ASP.NET Core MVC
- **Controllers**: AutenticacionController, PrincipalController
- **Views**: Interfaz de usuario con Razor
- Autenticación basada en sesiones
- Manejo de operaciones bancarias

### 3. **Cajero.Consola** - Aplicación de Consola
- Interfaz de línea de comandos
- Funcionalidades idénticas a la web
- Menús interactivos con emojis
- Perfecta para pruebas rápidas

### 4. **Cajero.Tests** - Pruebas Unitarias
- Proyecto xUnit
- Validación de operaciones
- Integración continua

🛠️ Tecnologías y Características
| Tecnología                 | Versión       | Uso                              |
|----------------------------|---------------|----------------------------------|
| .NET SDK                   | 10.0          | Runtime y compilación            |
| C#                         | 12+           | Lenguaje principal               |
| ASP.NET Core               | 10.0          | Web Framework                    |
| Razor Pages                | 10.0          | Views HTML con lógica            |
| Session Management         | 10.0          | Gestión de sesiones de usuario   |
| Inyección de Dependencias  | 10.0          | DI Container nativo              |
| Xunit                      | 2.9.3         | Testing unitario                 |
| Code Coverage              | Coverlet 6.0.4| Cobertura de código              |

## ✨ Características

### 🔐 Autenticación
- Login con número de cuenta y PIN
- Gestión de sesiones
- Logout seguro

### 💰 Operaciones Bancarias
- ✅ Consultar Saldo
- ✅ Retiros con validación
- ✅ Depósitos
- ✅ Transferencias entre cuentas
- ✅ Historial de transacciones
- ✅ Cambio de PIN
- ✅ Ver información de cuenta
- ✅ Comprobantes de operaciones

### 📊 Transacciones
- Registro automático de todas las operaciones
- Historial detallado por cuenta
- Trazabilidad completa

## 🚀 Cómo Usar

### Requisitos
- .NET 10 SDK
- Visual Studio o VS Code
- PowerShell/Terminal

### Ejecución Web
\\\ash
cd Cajero.Web
dotnet run
\\\
Accede a: \http://localhost:5000\ o \https://localhost:5001\

### Ejecución Consola
\\\ash
cd Cajero.Consola
dotnet run
\\\

### Ejecutar Tests
\\\ash
dotnet test Cajero.Tests
\\\

## 👤 Cuentas de Prueba

| Número de Cuenta | PIN | Propietario | Saldo Inicial | Tipo |
|---|---|---|---|---|
| 412087654321 | 8475 | Juan García López | \,750.50 | Corriente |
| 412087654322 | 5829 | María López Rodríguez | \,400.75 | Ahorro |
| 412087654323 | 9403 | Carlos Martínez González | \,600.00 | Corriente |

## 📁 Estructura del Proyecto

\\\
Sistema Cajero/
├── Cajero.Core/
│   ├── Interfaces/
│   ├── Models/
│   ├── Services/
│   └── Repositories/
├── Cajero.Web/
│   ├── Controllers/
│   ├── Views/
│   ├── Properties/
│   └── Program.cs
├── Cajero.Consola/
│   └── Program.cs
├── Cajero.Tests/
│   └── *Tests.cs
└── README.md

## 📝 Notas de Desarrollo

- El sistema usa **data en memoria** para simplificar pruebas
- Ideal para ambiente de desarrollo y testing
- Pronto: Integración con base de datos SQL

## 👨‍💼 Equipo

Proyecto desarrollado para **Sistema Cajero Automático 2026**
José Javier Aguilar Amaya
Dimas Emanuel Benítez Mejía
Dana Paola Burgos Escobar 
Kevin Isaac Figueroa Calderón 
Manuel De Jesús Mejía Rivera 


## 📄 Licencia

© 2026 New Smart Capital. Todos los derechos reservados.
