# 🚀 COMANDOS PARA EJECUTAR - BANCO NEW SMART CAPITAL

**Versión:** 2026.1.1  
**Entorno:** Visual Studio Community 2026  
**.NET:** 10.0.203  
**Directorio:** `C:\Users\figue\Downloads\Sistema Cajero`

---

## 📋 COMANDOS PRINCIPALES

### **1. EJECUTAR EN DESARROLLO (Con Hot Reload)**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet watch run --project Cajero.Web
```
**Qué hace:**
- ✅ Inicia la aplicación en modo debug
- ✅ Recarga automática al cambiar archivos (.cshtml, .cs, etc.)
- ✅ Más rápido para desarrollo
- ✅ URL: `https://localhost:5001` o `http://localhost:5000`

**Ventajas:**
- 🔥 Hot reload automático
- 🐛 Acceso a debug completo
- 📊 Información detallada de errores
- ⚡ Ideal para desarrollo

---

### **2. EJECUTAR EN MODO NORMAL (Sin Hot Reload)**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet run --project Cajero.Web
```
**Qué hace:**
- ✅ Ejecuta la aplicación una sola vez
- ✅ No se reinicia automáticamente
- ✅ Más estable que watch
- ✅ URL: `https://localhost:5001` o `http://localhost:5000`

**Cuándo usarlo:**
- ✅ Cuando quieres probar sin recompilaciones
- ✅ Para testing prolongado
- ✅ Cuando watch causes problemas

---

### **3. COMPILAR SIN EJECUTAR**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet build
```
**Qué hace:**
- ✅ Solo compila el código
- ✅ No ejecuta la aplicación
- ✅ Verifica que todo esté correcto
- ✅ Más rápido que `run`

**Ventajas:**
- ✅ Detecta errores antes de ejecutar
- ✅ No inicia un servidor
- ✅ Ideal para CI/CD

---

### **4. COMPILAR CON CONFIGURACIÓN RELEASE**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet build -c Release
```
**Qué hace:**
- ✅ Compila en modo Release (optimizado)
- ✅ Mejor rendimiento
- ✅ Tamaño más pequeño
- ✅ Sin símbolos de debug

**Cuándo usarlo:**
- ✅ Antes de desplegar a producción
- ✅ Para medir rendimiento real
- ✅ Para crear build final

---

### **5. EJECUTAR EN RELEASE**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet run --project Cajero.Web -c Release
```
**Qué hace:**
- ✅ Compila en Release y ejecuta
- ✅ Máximo rendimiento
- ✅ Sin información de debug
- ✅ Simula entorno de producción

---

### **6. LIMPIAR Y RECOMPILAR (Clean Build)**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet clean
dotnet build
```
**Qué hace:**
- ✅ Elimina carpetas `bin/` y `obj/`
- ✅ Recompila desde cero
- ✅ Resuelve problemas de cache

**Cuándo usarlo:**
- ✅ Cuando hay errores raros de compilación
- ✅ Cuando cambias configuración global
- ✅ Si el hot reload falla

---

## 🧪 COMANDOS PARA TESTING

### **7. EJECUTAR TESTS UNITARIOS**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet test
```
**Qué hace:**
- ✅ Ejecuta todos los tests del proyecto
- ✅ Muestra resultado (Pass/Fail)
- ✅ Genera reporte de cobertura

---

### **8. EJECUTAR TESTS CON VERBOSIDAD**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet test -v normal
```
**Opciones de verbosidad:**
- `quiet` - Solo resultado final
- `minimal` - Información básica
- `normal` - Detalles normales (recomendado)
- `detailed` - Muy detallado
- `diagnostic` - Todo incluido

---

### **9. EJECUTAR UN TEST ESPECÍFICO**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet test --filter "FullyQualifiedName~NombreDelTest"
```

---

## 📦 COMANDOS DE GESTIÓN DE PAQUETES

### **10. RESTAURAR DEPENDENCIAS**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet restore
```
**Qué hace:**
- ✅ Descarga todos los NuGet packages
- ✅ Verifica versiones compatibles
- ✅ Resuelve dependencias

**Cuándo usarlo:**
- ✅ Después de clonar el repositorio
- ✅ Si falta carpeta `.nuget`
- ✅ Después de cambiar .csproj

---

### **11. AGREGAR PAQUETE NUGET**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet add Cajero.Web package NombreDelPaquete
```
**Ejemplo:**
```powershell
dotnet add Cajero.Web package Microsoft.AspNetCore.Identity --version 10.0.0
```

---

### **12. ACTUALIZAR PAQUETE NUGET**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet package update -p Cajero.Web
```

---

## 🔍 COMANDOS DE DIAGNÓSTICO

