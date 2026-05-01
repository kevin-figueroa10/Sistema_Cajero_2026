# 🧪 GUÍA PASO A PASO - PRUEBAS DEL SISTEMA

## ¡Bienvenido al Testing! 🎉

Este documento te guiará para probar cada funcionalidad del Sistema Cajero.

---

## 🚀 PASO 1: PREPARACIÓN

### 1.1 Abre dos terminales

**Terminal 1 (Para la aplicación web):**
```bash
cd "C:\ruta\a\Sistema Cajero"
cd Cajero.Web
dotnet run
```

Deberías ver:
```
info: Microsoft.AspNetCore.Hosting.Hostings...
      Now listening on: https://localhost:5001
      Now listening on: http://localhost:5000
```

**Terminal 2 (Disponible para la consola luego):**
```bash
cd "C:\ruta\a\Sistema Cajero"
cd Cajero.Consola
dotnet run
```

### 1.2 Abre tu navegador

```
https://localhost:5001
```

Deberías ser redirigido a `/Autenticacion`

---

## 🔐 PASO 2: PRUEBAS DE AUTENTICACIÓN

### ✅ Test 1: Login Exitoso

**Acciones:**
1. En la página de login, ingresa:
   - **Número de Cuenta:** `1001`
   - **PIN:** `1234`
2. Haz clic en **"Iniciar Sesión"**

**Verifica:**
- ✅ Redirección a página de menú principal
- ✅ Navbar muestra: **"Juan García"** y **"Cuenta: 1001"**
- ✅ Aparece botón **"Cerrar Sesión"**
- ✅ 5 opciones de menú: Saldo, Retiro, Depósito, Transferencia, Historial

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### ❌ Test 2: Login Fallido - PIN Incorrecto

**Acciones:**
1. Cierra la sesión o limpia cookies
2. Intenta login con:
   - **Número de Cuenta:** `1001`
   - **PIN:** `9999` (incorrecto)
3. Haz clic en **"Iniciar Sesión"**

**Verifica:**
- ✅ Permanece en página de login
- ✅ Aparece **alerta roja**
- ✅ Mensaje: **"PIN incorrecto"**
- ✅ NO se crea sesión

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### ❌ Test 3: Login Fallido - Cuenta No Existe

**Acciones:**
1. Intenta login con:
   - **Número de Cuenta:** `9999` (no existe)
   - **PIN:** `1234`

**Verifica:**
- ✅ Alerta roja
- ✅ Mensaje: **"Cuenta no encontrada"**
- ✅ NO redirige a menú

**Resultado:** _____________ (PASÓ / FALLÓ)

---

## 💰 PASO 3: PRUEBAS DE OPERACIONES

### 💵 Test 4: Consultar Saldo

**Acciones:**
1. Loguea con: `1001` / `1234`
2. Haz clic en **"💰 Consultar Saldo"**

**Verifica:**
- ✅ Se muestra **saldo grande:** `$5,000.00`
- ✅ **Cuenta:** 1001
- ✅ **Titular:** Juan García
- ✅ Botón "← Volver al Menú"

**Resultado:** _____________ (PASÓ / FALLÓ)

**Nota:** Toma captura de pantalla para documentar

---

### 📤 Test 5: Retiro Exitoso

**Acciones:**
1. En el menú principal, clic en **"📤 Realizar Retiro"**
2. Ingresa monto: **`500`**
3. Haz clic **"Confirmar Retiro"**

**Verifica:**
- ✅ **Alerta verde** con mensaje: "✓ Retiro realizado exitosamente"
- ✅ Redirección a menú principal
- ✅ Saldo debe estar actualizado

**Para verificar el nuevo saldo:**
1. Clic en "💰 Consultar Saldo"
2. Deberías ver: **$4,500.00** (5000 - 500)

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### 📤 Test 6: Retiro Fallido - Saldo Insuficiente

**Acciones:**
1. En retiro, intenta: **`5,000`** (tienes $4,500)
2. Confirma

**Verifica:**
- ✅ **Alerta roja**
- ✅ Mensaje: **"Saldo insuficiente"**
- ✅ Permanece en página retiro
- ✅ **Saldo NO cambia**

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### 📥 Test 7: Depósito Exitoso

**Acciones:**
1. Loguea con: `1003` / `9012` (saldo: $12,000)
2. Clic en **"📥 Realizar Depósito"**
3. Ingresa: **`3,000`**
4. Confirma

**Verifica:**
- ✅ Alerta verde: "✓ Depósito realizado exitosamente"
- ✅ Consulta saldo → Debe ser **$15,000.00** (12000 + 3000)

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### 💸 Test 8: Transferencia Exitosa

**Acciones:**
1. Loguea con: `1001` / `1234` (saldo actual: $4,500)
2. Clic en **"💸 Transferencia"**
3. Selecciona: **"Cuenta 1002 - María López"**
4. Monto: **`1,000`**
5. Confirma

