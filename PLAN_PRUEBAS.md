# 🧪 PLAN DE PRUEBAS - SISTEMA CAJERO AUTOMÁTICO

## Documento de Testing Completo para QA

---

## 📋 INFORMACIÓN GENERAL

| Aspecto | Detalle |
|--------|---------|
| Proyecto | Sistema Cajero Automático 2026 |
| Versión | 1.0.0 |
| Fecha de Testing | 2026 |
| Responsable | QA Team |
| Ambiente | Development/Local |

---

## 🎯 OBJETIVOS DE TESTING

✅ Verificar que todas las funcionalidades funcionan correctamente
✅ Validar la seguridad del sistema
✅ Comprobar el manejo de errores
✅ Verificar la integridad de datos
✅ Validar la interfaz de usuario
✅ Probar casos extremos

---

## 🔧 CONFIGURACIÓN DEL AMBIENTE DE TESTING

### Requisitos Previos
```bash
✓ .NET 10 SDK instalado
✓ Visual Studio 2022+ o VS Code
✓ Git configurado
✓ Navegador web moderno
✓ Terminal/PowerShell
```

### Datos de Prueba
```
Cuenta 1001 | PIN: 1234 | Juan García    | $5,000.00
Cuenta 1002 | PIN: 5678 | María López    | $8,500.00
Cuenta 1003 | PIN: 9012 | Carlos M.      | $12,000.00
```

---

## 🚀 PRUEBAS DE COMPILACIÓN

### Test 1: Build Exitoso
**Pasos:**
```bash
cd "Sistema Cajero"
dotnet clean
dotnet restore
dotnet build
```

**Resultado Esperado:** ✅ Compilación sin errores
**Resultado Obtenido:** ✅ PASÓ

---

## 🌐 PRUEBAS DE INTERFAZ WEB

### Test 2: Inicio de Aplicación Web

**Pasos:**
```bash
cd Cajero.Web
dotnet run
```

**Resultado Esperado:**
- ✅ Aplicación inicia en https://localhost:5001
- ✅ No hay excepciones
- ✅ Server está escuchando

**Verificación:**
- [ ] Abrir en navegador: `https://localhost:5001`
- [ ] Deberá redirigir a `/Autenticacion`
- [ ] Página carga correctamente

**Resultado Obtenido:** ⏳ PENDIENTE (Ejecutar manualmente)

---

## 🔐 PRUEBAS DE AUTENTICACIÓN

### Test 3: Login Exitoso - Credenciales Válidas

**Pasos:**
1. Acceder a: `https://localhost:5001/Autenticacion`
2. Ingresar:
   - Número Cuenta: `1001`
   - PIN: `1234`
3. Clic en "Iniciar Sesión"

**Resultado Esperado:**
- ✅ Redirección a `/Principal`
- ✅ Mensaje: "Bienvenido, Juan García"
- ✅ Navbar muestra: "Juan García | Cuenta: 1001"
- ✅ Sesión creada correctamente

**Checklist:**
- [ ] Página de menú principal aparece
- [ ] Datos del usuario visibles en navbar
- [ ] 5 opciones de menú disponibles
- [ ] URL es `/Principal/`

**Estado:** ⏳ PENDIENTE

---

### Test 4: Login Fallido - PIN Incorrecto

**Pasos:**
1. Acceder a login
2. Ingresar:
   - Número Cuenta: `1001`
   - PIN: `9999` (incorrecto)
3. Clic en "Iniciar Sesión"

**Resultado Esperado:**
- ✅ Permanece en página login
- ✅ Mensaje de error: "PIN incorrecto"
- ✅ Campos de entrada se limpian
- ✅ No se crea sesión

**Checklist:**
- [ ] Alert rojo visible
- [ ] Mensaje contiene "PIN incorrecto"
- [ ] Sesión NO se crea
- [ ] URL sigue siendo `/Autenticacion`

**Estado:** ⏳ PENDIENTE

---

### Test 5: Login Fallido - Cuenta No Existe

**Pasos:**
1. Acceder a login
2. Ingresar:
   - Número Cuenta: `9999` (inexistente)
   - PIN: `1234`
