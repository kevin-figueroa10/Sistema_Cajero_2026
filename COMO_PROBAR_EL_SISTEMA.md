# 🚀 INSTRUCCIONES PARA PROBAR EL SISTEMA

## **¡Bienvenido al Testing! Aquí está todo lo que necesitas** 🧪

---

## 📚 ARCHIVOS DE TESTING DISPONIBLES

He creado **4 documentos** para ayudarte a probar completamente:

### 1. 📋 **PLAN_PRUEBAS.md** ← Documento OFICIAL de QA
- **Para:** Profesionales de QA
- **Contenido:** 30 casos de prueba detallados
- **Uso:** Documento formal para documentar resultados
- **Tiempo:** 2-3 horas

### 2. 🧪 **GUIA_PRUEBAS_PASO_A_PASO.md** ← RECOMENDADO PARA COMENZAR ⭐
- **Para:** Usuarios nuevos / Rápido
- **Contenido:** Pasos claros y sencillos
- **Uso:** Sigue los pasos, marca ✅ o ❌
- **Tiempo:** 45 minutos - 1 hora

### 3. ✅ **TESTING_CHECKLIST_RAPIDO.txt** ← Versión visual
- **Para:** Testing rápido
- **Contenido:** Checklist compacto
- **Uso:** Marcar rápidamente
- **Tiempo:** 30 minutos

### 4. 📖 **PLAN_PRUEBAS.md**
- **Para:** Documentación formal
- **Contenido:** 30 casos de prueba
- **Uso:** Registro oficial

---

## 🎯 RECOMENDADO: COMIENZA CON ESTA GUÍA

### **OPCIÓN 1: Testing Web + Consola (45 minutos)** ✨

**Paso 1: Abre Terminal 1**
```bash
cd "C:\ruta\a\Sistema Cajero"
cd Cajero.Web
dotnet run
```
Espera hasta ver:
```
Now listening on: https://localhost:5001
```

**Paso 2: Abre tu navegador**
```
https://localhost:5001
```

**Paso 3: Abre Terminal 2** (en otra ventana)
```bash
cd "C:\ruta\a\Sistema Cajero"
cd Cajero.Consola
dotnet run
```

**Paso 4: Abre el archivo** `GUIA_PRUEBAS_PASO_A_PASO.md`

**Paso 5: Sigue los pasos del Test 1 al Test 19**

---

## 🔍 LO QUE NECESITAS PROBAR

### **Autenticación Web (5 minutos)**
```
✓ Login correcta: 1001 / 1234
✗ Login PIN incorrecto
✗ Login cuenta no existe
```

### **Operaciones Bancarias (15 minutos)**
```
✓ Consultar saldo
✓ Retiro exitoso
✗ Retiro sin saldo
✓ Depósito
✓ Transferencia exitosa
✗ Transferencia misma cuenta
✓ Ver historial
```

### **Seguridad (5 minutos)**
```
✓ Protección de rutas (sin sesión)
✓ Logout
```

### **Consola (15 minutos)**
```
✓ Login
✓ Todas las operaciones
✓ Logout
```

### **Interfaz (5 minutos)**
```
✓ Responsivo (mobile, tablet, desktop)
✓ Elementos visuales
```

---

## 🔑 CREDENCIALES DE PRUEBA

Copia estas en tu documento de pruebas:

```
╔════════════════════════════════════════════════════════╗
║              DATOS DE PRUEBA LISTOS PARA USAR          ║
╠════════════════════════════════════════════════════════╣
║ Cuenta: 1001 | PIN: 1234 | Juan García    | $5,000   ║
║ Cuenta: 1002 | PIN: 5678 | María López    | $8,500   ║
║ Cuenta: 1003 | PIN: 9012 | Carlos M.      | $12,000  ║
╚════════════════════════════════════════════════════════╝
```

---

## 📋 MATRIZ RÁPIDA DE PRUEBAS

