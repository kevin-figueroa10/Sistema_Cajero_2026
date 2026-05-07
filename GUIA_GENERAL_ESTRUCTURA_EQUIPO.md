# 📘 GUÍA GENERAL - ESTRUCTURA DE EQUIPO Y TRABAJO

**Proyecto:** BANCO NEW SMART CAPITAL - Cajero Automático 2026  
**Fecha:** Enero 2026  
**Versión:** 2026.1.0

---

## 👥 ASIGNACIÓN DE PERSONAS Y RAMAS

```
┌─────────────────────────────────────────────────────────────┐
│  ESTRUCTURA DE EQUIPO PROFESIONAL                           │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  PERSONA 1: Backend - Aplicación Consola                    │
│  📂 Rama: feature-backend                                   │
│  📦 Proyecto: Cajero.Consola                                │
│  📝 Archivo: GUIA_PERSONA_1_BACKEND_CONSOLA.md              │
│                                                              │
│  PERSONA 2: Backend - Datos y Lógica de Negocio             │
│  📂 Rama: feature-backend (MISMO QUE PERSONA 1)             │
│  📦 Proyecto: Cajero.Core                                   │
│  📝 Archivo: GUIA_PERSONA_2_BACKEND_DATA.md                 │
│                                                              │
│  PERSONA 3: Frontend - Vistas (HTML/Razor)                  │
│  📂 Rama: feature-frontend-web                              │
│  📦 Proyecto: Cajero.Web/Views                              │
│  📝 Archivo: GUIA_PERSONAS_3_4_FRONTEND_WEB.md              │
│                                                              │
│  PERSONA 4: Frontend - Controladores                        │
│  📂 Rama: feature-frontend-web (MISMO QUE PERSONA 3)        │
│  📦 Proyecto: Cajero.Web/Controllers                        │
│  📝 Archivo: GUIA_PERSONAS_3_4_FRONTEND_WEB.md              │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎯 RESPONSABILIDADES DETALLADAS

### **PERSONA 1: Backend - Consola**
✅ **Responsabilidades:**
- Crear interfaz de consola completa
- Implementar menú principal interactivo
- Manejar entrada/salida de usuario
- Conectar con Cajero.Core
- Realizar pruebas básicas

📁 **Archivos a crear:**
```
Cajero.Consola/
├── Program.cs
├── Menus/
│   ├── MenuPrincipal.cs
│   ├── MenuOperaciones.cs
│   └── MenuCuenta.cs
└── Servicios/
    ├── GestorConsola.cs
    └── ValidadorEntrada.cs
```

---

### **PERSONA 2: Backend - Data**
✅ **Responsabilidades:**
- Crear modelos de datos
- Implementar servicios (lógica de negocio)
- Crear repositorios (acceso a datos)
- Manejar validaciones
- Gestionar transacciones

📁 **Archivos a crear:**
```
Cajero.Core/
├── Models/
│   ├── Cuenta.cs
│   ├── Transaccion.cs
│   ├── Comprobante.cs
│   └── Enums/TipoCuentaEnum.cs
├── Interfaces/
│   ├── IServicioCajero.cs
│   ├── IRepositorioCuenta.cs
│   └── IRepositorioTransaccion.cs
├── Repositories/
│   ├── RepositorioCuenta.cs
│   └── RepositorioTransaccion.cs
├── Services/
│   ├── ServicioCajero.cs
│   └── ConfiguracionCuenta.cs
└── Responses/
    └── ResultadoOperacion.cs
```

---

### **PERSONA 3: Frontend - Views**
✅ **Responsabilidades:**
- Diseñar todas las pantallas
- Crear vistas Razor (.cshtml)
- Aplicar estilos CSS
- Agregar validación de cliente (JavaScript)
- Mejorar la experiencia del usuario

📁 **Archivos a crear:**
```
Cajero.Web/
├── Views/
│   ├── Autenticacion/Index.cshtml
│   ├── Principal/
│   │   ├── Index.cshtml
│   │   ├── ConsultarSaldo.cshtml
│   │   ├── Retiro.cshtml
│   │   ├── Deposito.cshtml
│   │   ├── Transferencia.cshtml
│   │   ├── Historial.cshtml
│   │   ├── MiCuenta.cshtml
│   │   ├── CambiarPIN.cshtml
│   │   └── Comprobante.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       └── Error.cshtml
└── wwwroot/css/styles.css
```

---

### **PERSONA 4: Frontend - Controllers**
✅ **Responsabilidades:**
- Crear controladores ASP.NET
- Manejar sesiones y autenticación
- Conectar vistas con backend
- Procesar solicitudes HTTP
- Validar entrada de usuario

📁 **Archivos a crear:**
```
Cajero.Web/
├── Controllers/
│   ├── AutenticacionController.cs
│   └── PrincipalController.cs
└── Program.cs (configuración)
```

---

## 🚀 FLUJO DE TRABAJO GENERAL

```
┌─────────────────────────────────────────────────────────┐
│                 FLUJO GIT ESTÁNDAR                      │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  1. Clonar repositorio                                  │
│     git clone https://github.com/.../Sistema_Cajero    │
│                                                         │
│  2. Cambiar a tu rama                                   │
│     git checkout feature-xxx                           │
│                                                         │
│  3. Actualizar rama                                     │
│     git pull origin feature-xxx                        │
│                                                         │
│  4. Trabajar en tu código                              │
│     [Crear/editar archivos]                            │
│                                                         │
│  5. Ver cambios                                         │
│     git status                                         │
│                                                         │
│  6. Agregar cambios                                     │
│     git add .                                          │
│                                                         │
│  7. Hacer commit                                        │
│     git commit -m "feat: Descripción clara"            │
│                                                         │
│  8. Enviar a GitHub                                     │
│     git push origin feature-xxx                        │
│                                                         │
│  9. Crear Pull Request                                 │
│     [En GitHub] Base: develop | Compare: feature-xxx  │
│                                                         │
│  10. Administrador revisa y aprueba                    │
│      [El administrador integra a develop]             │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

