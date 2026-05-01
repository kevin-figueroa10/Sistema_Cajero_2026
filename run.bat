@echo off
REM Script para ejecutar el Sistema Cajero (Windows)

cls
echo.
echo ╔═══════════════════════════════════════════════════════════╗
echo ║         Sistema Cajero Automático 2026                   ║
echo ║         Script de Ejecución (Windows)                    ║
echo ╚═══════════════════════════════════════════════════════════╝
echo.

REM Verificar que .NET está instalado
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo Error: .NET SDK no está instalado.
    echo Descárgalo en: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo ✓ .NET SDK detectado
echo.
echo Selecciona qué deseas ejecutar:
echo 1) Aplicación Web (ASP.NET Core MVC)
echo 2) Interfaz de Consola
echo 3) Compilar solución
echo 4) Restaurar paquetes NuGet
echo 5) Ejecutar ambas
echo.

set /p option="Ingresa tu opción (1-5): "

if "%option%"=="1" (
    echo Iniciando Aplicación Web...
    cd Cajero.Web
    dotnet run
) else if "%option%"=="2" (
    echo Iniciando Interfaz de Consola...
    cd Cajero.Consola
    dotnet run
) else if "%option%"=="3" (
    echo Compilando solución...
    dotnet build
) else if "%option%"=="4" (
    echo Restaurando paquetes NuGet...
    dotnet restore
) else if "%option%"=="5" (
    echo Compilando...
    dotnet build
    echo.
    echo Iniciando Aplicación Web...
    start cmd /k "cd Cajero.Web && dotnet run"
    echo ✓ Aplicación Web iniciada en https://localhost:5001
    echo.
    pause
    cd Cajero.Consola
    dotnet run
) else (
    echo Opción inválida
    pause
    exit /b 1
)

echo.
echo ✓ Operación completada
pause