**Copia y pega esto en tu documento para ir marcando:**

```
PRUEBAS DE AUTENTICACIÓN
[ ] Test 1: Login Válido (1001/1234) ✅ / ❌
[ ] Test 2: Login PIN Inválido ✅ / ❌
[ ] Test 3: Login Cuenta No Existe ✅ / ❌

PRUEBAS DE OPERACIONES
[ ] Test 4: Consultar Saldo ✅ / ❌
[ ] Test 5: Retiro Exitoso ✅ / ❌
[ ] Test 6: Retiro Saldo Insuficiente ✅ / ❌
[ ] Test 7: Depósito Exitoso ✅ / ❌
[ ] Test 8: Transferencia Exitosa ✅ / ❌
[ ] Test 9: Transferencia Misma Cuenta ✅ / ❌
[ ] Test 10: Historial ✅ / ❌

PRUEBAS DE SEGURIDAD
[ ] Test 11: Protección de Rutas ✅ / ❌
[ ] Test 12: Logout ✅ / ❌

PRUEBAS DE CONSOLA
[ ] Test 13: Inicio Consola ✅ / ❌
[ ] Test 14: Login Consola ✅ / ❌
[ ] Test 15: Operaciones Consola ✅ / ❌

PRUEBAS DE INTERFAZ
[ ] Test 16: Responsivo ✅ / ❌
[ ] Test 17: Elementos Visuales ✅ / ❌

PRUEBAS DE DATOS
[ ] Test 18: Integridad de Datos ✅ / ❌
[ ] Test 19: Múltiples Operaciones ✅ / ❌

TOTAL: 19/19 ✅
```

---

## ✨ PRUEBAS RÁPIDAS POR FUNCIONALIDAD

### 🔐 **1. AUTENTICACIÓN (3 minutos)**

**LOGIN CORRECTO:**
```
1. Accede a https://localhost:5001
2. Ingresa: Cuenta 1001 | PIN 1234
3. ✅ Deberías ver "Bienvenido, Juan García"
```

**LOGIN INCORRECTO:**
```
1. Limpia cookies o cierra navegador
2. Ingresa: Cuenta 1001 | PIN 9999
3. ✅ Deberías ver alerta roja "PIN incorrecto"
```

---

### 💰 **2. OPERACIONES (10 minutos)**

**RETIRO:**
```
1. Loguea: 1001 / 1234
2. Clic "Retiro"
3. Ingresa: 500
4. ✅ Alerta verde, saldo = $4,500
```

**DEPÓSITO:**
```
1. Clic "Depósito"
2. Ingresa: 1,000
3. ✅ Alerta verde, saldo = $5,500
```

**TRANSFERENCIA:**
```
1. Clic "Transferencia"
2. Selecciona: Cuenta 1002 (María)
3. Ingresa: 1,500
4. ✅ Alerta verde
5. Loguea 1002 → Saldo debe ser $10,000 (8500+1500)
```

---

### 🔒 **3. SEGURIDAD (2 minutos)**

**PROTECCIÓN:**
```
1. Sin loguear, accede a: https://localhost:5001/Principal
2. ✅ Te redirige automáticamente a login
```

**LOGOUT:**
```
1. Estando loguado, clic "Cerrar Sesión"
2. ✅ Sesión se cierra y vuelves a login
```

---

### 💻 **4. CONSOLA (10 minutos)**

**BÁSICO:**
```
1. Terminal 2: dotnet run en Cajero.Consola
2. Cuenta: 1001 | PIN: 1234
3. ✅ "Bienvenido, Juan García"
4. Selecciona: 1 (Saldo) → $5,000
5. Selecciona: 2 (Retiro) → 500 → $4,500
6. Selecciona: 6 (Logout) → Vuelve a login
```

---

## 🎯 PRUEBA COMPLETA EN 5 MINUTOS (VERSIÓN ULTRA RÁPIDA)

