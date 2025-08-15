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


## Lógica de sincronización de productos y catálogos

- **Subida idempotente:** Solo se suben productos, líneas, marcas e impuestos que no han sido exportados (campo `exportado = 0`).
- **Actualización vs alta:**
  - Si el producto ya existe en la nube (GET exitoso), se hace PUT (actualización).
  - Si no existe, se hace POST (alta).
- **Marcado inmediato:** Tras un POST/PUT exitoso, el producto se marca como exportado inmediatamente en la base local.
- **Evita duplicados:** Si un POST responde 422 (clave ya existe o error de validación), no se reintenta ni se marca como exportado.
- **Validaciones automáticas:**
  - Si el campo `unidad` está vacío, se envía como "PZA".
  - Si el campo `claveprodserv` está vacío, se envía como "01010101".
  - Si el campo `claveunidad` está vacío, se envía como "H87".
- **Catálogos auxiliares:** Líneas, marcas e impuestos se crean automáticamente si no existen en la nube y se marcan como exportados tras POST exitoso.
- **Sincronización descendente:**
  - Descarga productos pendientes desde la nube y actualiza la base local.
  - Confirma cada producto actualizado ante el API.
- **Logs detallados:** Cada operación relevante (POST, PUT, errores, validaciones) queda registrada en archivos de log.


## Requisitos
- Windows 10/11
- .NET Framework 4.7.2 y/o .NET 8
- Visual Studio 2022 o superior


## Quickstart (Onboarding en 5 minutos)

1. Clona el repositorio:
  ```
  git clone https://github.com/dexter1521/inube-sync.git
  ```

2. Copia el archivo de ejemplo de configuración:
  ```
  cp appsettings.example.json appsettings.json
  ```
  Edita los valores de `ApiUrl`, `DeviceToken`, y la cadena de conexión SQL según tu entorno.

3. Abre `SincronizadorSolucion.sln` en Visual Studio.

4. Compila la solución y ejecuta el proyecto deseado (UI, Worker, etc.).

5. Los logs diarios se guardan en la carpeta configurada (`LogsPath`).

¡Listo! Tu entorno estará funcionando en minutos.

## Licencia
Este proyecto es propiedad de inube. Uso interno y privado.

---

Para dudas o soporte, contacta al equipo de desarrollo de inube.
