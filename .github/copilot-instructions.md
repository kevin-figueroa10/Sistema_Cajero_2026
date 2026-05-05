# Copilot Instructions

## Directrices del proyecto
- En Razor (.cshtml), evitar multiplicaciones directas entre decimal y double. Usar casting explícito: ((decimal)ViewBag.Valor * 0.015m) en lugar de (ViewBag.Valor * 0.015). Esto previene RuntimeBinderException.