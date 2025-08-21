# inube-sync

Sincronizador de productos y catálogos para entornos Windows, desarrollado en .NET y C#. Permite la gestión y sincronización de datos entre base local y API, con control de exportación, bloqueo y logs detallados.

## Proyectos incluidos

- **SincronizadorConfigUI**: Interfaz gráfica para configuración y administración.
- **SincronizadorConfigUIv8**: UI moderna compatible con .NET 8.
- **SincronizadorCore**: Lógica central, modelos y utilidades.
- **SincronizadorWorker**: Servicio de sincronización automatizada.

## Estructura principal

```
SincronizadorSolucion.sln         # Archivo de solución principal
SincronizadorConfigUI/            # UI de configuración clásica
SincronizadorConfigUIv8/          # UI de configuración moderna
SincronizadorCore/                # Lógica central y modelos
SincronizadorWorker/              # Servicio de sincronización
packages/                         # Paquetes NuGet
```

## Estado actual y novedades

- Primer versión funcional lista para pruebas y despliegue.
- Lectura y envío correcto del campo `bloqueado` en productos.
- Logs mejorados: ahora se distingue entre registros nuevos (`[REGISTER]`) y actualizaciones (`[UPLOAD]`).
- Se registra el JSON completo de productos bloqueados para auditoría temporal.
- Validación robusta de datos leídos desde SQL Server (soporte para `smallint`).

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
- **Logs detallados:** Cada operación relevante (POST, PUT, errores, validaciones) queda registrada en archivos de log. Los productos bloqueados se auditan temporalmente con su JSON completo.

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
4. Configura la conexión a la base local y API en los archivos `appsettings.json`.
5. Revisa los logs en la carpeta `Logs/` para auditoría y depuración.

## Recomendaciones para pruebas

- Verifica que los productos bloqueados se exporten correctamente y se registren en los logs.
- Revisa los logs para confirmar el flujo de registro y actualización.
- Realiza pruebas de sincronización ascendente y descendente.

## Licencia

Este proyecto es propiedad de inube. Uso interno y privado.

---

Para dudas o soporte, contacta al equipo de desarrollo de inube.