---

## 📋 ESTRUCTURA DE CARPETAS DEL PROYECTO

```
Sistema Cajero/
│
├── .git/                          (Repositorio Git)
│
├── Cajero.sln                     (Solución principal)
│
├── Cajero.Core/                   (PERSONA 2)
│   ├── Models/
│   ├── Interfaces/
│   ├── Repositories/
│   ├── Services/
│   ├── Responses/
│   └── Cajero.Core.csproj
│
├── Cajero.Consola/                (PERSONA 1)
│   ├── Program.cs
│   ├── Menus/
│   ├── Servicios/
│   └── Cajero.Consola.csproj
│
├── Cajero.Web/                    (PERSONAS 3 y 4)
│   ├── Controllers/               (PERSONA 4)
│   ├── Views/                     (PERSONA 3)
│   ├── wwwroot/
│   ├── Program.cs
│   ├── appsettings.json
│   └── Cajero.Web.csproj
│
└── [Documentos]
    ├── GUIA_PERSONA_1_BACKEND_CONSOLA.md
    ├── GUIA_PERSONA_2_BACKEND_DATA.md
    ├── GUIA_PERSONAS_3_4_FRONTEND_WEB.md
    └── GUIA_GENERAL_ESTRUCTURA_EQUIPO.md
```

---

## 💻 COMANDOS IMPORTANTES

### **Clonar repositorio**
```powershell
git clone https://github.com/kevin-figueroa10/Sistema_Cajero_2026.git
cd "Sistema Cajero"
```

### **Cambiar a tu rama**
```powershell
git checkout feature-backend           # Personas 1 y 2
git checkout feature-frontend-web      # Personas 3 y 4
```

### **Actualizar tu rama**
```powershell
git pull origin feature-backend        # O tu rama asignada
```

### **Ver estado**
```powershell
git status
git branch
```

### **Hacer cambios**
```powershell
git add .
git commit -m "feat: Descripción clara de cambios"
git push origin feature-backend        # O tu rama asignada
```

### **Compilar proyecto**
```powershell
cd "C:\Users\[Tu Nombre]\Downloads\Sistema Cajero"
dotnet build
```

### **Ejecutar proyecto**
```powershell
# Aplicación de consola
dotnet run --project Cajero.Consola

# Aplicación web
dotnet run --project Cajero.Web
```

---

## 🔗 DEPENDENCIAS ENTRE PROYECTOS

```
Cajero.Consola
    ↓ (depende de)
Cajero.Core
    (proporciona modelos e interfaces)

Cajero.Web
    ↓ (depende de)
Cajero.Core
    (proporciona modelos e interfaces)
```

**NOTA:** Cajero.Core es la base de todo. Persona 2 debe completarlo primero.

---

## ✅ ORDEN RECOMENDADO DE TRABAJO

### **FASE 1: Backend Data (Persona 2)** 
⏱️ Tiempo estimado: 3-4 días
- Crear todos los modelos
- Crear interfaces
- Crear repositorios con datos iniciales
- Crear servicios con lógica completa

### **FASE 2: Backend Consola (Persona 1)**
⏱️ Tiempo estimado: 2-3 días
- Crear menú principal
- Conectar con Cajero.Core
- Implementar todas las operaciones

### **FASE 3: Frontend Controllers (Persona 4)**
⏱️ Tiempo estimado: 2-3 días
- Crear controlador de autenticación
- Crear controlador de operaciones
- Manejar sesiones

### **FASE 4: Frontend Views (Persona 3)**
⏱️ Tiempo estimado: 2-3 días
- Crear todas las vistas
- Aplicar estilos
- Agregar validaciones