3. Clic en "Iniciar Sesión"

**Resultado Esperado:**
- ✅ Permanece en login
- ✅ Mensaje: "Cuenta no encontrada"
- ✅ No se crea sesión

**Checklist:**
- [ ] Alert rojo visible
- [ ] Mensaje específico
- [ ] No redirige a menú

**Estado:** ⏳ PENDIENTE

---

### Test 6: Login Fallido - Campos Vacíos

**Pasos:**
1. Acceder a login
2. Dejar campos vacíos
3. Clic en "Iniciar Sesión"

**Resultado Esperado:**
- ✅ Campos marcan como requeridos
- ✅ No envía form
- ✅ Validación HTML5

**Estado:** ⏳ PENDIENTE

---

## 💰 PRUEBAS DE OPERACIONES BANCARIAS

### Test 7: Consultar Saldo

**Pasos:**
1. Loguear con: Cuenta 1001 | PIN 1234
2. Clic en "💰 Consultar Saldo"

**Resultado Esperado:**
- ✅ Pantalla muestra saldo: **$5,000.00**
- ✅ Cuenta: 1001
- ✅ Titular: Juan García
- ✅ Botón "Volver al Menú"

**Checklist:**
- [ ] Monto correcto
- [ ] Datos del titular visibles
- [ ] Número de cuenta visible

**Estado:** ⏳ PENDIENTE

---

### Test 8: Retiro Exitoso

**Pasos:**
1. Loguear: 1002 | 5678
2. Ir a "📤 Realizar Retiro"
3. Ingresar monto: `500`
4. Clic "Confirmar Retiro"

**Resultado Esperado:**
- ✅ Mensaje: "Retiro realizado exitosamente"
- ✅ Redirección a menú principal
- ✅ Saldo actualizado a: $8,000.00

**Verificación:**
1. Consultar saldo → Debe ser $8,000.00
2. Ver historial → Debe mostrar transacción
3. Loguear otra cuenta → No afecta su saldo

**Checklist:**
- [ ] Mensaje de éxito (verde)
- [ ] Saldo actualizado
- [ ] En historial aparece transacción

**Estado:** ⏳ PENDIENTE

---

### Test 9: Retiro Fallido - Saldo Insuficiente

**Pasos:**
1. Loguear: 1001 | 1234 (saldo: $5,000)
2. Ir a "Retiro"
3. Ingresar: `10,000` (más que el saldo)
4. Confirmar

**Resultado Esperado:**
- ✅ Mensaje error: "Saldo insuficiente"
- ✅ No redirige
- ✅ Saldo NO cambia

**Verificación:**
- Consultar saldo → Debe seguir siendo $5,000.00

**Estado:** ⏳ PENDIENTE

---

### Test 10: Retiro Fallido - Monto Inválido

**Pasos:**
1. Loguear
2. Ir a retiro
3. Intentar:
   - Monto: `-100` (negativo)
   - Monto: `0`
   - Monto: `abc` (texto)

**Resultado Esperado:**
- ✅ Validación HTML5 previene entrada inválida
- ✅ No permite números negativos o cero

**Estado:** ⏳ PENDIENTE

---

### Test 11: Depósito Exitoso

**Pasos:**
1. Loguear: 1003 | 9012 (saldo: $12,000)
2. Ir a "📥 Realizar Depósito"
3. Ingresar: `2,000`
4. Confirmar

**Resultado Esperado:**
- ✅ Mensaje: "Depósito realizado exitosamente"
- ✅ Nuevo saldo: $14,000.00
- ✅ Transacción en historial

**Verificación:**
1. Consultar saldo → $14,000.00
2. Historial → Muestra depósito

**Estado:** ⏳ PENDIENTE

---

### Test 12: Transferencia Exitosa

**Pasos:**
1. Loguear: 1001 | 1234
2. Ir a "💸 Transferencia"
3. Seleccionar destino: "Cuenta 1002 - María López"
4. Monto: `1,000`
5. Confirmar

**Resultado Esperado:**
- ✅ Mensaje éxito
- ✅ Saldo de 1001: $4,000.00 (5000 - 1000)
- ✅ Saldo de 1002: $9,500.00 (8500 + 1000)
- ✅ Ambas cuentas en historial

