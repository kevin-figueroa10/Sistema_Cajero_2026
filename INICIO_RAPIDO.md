# 🚀 Guía Rápida de Inicio

## ⚡ Inicio Rápido (5 minutos)

### En Windows:
```batch
run.bat
```

### En Linux/macOS:
```bash
chmod +x run.sh
./run.sh
```

### Manualmente:
```bash
# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar Web
cd Cajero.Web
dotnet run

# En otra terminal - Ejecutar Consola
cd Cajero.Consola
dotnet run
```

## 🌐 Acceso a la Aplicación Web

Una vez ejecutada, accede a:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

## 🔑 Credenciales de Prueba

| Cuenta | PIN | Nombre |
|--------|-----|--------|
| 1001 | 1234 | Juan García |
| 1002 | 5678 | María López |
| 1003 | 9012 | Carlos Martínez |

## 📁 Estructura de Carpetas

```
Sistema Cajero/
├── Cajero.Core/          → Lógica de negocio
├── Cajero.Web/           → Aplicación web MVC
├── Cajero.Consola/       → Interfaz consola
├── README.md             → Documentación principal
├── DESARROLLO.md         → Guía para desarrolladores
├── INICIO_RAPIDO.md      → Este archivo
├── run.sh                → Script para Linux/macOS
└── run.bat               → Script para Windows
```

## 🎯 Funcionalidades Principales

### 1. **Autenticación**
- Login con número de cuenta y PIN
- Sesión segura (30 minutos)
- Validaciones robustas

### 2. **Operaciones Bancarias**
- ✅ Consultar saldo
- ✅ Realizar retiros
- ✅ Realizar depósitos
- ✅ Transferencias entre cuentas
- ✅ Ver historial de transacciones

### 3. **Interfaz de Usuario**
- Diseño limpio y profesional
- Totalmente responsivo
- Accesible y fácil de usar

## 📊 Arquitectura

```
Presentación (Views & Controllers)
        ↓
Aplicación (Services)
        ↓
Dominio (Models & Interfaces)
        ↓
Persistencia (Repositories)
```

## 🔐 Seguridad Implementada

- ✅ Validación de sesión
- ✅ Validaciones en servidor
- ✅ Manejo de errores seguro
- ✅ HTTPS por defecto

## 📝 Primeros Pasos

1. **Inicia la aplicación** usando `run.bat` o `run.sh`
2. **Accede a** https://localhost:5001
3. **Ingresa credenciales:**
   - Cuenta: `1001`
   - PIN: `1234`
4. **Explora las operaciones** en el menú principal

## 🐛 Solución de Problemas

### Puerto 5001 ocupado
```bash
# Cambiar puerto en Cajero.Web/Properties/launchSettings.json
# O matar el proceso en el puerto
netstat -ano | findstr :5001  # Windows
lsof -i :5001                # Linux/macOS
```

### Error de dependencias
```bash
dotnet clean
dotnet restore
dotnet build
```

### Error de compilación
- Verificar que .NET 10 esté instalado
- Ejecutar `dotnet restore`

## 📚 Documentación Adicional

- **README.md** - Documentación completa del proyecto
- **DESARROLLO.md** - Guía para desarrolladores
- Comentarios en código - Documentación inline

## 🎓 Aprende de Este Proyecto

Este proyecto demuestra:
- ✅ Arquitectura en capas
- ✅ Inyección de dependencias
- ✅ Patrones de diseño (Repository)
- ✅ ASP.NET Core MVC
- ✅ Manejo de sesiones
- ✅ Validaciones robustas
- ✅ Interfaz web moderna

## 🤝 Contribuir

1. Fork el repositorio
2. Crea una rama: `git checkout -b feature/tu-feature`
3. Commit: `git commit -am 'Añade nueva feature'`
4. Push: `git push origin feature/tu-feature`
5. Pull Request

## 📞 Soporte

- **GitHub Issues**: https://github.com/kevin-figueroa10/Sistema_Cajero_2026/issues
- **Documentación**: Ver archivos .md en el proyecto

---

**¡Listo para comenzar! 🎉**

Más información en: https://github.com/kevin-figueroa10/Sistema_Cajero_2026
