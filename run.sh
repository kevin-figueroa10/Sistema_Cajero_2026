#!/bin/bash
# Script para ejecutar el Sistema Cajero

echo "╔═══════════════════════════════════════════════════════════╗"
echo "║         Sistema Cajero Automático 2026                   ║"
echo "║         Script de Ejecución                               ║"
echo "╚═══════════════════════════════════════════════════════════╝"
echo ""

# Color codes
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Verificar que .NET está instalado
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}❌ Error: .NET SDK no está instalado.${NC}"
    echo "Descárgalo en: https://dotnet.microsoft.com/download"
    exit 1
fi

echo -e "${GREEN}✓ .NET SDK detectado$(dotnet --version)${NC}"
echo ""

# Opciones de ejecución
echo "Selecciona qué deseas ejecutar:"
echo "1) Aplicación Web (ASP.NET Core MVC)"
echo "2) Interfaz de Consola"
echo "3) Compilar solución"
echo "4) Restaurar paquetes NuGet"
echo "5) Ejecutar ambas"
echo ""
read -p "Ingresa tu opción (1-5): " option

case $option in
    1)
        echo -e "${BLUE}Iniciando Aplicación Web...${NC}"
        cd Cajero.Web
        dotnet run
        ;;
    2)
        echo -e "${BLUE}Iniciando Interfaz de Consola...${NC}"
        cd Cajero.Consola
        dotnet run
        ;;
    3)
        echo -e "${BLUE}Compilando solución...${NC}"
        dotnet build
        ;;
    4)
        echo -e "${BLUE}Restaurando paquetes NuGet...${NC}"
        dotnet restore
        ;;
    5)
        echo -e "${BLUE}Compilando...${NC}"
        dotnet build
        echo ""
        echo -e "${BLUE}Iniciando Interfaz Web...${NC}"
        cd Cajero.Web
        dotnet run &
        echo -e "${GREEN}✓ Aplicación Web iniciada en https://localhost:5001${NC}"
        echo ""
        read -p "Presiona ENTER para iniciar la Consola también..."
        cd ../Cajero.Consola
        dotnet run
        ;;
    *)
        echo -e "${RED}Opción inválida${NC}"
        exit 1
        ;;
esac

echo ""
echo -e "${GREEN}✓ Operación completada${NC}"