**Verifica:**
- ✅ Alerta verde
- ✅ Mensaje: "✓ Transferencia realizada exitosamente"
- ✅ Saldo de 1001: **$3,500.00** (4500 - 1000)

**Verificar con la otra cuenta:**
1. Cierra sesión
2. Loguea con: `1002` / `5678` (saldo anterior: $8,500)
3. Consulta saldo → Debe ser **$9,500.00** (8500 + 1000)

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### ❌ Test 9: Transferencia Fallida - Misma Cuenta

**Acciones:**
1. En Transferencia, selecciona tu misma cuenta
2. Ingresa monto: `100`
3. Confirma

**Verifica:**
- ✅ Alerta roja
- ✅ Mensaje: **"No se puede transferir a la misma cuenta"**
- ✅ Saldo NO cambia

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### 📋 Test 10: Historial de Transacciones

**Acciones:**
1. Loguea con: `1002` / `5678`
2. Clic en **"📋 Historial"**

**Verifica:**
- ✅ Tabla visible con columnas:
  - Fecha
  - Tipo (con badge: 📤 rojo, 📥 verde, 💸 azul)
  - Descripción
  - Monto
  - Saldo Anterior
  - Saldo Nuevo
- ✅ Muestra **transacciones recientes**
- ✅ **Ordenadas por fecha** (más reciente primero)
- ✅ Datos correctos

**Ejemplo esperado:**
```
Fecha: 26/12/2024 14:32
Tipo: 📤 Transferencia
Descripción: Transferencia desde Cuenta 1001 - $1,000.00
Saldo Anterior: $8,500.00
Saldo Nuevo: $9,500.00
```

**Resultado:** _____________ (PASÓ / FALLÓ)

---

## 🔒 PASO 4: PRUEBAS DE SEGURIDAD

### Test 11: Protección de Rutas

**Acciones:**
1. Cierra completamente el navegador (limpia sesión)
2. Intenta acceder a: `https://localhost:5001/Principal/`

**Verifica:**
- ✅ **Redirección automática** a `/Autenticacion`
- ✅ NO puedes acceder sin sesión

**Intenta otras rutas sin sesión:**
- `https://localhost:5001/Principal/ConsultarSaldo`
- `https://localhost:5001/Principal/Retiro`
- `https://localhost:5001/Principal/Historial`

**Verifica:** ✅ Todas redirigen a login

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### Test 12: Logout

**Acciones:**
1. Loguea con: `1001` / `1234`
2. En navbar (parte superior derecha)
3. Haz clic en **"Cerrar Sesión"**

**Verifica:**
- ✅ Redirección a login
- ✅ **Alerta verde:** "✓ Sesión cerrada correctamente"
- ✅ Sesión eliminada
- ✅ No puedes acceder a Principal

**Resultado:** _____________ (PASÓ / FALLÓ)

---

## 💻 PASO 5: PRUEBAS DE CONSOLA

### Test 13: Inicio de Consola

**Acciones:**
En Terminal 2, ejecuta:
```bash
cd Cajero.Consola
dotnet run
```

**Verifica:**
- ✅ Menú inicial aparece
- ✅ Solicita "Número de Cuenta:"
- ✅ Solicita "PIN:"
- ✅ Sin excepciones

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### Test 14: Login Consola

**Acciones:**
1. Ingresa cuenta: `1001`
2. Ingresa PIN: `1234`

**Verifica:**
- ✅ Mensaje: **"✓ Autenticación exitosa."**
- ✅ Menú principal aparece
- ✅ Muestra: **"Bienvenido, Juan García"**
- ✅ 6 opciones numeradas

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### Test 15: Operaciones Consola

**Acciones (Haz esto secuencialmente):**

**1. Consultar Saldo (Opción 1)**
```
Selecciona: 1
↓
Resultado Esperado: "Saldo Disponible: $5,000.00"
```
✅ _____________

**2. Hacer Retiro (Opción 2)**
```
Selecciona: 2
Monto: 300
↓
Resultado Esperado: "✓ Retiro realizado exitosamente."
"Nuevo Saldo: $4,700.00"
```
✅ _____________

**3. Hacer Depósito (Opción 3)**
```
Selecciona: 3
Monto: 500
↓
Resultado Esperado: "✓ Depósito realizado exitosamente."
"Nuevo Saldo: $5,200.00"
```
✅ _____________

**4. Consultar Saldo Nuevo (Opción 1)**
```
Selecciona: 1
↓
Resultado Esperado: "Saldo Disponible: $5,200.00"
```
✅ _____________

**5. Ver Historial (Opción 5)**
```
Selecciona: 5
↓
Resultado Esperado:
- Muestra transacciones
- Tipo, monto, saldos
- Ordenadas cronológicamente
```
✅ _____________

**6. Cerrar Sesión (Opción 6)**
```
Selecciona: 6
↓
Resultado Esperado:
"✓ Sesión cerrada. Hasta luego."
Vuelve a solicitar login
```
✅ _____________