### **13. VER INFORMACIÓN DEL PROYECTO**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet sln list
```
**Qué hace:**
- ✅ Lista todos los proyectos en la solución
- ✅ Muestra rutas y tipos

---

### **14. VERIFICAR VERSIÓN DE .NET**
```powershell
dotnet --version
```
**Resultado:** `10.0.203`

---

### **15. LISTAR TODOS LOS PROYECTOS DEL SDK**
```powershell
dotnet --list-sdks
```

---

## 🌐 COMANDOS CON PUERTOS ESPECÍFICOS

### **16. EJECUTAR EN PUERTO PERSONALIZADO**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet run --project Cajero.Web -- --urls "http://localhost:7000;https://localhost:7001"
```

**Qué hace:**
- ✅ Ejecuta en puerto 7000 (HTTP) y 7001 (HTTPS)
- ✅ Acceso por: `http://localhost:7000`

---

### **17. EJECUTAR SOLO HTTP (Sin HTTPS)**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
dotnet run --project Cajero.Web -- --urls "http://localhost:5000"
```

---

## 📝 GIT COMMANDS (Relacionados)

### **18. VER RAMA ACTUAL**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
git branch -a
```
**Estado actual:** `feature-arquitectura`

---

### **19. HACER COMMIT Y PUSH**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
git add -A
git commit -m "tu mensaje"
git push origin feature-arquitectura
```

---

### **20. VER ESTADO DE CAMBIOS**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero"
git status
```

---

## ⚡ COMANDOS RÁPIDOS (Copy-Paste)

### **DESARROLLO RÁPIDO (Recomendado)**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero" && dotnet watch run --project Cajero.Web
```
✅ **Ejecutar → Modificar → Recargar automático**

---

### **TESTING RÁPIDO**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero" && dotnet build && dotnet test
```
✅ **Compilar → Ejecutar tests**

---

### **CLEAN BUILD RÁPIDO**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero" && dotnet clean && dotnet build
```
✅ **Limpiar todo → Recompilar**

---

### **BUILD PARA PRODUCCIÓN**
```powershell
cd "C:\Users\figue\Downloads\Sistema Cajero" && dotnet build -c Release && dotnet run --project Cajero.Web -c Release
```
✅ **Compilar Release → Ejecutar**

---

## 📊 TABLA COMPARATIVA

| Comando | Velocidad | Debug | Hot Reload | Uso |
|---------|-----------|-------|-----------|-----|
| `dotnet watch run` | 🔄 Lento* | ✅ Sí | ✅ Sí | **Desarrollo** |
| `dotnet run` | ⚡ Normal | ✅ Sí | ❌ No | Testing |
| `dotnet build` | ⚡⚡ Rápido | ❌ No | ❌ No | Verificación |
| `dotnet run -c Release` | ⚡⚡⚡ Muy rápido | ❌ No | ❌ No | **Producción** |

*Primer start es lento, luego recarga es rápida

---

## 🎯 RECOMENDACIONES POR ESCENARIO

### **Desarrollo Activo (Recomendado)**
```powershell
dotnet watch run --project Cajero.Web
```
✨ **Lo mejor:** Hot reload automático mientras editas

---

### **Testing de Funcionalidad**
```powershell
dotnet run --project Cajero.Web
```
✨ **Mejor:** Ejecución estable sin recompilaciones

---

### **Antes de Hacer Commit**
```powershell
dotnet clean
dotnet build
dotnet test
```
✨ **Mejor:** Asegurar que todo compila y pasa tests

---

### **Despliegue a Producción**
```powershell
dotnet build -c Release
dotnet publish -c Release
```
✨ **Mejor:** Build optimizado y publicable

---

## 🆘 SOLUCIONAR PROBLEMAS

### **Si `dotnet watch` no recarga:**
```powershell
# Detener el proceso actual (Ctrl+C)
# Luego ejecutar:
dotnet clean
dotnet watch run --project Cajero.Web
```

---

### **Si hay errores de puerto ya en uso:**
```powershell
# Ver qué está usando los puertos
netstat -ano | findstr :5000

# O ejecutar en puerto diferente
dotnet run --project Cajero.Web -- --urls "http://localhost:6000"
```

---

### **Si falla la compilación:**
```powershell
# 1. Limpiar completamente
dotnet clean

# 2. Restaurar dependencias
dotnet restore

# 3. Intentar compilar nuevamente
dotnet build
```

---

## 📚 REFERENCIAS ÚTILES

- **Documentación oficial:** https://docs.microsoft.com/dotnet
- **Comandos dotnet:** `dotnet --help`
- **Ayuda para un comando:** `dotnet run --help`

---

**Última actualización:** Enero 2026  
**Estado:** ✅ Listo para usar