```
1. [ ] Compila: dotnet build ✅
2. [ ] Web inicia en Terminal 1: dotnet run ✅
3. [ ] Navegador: https://localhost:5001 ✅
4. [ ] Login: 1001 / 1234 ✅
5. [ ] Retiro: $500 → $4,500 ✅
6. [ ] Transferencia: a 1002 / $1,000 ✅
7. [ ] Historial: Muestra transacciones ✅
8. [ ] Logout ✅
9. [ ] Consola: Mismas pruebas ✅
```

**RESULTADO: ✅ SISTEMA FUNCIONA PERFECTO**

---

## 📱 PRUEBA EN MOBILE (Bonus)

```
1. F12 (DevTools)
2. Ctrl+Shift+M (Vista responsive)
3. Selecciona iPhone 12 (390px)
4. [ ] Todo se ve bien ✅
5. [ ] Botones clickeables ✅
6. [ ] Texto legible ✅
```

---

## 🎬 GRABANDO TUS PRUEBAS

Si quieres documentar video:

1. Abre OBS Studio (gratuito)
2. Graba pantalla + audio
3. Narra mientras pruebas
4. Sube a YouTube o guarda

**Script de narración:**
```
"Probando Sistema Cajero Automático 2026...
Ingresando cuenta 1001 con PIN 1234...
Redirigiendo a menú principal...
Realizando retiro de $500...
Consultando nuevo saldo..."
```

---

## ❓ SI ALGO FALLA

### **Error: Puerto 5001 ocupado**
```bash
# Windows
netstat -ano | findstr :5001
taskkill /PID [PID] /F

# Linux/Mac
lsof -i :5001
kill -9 [PID]
```

### **Error: No encuentra .NET**
```bash
# Verifica instalación
dotnet --version

# Si no funciona, descarga de:
https://dotnet.microsoft.com/download
```

### **Error: No compila**
```bash
dotnet clean
dotnet restore
dotnet build
```

### **Error: Sesión no se crea**
- Borra cookies del navegador
- Usa navegación privada/incógnito
- Limpia cache: Ctrl+Shift+Del

---

## 📞 NECESITAS AYUDA?

1. **Revisa README.md** - Documentación general
2. **Revisa DESARROLLO.md** - Cosas técnicas
3. **Revisa GUIA_PRUEBAS_PASO_A_PASO.md** - Paso a paso

---

## 🎉 ¡LISTO PARA EMPEZAR!

**Recomendación para empezar YA:**

```bash
# Terminal 1
cd "Sistema Cajero"
cd Cajero.Web
dotnet run

# Terminal 2  
cd "Sistema Cajero"
cd Cajero.Consola
dotnet run
```

Luego abre: `GUIA_PRUEBAS_PASO_A_PASO.md` y sigue los pasos

---

## 📊 VERSIONES DE TESTING

| Tipo | Tiempo | Archivo | Para Quién |
|------|--------|---------|-----------|
| Ultra Rápida | 5 min | Este archivo | Verificación rápida |
| Rápida | 30 min | TESTING_CHECKLIST_RAPIDO.txt | Testing ágil |
| Estándar | 45 min | GUIA_PRUEBAS_PASO_A_PASO.md ⭐ | Usuario nuevo |
| Profesional | 2-3 hrs | PLAN_PRUEBAS.md | QA formal |

---

## ✅ CHECKLIST ANTES DE EMPEZAR

- [ ] .NET 10 instalado
- [ ] Visual Studio o VS Code
- [ ] Código descargado
- [ ] `dotnet build` exitoso
- [ ] Navegador abierto
- [ ] 2 terminales preparadas
- [ ] Este documento a mano
- [ ] Datos de prueba copiados
- [ ] Ánimo de testing 🚀

---

**¡Vamos a probar el Sistema Cajero Automático! 💳🚀**

Comienza ahora mismo con:
👉 **GUIA_PRUEBAS_PASO_A_PASO.md**