**Resultado General Consola:** _____________ (PASÓ / FALLÓ)

---

## 🎨 PASO 6: PRUEBAS DE INTERFAZ

### Test 16: Diseño Responsivo

**Acciones:**
1. Abre DevTools: **F12**
2. Activa modo responsive: **Ctrl + Shift + M**
3. Prueba en diferentes tamaños:

**Mobile (375px):**
- [ ] Navbar se adapta
- [ ] Menú se colapsa
- [ ] Cards se apilan
- [ ] Botones clickeables

**Tablet (768px):**
- [ ] Grid de 2 columnas
- [ ] Tabla scrollea
- [ ] Elementos legibles

**Desktop (1920px):**
- [ ] Diseño óptimo
- [ ] Columnas bien distribuidas

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### Test 17: Elementos Visuales

**Verifica:**
- [ ] **Logo/Titulo:** "💳 Sistema Cajero"
- [ ] **Colores:** Azul profesional (#1e40af, #3b82f6)
- [ ] **Emojis:** Presentes en menú y botones
- [ ] **Gradientes:** Fondo con degradado
- [ ] **Alerts:** Colores diferenciados (rojo, verde, azul)
- [ ] **Fuentes:** Legibles y consistentes
- [ ] **Espaciado:** Adecuado y ordenado

**Resultado:** _____________ (PASÓ / FALLÓ)

---

## 📊 PASO 7: PRUEBAS DE DATOS

### Test 18: Integridad de Datos

**Escenario: Transferencia de Juan a María**

**Antes:**
- Cuenta 1001 (Juan): $5,000.00
- Cuenta 1002 (María): $8,500.00

**Acción:**
- Juan transfiere $2,000 a María

**Después (Verifica):**

**Loguea Juan (1001/1234):**
```
Saldo: $3,000.00 (5000 - 2000) ✅ _______
Historial: Mostrar transferencia ✅ _______
```

**Loguea María (1002/5678):**
```
Saldo: $10,500.00 (8500 + 2000) ✅ _______
Historial: Mostrar transferencia ✅ _______
```

**Resultado:** _____________ (PASÓ / FALLÓ)

---

### Test 19: Múltiples Operaciones

**Acciones (Loguea con 1003/9012):**
1. Retiro: $500 → Saldo: $14,500
2. Depósito: $1,000 → Saldo: $15,500
3. Retiro: $250 → Saldo: $15,250
4. Transferencia a 1001: $2,000 → Saldo: $13,250

**Verifica:**
- ✅ Saldo final correcto: **$13,250.00**
- ✅ Historial muestra todas (4 transacciones)
- ✅ Montos correctos
- ✅ Orden cronológico

**Resultado:** _____________ (PASÓ / FALLÓ)

---

## 🎯 RESUMEN FINAL

### Matriz de Pruebas Completadas

| # | Test | Resultado |
|---|------|-----------|
| 1 | Login Válido | ✅ / ❌ |
| 2 | Login PIN Inválido | ✅ / ❌ |
| 3 | Login Cuenta No Existe | ✅ / ❌ |
| 4 | Consultar Saldo | ✅ / ❌ |
| 5 | Retiro Exitoso | ✅ / ❌ |
| 6 | Retiro Saldo Insuficiente | ✅ / ❌ |
| 7 | Depósito Exitoso | ✅ / ❌ |
| 8 | Transferencia Exitosa | ✅ / ❌ |
| 9 | Transferencia Misma Cuenta | ✅ / ❌ |
| 10 | Historial | ✅ / ❌ |
| 11 | Protección de Rutas | ✅ / ❌ |
| 12 | Logout | ✅ / ❌ |
| 13 | Consola Inicio | ✅ / ❌ |
| 14 | Consola Login | ✅ / ❌ |
| 15 | Consola Operaciones | ✅ / ❌ |
| 16 | Responsivo | ✅ / ❌ |
| 17 | Elementos Visuales | ✅ / ❌ |
| 18 | Integridad de Datos | ✅ / ❌ |
| 19 | Múltiples Operaciones | ✅ / ❌ |

---

## 📋 PUNTUACIÓN FINAL

```
Total Pruebas: 19
Aprobadas: _____ / 19
Porcentaje: _____%

[ ] 100% - EXCELENTE ✅
[ ] 95-99% - MUY BIEN
[ ] 90-94% - BIEN
[ ] < 90% - REVISAR
```

---

## 🐛 Bugs o Problemas Encontrados

```
1. _________________________________
2. _________________________________
3. _________________________________
```

---

## 📝 Observaciones Finales

```
_________________________________________________________________________
_________________________________________________________________________
_________________________________________________________________________
```

---

## ✅ Firma del Tester

**Nombre:** ________________________

**Fecha:** ________________________

**Resultado Final:** ✅ APROBADO / ⚠️ APROBADO CON SALVEDADES / ❌ NO APROBADO

---

**¡Gracias por probar el Sistema Cajero Automático! 🎉**