**Verificación:**
1. Loguear 1001 → Saldo: $4,000
2. Loguear 1002 → Saldo: $9,500
3. Historial de 1001 → Mostrar transferencia salida
4. Historial de 1002 → Mostrar transferencia entrada

**Estado:** ⏳ PENDIENTE

---

### Test 13: Transferencia Fallida - Misma Cuenta

**Pasos:**
1. Loguear: 1001 | 1234
2. Transferencia
3. Seleccionar: "Cuenta 1001 - Juan García" (misma)
4. Monto: 100

**Resultado Esperado:**
- ✅ Mensaje error: "No se puede transferir a la misma cuenta"
- ✅ Saldo NO cambia

**Estado:** ⏳ PENDIENTE

---

### Test 14: Historial de Transacciones

**Pasos:**
1. Loguear: 1002 | 5678
2. Clic "📋 Historial"

**Resultado Esperado:**
- ✅ Tabla visible
- ✅ Columnas: Fecha | Tipo | Descripción | Monto | Saldo Anterior | Saldo Nuevo
- ✅ Transacciones ordenadas (más recientes primero)
- ✅ Badges de color: 📤 Rojo | 📥 Verde | 💸 Azul

**Datos Esperados:**
- Mostrar última transacción (retiro o transferencia)
- Saldos correctos
- Fechas ordenadas descendente

**Checklist:**
- [ ] Tabla visible
- [ ] Mínimo 1 transacción
- [ ] Datos correctos
- [ ] Badges visibles

**Estado:** ⏳ PENDIENTE

---

## 🔒 PRUEBAS DE SEGURIDAD

### Test 15: Protección de Rutas - Sin Sesión

**Pasos:**
1. Sin loguear
2. Intentar acceder a:
   - `https://localhost:5001/Principal/`
   - `https://localhost:5001/Principal/ConsultarSaldo`
   - `https://localhost:5001/Principal/Retiro`

**Resultado Esperado:**
- ✅ Redirección a `/Autenticacion`
- ✅ No pueden acceder sin sesión
- ✅ Mensaje error visible

**Estado:** ⏳ PENDIENTE

---

### Test 16: Logout

**Pasos:**
1. Loguear: 1001 | 1234
2. Clic "Cerrar Sesión"

**Resultado Esperado:**
- ✅ Sesión se cierra
- ✅ Redirección a login
- ✅ Mensaje: "Sesión cerrada correctamente"
- ✅ No puede acceder a Principal

**Verificación:**
- Después de logout, intentar acceder a /Principal → Redirige a login

**Estado:** ⏳ PENDIENTE

---

### Test 17: Timeout de Sesión

**Pasos:**
1. Loguear
2. Esperar 30+ minutos (o simular modificando appsettings)
3. Intentar hacer operación

**Resultado Esperado:**
- ✅ Sesión expira
- ✅ Redirección a login

**Nota:** Para testing rápido, reducir timeout en appsettings.json

**Estado:** ⏳ PENDIENTE

---

## 💻 PRUEBAS DE INTERFAZ CONSOLA

### Test 18: Inicio de Consola

**Pasos:**
```bash
cd Cajero.Consola
dotnet run
```

**Resultado Esperado:**
- ✅ Menú inicial visible
- ✅ Solicita número de cuenta
- ✅ Solicita PIN
- ✅ No hay excepciones

**Estado:** ⏳ PENDIENTE

---

### Test 19: Login Consola - Exitoso

**Pasos:**
1. Ingresar cuenta: `1001`
2. Ingresar PIN: `1234`

**Resultado Esperado:**
- ✅ Mensaje: "✓ Autenticación exitosa"
- ✅ Menú principal aparece
- ✅ Muestra nombre: "Juan García"

**Estado:** ⏳ PENDIENTE

---

### Test 20: Operaciones Consola

**Pasos (Para cada operación):**

1. **Consultar Saldo**
   - Seleccionar opción 1
   - Resultado: Saldo $5,000.00

2. **Retiro**
   - Seleccionar opción 2
   - Ingresar: 500
   - Resultado: Saldo nuevo $4,500.00

