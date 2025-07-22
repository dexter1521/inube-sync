# inube-sync

Solución de sincronización multiaplicación para entornos Windows, desarrollada en .NET y C#. Este repositorio contiene una suite de herramientas y servicios para la gestión y sincronización de configuraciones y datos entre diferentes componentes.

## Proyectos incluidos

- **SincronizadorConfigUI**: Aplicación de escritorio con interfaz gráfica para la configuración y administración de la sincronización.
- **SincronizadorConfigUIv8**: Versión alternativa/actualizada de la interfaz de configuración, compatible con .NET 8.
- **SincronizadorCore**: Núcleo de lógica de negocio y utilidades compartidas entre los proyectos.
- **SincronizadorWorker**: Servicio o aplicación de fondo encargado de ejecutar tareas de sincronización automatizadas.

## Estructura principal

```
SincronizadorSolucion.sln         # Archivo de solución principal
SincronizadorConfigUI/            # UI de configuración clásica
SincronizadorConfigUIv8/          # UI de configuración moderna
SincronizadorCore/                # Lógica central y modelos
SincronizadorWorker/              # Servicio de sincronización
packages/                         # Paquetes NuGet
```

## Características
- Sincronización de configuraciones y datos entre aplicaciones.
- Interfaces gráficas para administración y monitoreo.
- Servicio de sincronización automatizada.
- Modularidad y escalabilidad.

## Requisitos
- Windows 10/11
- .NET Framework 4.7.2 y/o .NET 8
- Visual Studio 2022 o superior

## Uso
1. Clona el repositorio:
   ```
   git clone https://github.com/dexter1521/inube-sync.git
   ```
2. Abre `SincronizadorSolucion.sln` en Visual Studio.
3. Compila la solución y ejecuta el proyecto deseado.

## Licencia
Este proyecto es propiedad de inube. Uso interno y privado.

---

Para dudas o soporte, contacta al equipo de desarrollo de inube.
