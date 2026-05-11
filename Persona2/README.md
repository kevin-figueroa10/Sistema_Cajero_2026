# 👤 PERSONA 2 - BACKEND DATA & CORE

**Rol:** Desarrollador Backend (Core)  
**Rama:** `feature-backend`  
**Proyecto Principal:** `Cajero.Core`  
**Tecnología:** C# - Class Library (.NET 10)

---

## 📌 RESPONSABILIDADES

✅ Crear modelos de datos (Cuenta, Transaccion, Comprobante)  
✅ Crear interfaces (IRepositorio, IServicio)  
✅ Crear repositorios (acceso a datos)  
✅ Crear servicios (lógica de negocio)  
✅ Crear configuraciones (limites, comisiones)  

## 🎯 QUÉ DEBES HACER

1. **Crear carpeta:** `Models/`
2. **Crear carpeta:** `Interfaces/`
3. **Crear carpeta:** `Repositories/`
4. **Crear carpeta:** `Services/`
5. **Crear archivos:** Todos los que se indican abajo
6. **No modificar:** Nada del proyecto actual

## 📁 CARPETAS Y ARCHIVOS

```
Cajero.Core/
├── Models/
│   ├── Cuenta.cs
│   ├── Transaccion.cs
│   ├── Comprobante.cs
│   ├── ResultadoOperacion.cs
│   ├── RespuestaAutenticacion.cs
│   ├── RespuestaSaldo.cs
│   ├── RespuestaOperacionConComprobante.cs
│   └── TipoCuenta.cs
├── Interfaces/
│   ├── IRepositorioCuenta.cs
│   ├── IRepositorioTransaccion.cs
│   └── IServicioCajero.cs
├── Repositories/
│   ├── RepositorioCuenta.cs
│   └── RepositorioTransaccion.cs
├── Services/
│   ├── ServicioCajero.cs
│   └── ConfiguracionCuenta.cs
└── Cajero.Core.csproj
```

## 🔗 DEPENDENCIAS

**Requiere:** .NET 10  
**Usa:** Sin dependencias externas (solo System)  

## 📖 PARA COPIAR

Abre archivos en la carpeta `codigo_persona2/` en orden:

1. CODIGO_Models_*.cs (modelos)
2. CODIGO_Interfaces_*.cs (contratos)
3. CODIGO_Repositories_*.cs (datos)
4. CODIGO_Services_*.cs (lógica)

---

## 🚀 PASO 5: ENVIAR CAMBIOS A GITHUB DESDE TERMINAL

Una vez que hayas copiado TODO el código y verificado que compila:

### 5.1 - Ver cambios realizados
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
git status
```

### 5.2 - Agregar todos los cambios
```powershell
git add .
```

### 5.3 - Hacer commit con mensaje descriptivo
```powershell
git commit -m "feat: Implementar Cajero.Core Backend completamente

- Crear 8 modelos de datos (Cuenta, Transaccion, Comprobante, etc.)
- Crear 3 interfaces de contratos (IRepositorio, IServicio)
- Crear 2 repositorios (Cuentas y Transacciones)
- Crear ConfiguracionCuenta con límites y comisiones
- Crear ServicioCajero con lógica de negocio
- Implementar autenticación y validaciones
- Incluir 3 cuentas de prueba precargadas"
```

### 5.4 - Enviar cambios a rama remota
```powershell
git push origin feature-backend
```

### 5.5 - Crear Pull Request desde terminal (OPCIÓN 1: Automático)
```powershell
# Si tienes GitHub CLI instalado:
gh pr create --title "Implementación Backend Core (Persona 2)" `
  --body "Implementación completa de Cajero.Core con:
- 8 modelos de datos
- 3 interfaces de contratos
- 2 repositorios (en memoria)
- Servicios de lógica de negocio
- Configuración de límites y comisiones
- Validaciones completas
- 3 cuentas de prueba" `
  --base develop `
  --head feature-backend
```

### 5.5 - Crear Pull Request desde terminal (OPCIÓN 2: Manual en GitHub)
```powershell
# Si no tienes GitHub CLI, abre manualmente:
start "https://github.com/kevin-figueroa10/Sistema_Cajero_2026/pull/new/feature-backend"
```

### 5.6 - Esperar a que sea revisado
- ✅ Tu PR aparecerá en GitHub
- ✅ El equipo podrá revisar tu código
- ✅ Se fusionará a `develop` cuando esté aprobado

---

**Consulta:** `GUIA_PERSONA_2_BACKEND_DATA.md` para instrucciones completas
