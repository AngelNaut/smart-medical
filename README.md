# SmartMedical - Onion Architecture

Se creó una base de arquitectura Onion con 4 proyectos y **sin clases iniciales**:

- `SmartMedical.Domain`: núcleo del dominio.
- `SmartMedical.Application`: casos de uso y contratos de aplicación.
- `SmartMedical.Infrastructure`: implementaciones técnicas.
- `SmartMedical.API`: punto de entrada (Web API).

## Reglas de dependencia

- `Domain` no depende de nadie.
- `Application` depende de `Domain`.
- `Infrastructure` depende de `Application` y `Domain`.
- `API` depende de `Application` y `Infrastructure`.