---

## 📞 COMUNICACIÓN Y COORDINACIÓN

### **Reuniones recomendadas:**
- **Diarias:** 15 minutos - Status update
- **Cada 2 días:** 30 minutos - Sincronización técnica
- **Semanales:** 1 hora - Revisión de progreso

### **Canales:**
- WhatsApp/Teams: Comunicación rápida
- GitHub Issues: Problemas y bugs
- Pull Requests: Revisión de código

### **Coordinación especial:**

**Entre Personas 1 y 2 (Rama feature-backend):**
- Coordinar commits frecuentes
- Persona 2 primero, luego Persona 1
- Revisar regularmente cambios

**Entre Personas 3 y 4 (Rama feature-frontend-web):**
- Persona 4 define controladores
- Persona 3 crea vistas según especificación
- Sincronizar nombres de propiedades

---

## 🐛 RESOLVIENDO CONFLICTOS GIT

Si hay conflictos al hacer pull:

```powershell
# Ver conflictos
git status

# Abrir archivo conflictivo y resolver manualmente
# Luego:
git add .
git commit -m "fix: Resolver conflictos de merge"
git push origin [tu-rama]
```

---

## 📊 CHECKLIST ANTES DE HACER PULL REQUEST

- [ ] Compilación sin errores: `dotnet build`
- [ ] Pruebas locales: `dotnet run`
- [ ] Commits con mensaje claro
- [ ] Sin cambios accidentales de otras personas
- [ ] Código bien indentado y legible
- [ ] Documentación actualizada (si aplica)

---

## 🎓 BUENAS PRÁCTICAS

✅ **HACER:**
- Commits pequeños y frecuentes
- Mensajes de commit descriptivos
- Pruebas locales antes de push
- Comunicación constante
- Revisar código de otros

❌ **NO HACER:**
- Cambiar código de otra persona sin avisar
- Commits grandes sin revisar
- Mensaje de commit "aaa" o "fix"
- Trabajar sin actualizar la rama
- Hacer push a main o develop directamente

---

## 📚 RECURSOS DISPONIBLES

**Documentos de guía:**
- `GUIA_PERSONA_1_BACKEND_CONSOLA.md` - Para Persona 1
- `GUIA_PERSONA_2_BACKEND_DATA.md` - Para Persona 2
- `GUIA_PERSONAS_3_4_FRONTEND_WEB.md` - Para Personas 3 y 4

**En el repositorio:**
- Carpeta `Docs/` - Documentación adicional
- `README.md` - Información general del proyecto
- `CUENTAS_PROFESIONALES_DOCUMENTACION.md` - Cuentas de prueba

---

## 🚀 PRÓXIMAS ACCIONES

### **HOY:**
1. ✅ Leer esta guía completa
2. ✅ Leer tu guía específica (GUIA_PERSONA_X_XXX.md)
3. ✅ Clonar el repositorio
4. ✅ Cambiar a tu rama asignada

### **MAÑANA:**
1. ✅ Crear carpetas del proyecto
2. ✅ Crear primera versión de archivos
3. ✅ Hacer primer commit
4. ✅ Hacer primer push

### **ESTA SEMANA:**
1. ✅ Implementar funcionalidad básica
2. ✅ Hacer múltiples commits
3. ✅ Sincronizar con equipo
4. ✅ Probar en desarrollo

---

## 📞 ADMINISTRADOR

**Encargado de:** Rama `feature-arquitectura`

**Responsabilidades:**
- Revisar Pull Requests
- Resolver conflictos
- Integrar a develop
- Supervisar calidad de código

**Cómo contactar:**
- GitHub Issues
- Pull Request comments
- Chat del equipo

---

## 📝 PLANTILLA DE COMMIT

```
[tipo]: [descripción corta]

[descripción detallada opcional]

[referencias a issues, si aplica]

Tipos recomendados:
feat: Nueva funcionalidad
fix: Corrección de bug
docs: Documentación
refactor: Cambio de código sin nueva funcionalidad
test: Pruebas
```

### Ejemplos:
```
feat: Implementar modelo Cuenta con validaciones

fix: Corregir cálculo de comisión en transferencias

docs: Actualizar instrucciones de instalación
```

---

## 🎉 ¡ESTÁS LISTO PARA EMPEZAR!

**Siguiente paso:** 
Lee la guía específica para tu persona/rol:
- Persona 1 → `GUIA_PERSONA_1_BACKEND_CONSOLA.md`
- Persona 2 → `GUIA_PERSONA_2_BACKEND_DATA.md`
- Persona 3 → `GUIA_PERSONAS_3_4_FRONTEND_WEB.md`
- Persona 4 → `GUIA_PERSONAS_3_4_FRONTEND_WEB.md`

---

**Versión:** 2026.1.0  
**Última actualización:** Enero 2026  
**Estado:** Listo para usar