3. **Depósito**
   - Seleccionar opción 3
   - Ingresar: 1000
   - Resultado: Saldo nuevo $5,500.00

4. **Transferencia**
   - Seleccionar opción 4
   - Seleccionar cuenta: 2
   - Ingresar: 1000
   - Resultado: Exitoso

5. **Historial**
   - Seleccionar opción 5
   - Resultado: Mostrar últimas transacciones

6. **Logout**
   - Seleccionar opción 6
   - Resultado: Volver a pantalla login

**Estado:** ⏳ PENDIENTE

---

## 🎨 PRUEBAS DE INTERFAZ DE USUARIO

### Test 21: Diseño Responsivo - Web

**Pasos:**
1. Abrir en navegador: https://localhost:5001
2. Presionar F12 (DevTools)
3. Activar vista responsive
4. Probar en:
   - Mobile (375px)
   - Tablet (768px)
   - Desktop (1920px)

**Verificaciones:**
- [ ] Navbar se adapta
- [ ] Menú se colapsa en mobile
- [ ] Cards se apilan correctamente
- [ ] Formularios son legibles
- [ ] Botones clickeables en mobile
- [ ] Tabla de historial scrollea horizontalmente

**Estado:** ⏳ PENDIENTE

---

### Test 22: Elementos Visuales

**Verificaciones Web:**
- [ ] Logo y favicon visible
- [ ] Colores del tema (azul)
- [ ] Emojis están presentes
- [ ] Textos legibles
- [ ] Alineación correcta
- [ ] Sin elementos cortados
- [ ] Alerts (éxito/error) visibles

**Estado:** ⏳ PENDIENTE

---

## 🐛 PRUEBAS DE VALIDACIÓN

### Test 23: Validación de Números

**Pasos:**
Intentar ingresar en campo numérico:
- `-100`
- `0`
- `999999999.99`
- `abc`
- `@#$%`

**Resultado Esperado:**
- ✅ Solo acepta números positivos
- ✅ Decimales correctos
- ✅ Validación HTML5

**Estado:** ⏳ PENDIENTE

---

### Test 24: Límites de Datos

**Pasos:**
1. Retiro: Monto = 0
2. Depósito: Monto negativo
3. Transferencia: Monto = 99999999

**Resultado Esperado:**
- ✅ Validaciones previenen datos inválidos
- ✅ Mensajes claros

**Estado:** ⏳ PENDIENTE

---

## 📊 PRUEBAS DE INTEGRIDAD DE DATOS

### Test 25: Consistencia Entre Cuentas

**Pasos:**
1. Loguear con: 1001
2. Consultar saldo inicial: $5,000
3. Hacer transferencia a 1002: $1,000
4. Loguear 1002
5. Verificar saldo aumentó $1,000

**Resultado Esperado:**
- ✅ Dinero trasferido correctamente
- ✅ Ambas cuentas reflejan cambio
- ✅ Sin duplicados ni pérdida de datos

**Verificación:**
- 1001: $4,000 (5000 - 1000)
- 1002: $9,500 (8500 + 1000)

**Estado:** ⏳ PENDIENTE

---

### Test 26: Historial Consistente

**Pasos:**
1. Hacer operación
2. Ir a historial
3. Verificar datos

**Resultado Esperado:**
- ✅ Todas las operaciones registradas
- ✅ Montos correctos
- ✅ Saldos correctos
- ✅ Fechas correctas

**Estado:** ⏳ PENDIENTE

---

## 🔥 PRUEBAS DE RENDIMIENTO

### Test 27: Tiempo de Respuesta

**Pasos:**
1. Abrir DevTools (F12)
2. Ir a Network
3. Realizar operación
4. Medir tiempo de respuesta

**Resultado Esperado:**
- ✅ Login: < 500ms
- ✅ Operaciones: < 200ms
- ✅ Página se carga: < 1s

**Estado:** ⏳ PENDIENTE

---

### Test 28: Estabilidad

**Pasos:**
1. Realizar 10 retiros seguidos
2. Realizar 10 depósitos seguidos
3. Realizar 10 transferencias seguidas

**Resultado Esperado:**
- ✅ Sin errores
- ✅ Sin degradación
- ✅ Datos consistentes

**Estado:** ⏳ PENDIENTE

---

## 📝 PRUEBAS DE CASOS EXTREMOS

### Test 29: Retiro de Todo el Saldo

**Pasos:**
1. Loguear: 1001 (saldo: $5,000)
2. Retiro: $5,000

**Resultado Esperado:**
- ✅ Saldo final: $0.00
- ✅ Operación exitosa
- ✅ Puede seguir usando cuenta

**Estado:** ⏳ PENDIENTE

---

### Test 30: Múltiples Transacciones Rápido

**Pasos:**
1. Retiro: $100
2. Depósito: $200
3. Retiro: $50
4. Transferencia: $75 a otra cuenta

**Resultado Esperado:**
- ✅ Todas registradas
- ✅ Saldo final correcto
- ✅ Orden cronológico

**Estado:** ⏳ PENDIENTE

---

## 📋 RESUMEN DE PRUEBAS

### Matriz de Testing

| # | Test | Resultado | Observaciones |
|---|------|-----------|--------------|
| 1 | Build | ✅ PASÓ | Compilación correcta |
| 2 | Web Startup | ⏳ PENDIENTE | |
| 3 | Login Válido | ⏳ PENDIENTE | |
| 4 | Login PIN Inválido | ⏳ PENDIENTE | |
| 5 | Login Cuenta No Existe | ⏳ PENDIENTE | |
| 6 | Login Vacío | ⏳ PENDIENTE | |
| 7 | Consultar Saldo | ⏳ PENDIENTE | |
| 8 | Retiro Exitoso | ⏳ PENDIENTE | |
| 9 | Retiro Saldo Insuficiente | ⏳ PENDIENTE | |
| 10 | Retiro Monto Inválido | ⏳ PENDIENTE | |
| 11 | Depósito Exitoso | ⏳ PENDIENTE | |
| 12 | Transferencia Exitosa | ⏳ PENDIENTE | |
| 13 | Transferencia Misma Cuenta | ⏳ PENDIENTE | |
| 14 | Historial | ⏳ PENDIENTE | |
| 15 | Protección de Rutas | ⏳ PENDIENTE | |
| 16 | Logout | ⏳ PENDIENTE | |
| 17 | Timeout Sesión | ⏳ PENDIENTE | |
| 18 | Consola Inicio | ⏳ PENDIENTE | |
| 19 | Consola Login | ⏳ PENDIENTE | |
| 20 | Consola Operaciones | ⏳ PENDIENTE | |
| 21 | Responsivo | ⏳ PENDIENTE | |
| 22 | Elementos Visuales | ⏳ PENDIENTE | |
| 23 | Validación Números | ⏳ PENDIENTE | |
| 24 | Límites de Datos | ⏳ PENDIENTE | |
| 25 | Integridad Entre Cuentas | ⏳ PENDIENTE | |
| 26 | Historial Consistente | ⏳ PENDIENTE | |
| 27 | Rendimiento | ⏳ PENDIENTE | |
| 28 | Estabilidad | ⏳ PENDIENTE | |
| 29 | Retiro Todo Saldo | ⏳ PENDIENTE | |
| 30 | Múltiples Operaciones | ⏳ PENDIENTE | |

---

## 📊 RESULTADOS ESPERADOS

**Total de Pruebas:** 30
**Esperadas a Pasar:** 30
**% Éxito Esperado:** 100%

---

## 🔧 NOTAS DE TESTING

### Ambiente
- Limpiar cookies/cache antes de testing
- Usar navegador privado/incógnito
- Verificar logs en consola (F12)

### Credenciales
```
1001 | 1234 | Juan García    | $5,000.00
1002 | 5678 | María López    | $8,500.00
1003 | 9012 | Carlos M.      | $12,000.00
```

### Bugs Conocidos
(Será completado después de testing)

### Mejoras Futuras
(Será completado después de testing)

---

## ✍️ Firmado

**Tester:** ___________________
**Fecha:** ___________________
**Resultado Final:** ___________________

---

**¡Que comience el testing! 🚀**
