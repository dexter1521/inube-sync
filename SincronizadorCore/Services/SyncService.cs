

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SincronizadorCore.Models;
using SincronizadorCore.Utils;


namespace SincronizadorCore.Services
{
	public class SyncService
	{
		private readonly HttpClient _httpClient;
		private readonly AppSettings _settings;

		public SyncService(AppSettings settings)
		{
			_settings = settings;
			// Eliminar /api/ final si existe para evitar dobles /api/
			var apiUrl = _settings.ApiUrl?.TrimEnd('/');
			if (apiUrl != null && apiUrl.EndsWith("/api"))
				apiUrl = apiUrl.Substring(0, apiUrl.Length - 4);
			_httpClient = new HttpClient
			{
				BaseAddress = new Uri(apiUrl ?? string.Empty)
			};
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", _settings.ApiPassword);
		}

		// Sincronizar datos locales hacia la nube
		public async Task SincronizarHaciaNubeAsync()
		{
			try
			{

				// Subir productos

				// Obtener catálogos locales no exportados para referencia rápida
				var lineasNoExportadas = SqlHelper.ObtenerLineasNoExportadas(_settings.ConnectionStrings);
				var marcasNoExportadas = SqlHelper.ObtenerMarcasNoExportadas(_settings.ConnectionStrings);
				var impuestosLocales = SqlHelper.ObtenerTodosImpuestos(_settings.ConnectionStrings);

				// HashSets para evitar crear repetidamente
				var lineasVerificadas = new HashSet<string>();
				var marcasVerificadas = new HashSet<string>();
				var impuestosVerificados = new HashSet<string>();

				bool primeraVuelta = true;
				List<ProductoModel> productosOrdenados = new List<ProductoModel>();
				while (true)
				{
					var productos = SqlHelper.ObtenerTodosProductos(_settings.ConnectionStrings);
					if (productos == null || productos.Count == 0)
						break;
					// Ordenar solo la primera vez para mantener el ciclo incremental
					if (primeraVuelta)
					{
						productosOrdenados = new List<ProductoModel>(productos);
						productosOrdenados.Sort((a, b) =>
						{
							int cmp = string.Compare(a.linea, b.linea, StringComparison.OrdinalIgnoreCase);
							if (cmp != 0) return cmp;
							return string.Compare(a.marca, b.marca, StringComparison.OrdinalIgnoreCase);
						});
						primeraVuelta = false;
					}
					// Buscar el primer producto pendiente en el orden establecido
					ProductoModel? producto = productosOrdenados.FirstOrDefault(p => productos.Exists(x => x.articulo == p.articulo));
					if (producto == null)
						break;
					var productoNoNull = producto!;
					// 1. Validar/crear línea solo si no se ha verificado
					if (!string.IsNullOrWhiteSpace(productoNoNull.linea) && !lineasVerificadas.Contains(productoNoNull.linea))
					{
						var getLinea = await _httpClient.GetAsync($"/api/lineas/{Uri.EscapeDataString(productoNoNull.linea)}");
						if (!getLinea.IsSuccessStatusCode)
						{
							lineasNoExportadas = SqlHelper.ObtenerLineasNoExportadas(_settings.ConnectionStrings);
							var lineaLocal = lineasNoExportadas.Find(l => l.Linea == productoNoNull.linea);
							if (lineaLocal != null)
							{
								var lineaContent = new StringContent(JsonSerializer.Serialize(lineaLocal), Encoding.UTF8, "application/json");
								var postLinea = await _httpClient.PostAsync("/api/lineas", lineaContent);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Línea auto-creada para producto {productoNoNull.articulo}: {productoNoNull.linea} - {postLinea.StatusCode}");
								if (postLinea.IsSuccessStatusCode)
								{
									SqlHelper.MarcarLineasComoExportadas(new List<string> { productoNoNull.linea }, _settings.ConnectionStrings);
									LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Línea marcada como exportada: {productoNoNull.linea}");
									// Refrescar la lista después de marcar como exportada
									lineasNoExportadas = SqlHelper.ObtenerLineasNoExportadas(_settings.ConnectionStrings);
								}
							}
						}
						lineasVerificadas.Add(productoNoNull.linea);
					}
					// (Eliminado control de productosProcesados, ya no es necesario)
					{
						var getMarca = await _httpClient.GetAsync($"/api/marcas/{Uri.EscapeDataString(productoNoNull.marca)}");
						if (!getMarca.IsSuccessStatusCode)
						{
							marcasNoExportadas = SqlHelper.ObtenerMarcasNoExportadas(_settings.ConnectionStrings);
							var marcaLocal = marcasNoExportadas.Find(m => m.Marca == productoNoNull.marca);
							if (marcaLocal != null)
							{
								var marcaContent = new StringContent(JsonSerializer.Serialize(marcaLocal), Encoding.UTF8, "application/json");
								var postMarca = await _httpClient.PostAsync("/api/marcas", marcaContent);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Marca auto-creada para producto {productoNoNull.articulo}: {productoNoNull.marca} - {postMarca.StatusCode}");
								if (postMarca.IsSuccessStatusCode)
								{
									SqlHelper.MarcarMarcasComoExportadas(new List<string> { productoNoNull.marca }, _settings.ConnectionStrings);
									LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Marca marcada como exportada: {productoNoNull.marca}");
									// Refrescar la lista después de marcar como exportada
									marcasNoExportadas = SqlHelper.ObtenerMarcasNoExportadas(_settings.ConnectionStrings);
								}
							}
						}
						marcasVerificadas.Add(productoNoNull.marca);
					}

					// 3. Validar/crear impuesto solo si no se ha verificado
					if (!string.IsNullOrWhiteSpace(productoNoNull.impuesto) && !impuestosVerificados.Contains(productoNoNull.impuesto))
					{
						var getImpuesto = await _httpClient.GetAsync($"/api/impuestos/{Uri.EscapeDataString(productoNoNull.impuesto)}");
						if (!getImpuesto.IsSuccessStatusCode)
						{
							var impuestoLocal = impuestosLocales.Find(i => i.Impuesto == productoNoNull.impuesto);
							if (impuestoLocal != null)
							{
								var impuestoContent = new StringContent(JsonSerializer.Serialize(impuestoLocal), Encoding.UTF8, "application/json");
								var postImpuesto = await _httpClient.PostAsync("/api/impuestos", impuestoContent);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Impuesto auto-creado para producto {productoNoNull.articulo}: {productoNoNull.impuesto} - {postImpuesto.StatusCode}");
							}
						}
						impuestosVerificados.Add(productoNoNull.impuesto);
					}

					// 4. Subir producto usando ProductoUploadModel
					var upload = new ProductoUploadModel
					{
						Clave = productoNoNull.articulo,
						Descripcion = productoNoNull.descripcion,
						Precio = productoNoNull.precio1,
						Linea = productoNoNull.linea,
						Marca = productoNoNull.marca,
						Fabricante = productoNoNull.fabricante,
						Ubicacion = productoNoNull.ubicacion,
						Unidad = string.IsNullOrWhiteSpace(productoNoNull.unidad) ? "PZA" : productoNoNull.unidad,
						Bloqueado = productoNoNull.bloqueado,
						ParaVenta = productoNoNull.paraventa,
						Invent = productoNoNull.invent,
						Granel = productoNoNull.granel,
						Speso = productoNoNull.speso,
						BajoCosto = productoNoNull.bajocosto,
						Impuesto = productoNoNull.impuesto,
						Existencia = productoNoNull.existencia,
						Precio2 = productoNoNull.precio2,
						Precio3 = productoNoNull.precio3,
						Precio4 = productoNoNull.precio4,
						Precio5 = productoNoNull.precio5,
						Precio6 = productoNoNull.precio6,
						Precio7 = productoNoNull.precio7,
						Precio8 = productoNoNull.precio8,
						Precio9 = productoNoNull.precio9,
						Precio10 = productoNoNull.precio10,
						U1 = productoNoNull.u1.ToString(),
						U2 = productoNoNull.u2.ToString(),
						U3 = productoNoNull.u3.ToString(),
						U4 = productoNoNull.u4.ToString(),
						U5 = productoNoNull.u5.ToString(),
						U6 = productoNoNull.u6.ToString(),
						U7 = productoNoNull.u7.ToString(),
						U8 = productoNoNull.u8.ToString(),
						U9 = productoNoNull.u9.ToString(),
						U10 = productoNoNull.u10.ToString(),
						C2 = productoNoNull.c2.ToString(),
						C3 = productoNoNull.c3.ToString(),
						C4 = productoNoNull.c4.ToString(),
						C5 = productoNoNull.c5.ToString(),
						C6 = productoNoNull.c6.ToString(),
						C7 = productoNoNull.c7.ToString(),
						C8 = productoNoNull.c8.ToString(),
						C9 = productoNoNull.c9.ToString(),
						C10 = productoNoNull.c10.ToString(),
						CostoUltimo = productoNoNull.costoultimo,
						ClaveProdServ = string.IsNullOrWhiteSpace(productoNoNull.claveprodserv) ? "01010101" : productoNoNull.claveprodserv,
						ClaveUnidad = string.IsNullOrWhiteSpace(productoNoNull.claveunidad) ? "H87" : productoNoNull.claveunidad
					};
					var content = new StringContent(JsonSerializer.Serialize(upload), Encoding.UTF8, "application/json");
					var getResp = await _httpClient.GetAsync($"/api/productos/{Uri.EscapeDataString(producto.articulo)}");
					if (getResp.IsSuccessStatusCode)
					{
						// Solo PUT si el producto ya existe en la nube
						var putResp = await _httpClient.PutAsync($"/api/productos/{Uri.EscapeDataString(producto.articulo)}", content);
						var putMsg = putResp.IsSuccessStatusCode ? "Actualizado" : "Error al actualizar";
						var putError = putResp.IsSuccessStatusCode ? "" : $" | Error: {await putResp.Content.ReadAsStringAsync()}";
						LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Producto {producto.articulo} (PUT): {putResp.StatusCode} - {putMsg}{putError}");
						if (putResp.IsSuccessStatusCode)
						{
							SqlHelper.MarcarProductosComoExportados(new List<string> { producto.articulo }, _settings.ConnectionStrings);
							LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Producto marcado como exportado: {producto.articulo}");
						}
					}
					else
					{
						// Solo POST si el producto no existe en la nube
						var postResp = await _httpClient.PostAsync("/api/productos", content);
						var postMsg = postResp.IsSuccessStatusCode ? "Registrado" : "Error al registrar";
						var postError = postResp.IsSuccessStatusCode ? "" : $" | Error: {await postResp.Content.ReadAsStringAsync()}";
						if (!postResp.IsSuccessStatusCode)
						{
							LogService.WriteLog(_settings.LogDirectory, $"[DEBUG] JSON enviado para {producto.articulo}: {JsonSerializer.Serialize(upload)}");
							// Si el error es 422, marcar como exportado y continuar
							if ((int)postResp.StatusCode == 422)
							{
								SqlHelper.MarcarProductosComoExportados(new List<string> { producto.articulo }, _settings.ConnectionStrings);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Producto {producto.articulo} (POST): 422 - Ya existe, marcado como exportado localmente.");
								continue;
							}
						}
						LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Producto {producto.articulo} (POST): {postResp.StatusCode} - {postMsg}{postError}");
						if (postResp.IsSuccessStatusCode)
						{
							SqlHelper.MarcarProductosComoExportados(new List<string> { producto.articulo }, _settings.ConnectionStrings);
							LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Producto marcado como exportado: {producto.articulo}");
						}
					}
				}


				LogService.WriteLog(_settings.LogDirectory, "[UPLOAD] Sincronización hacia la nube finalizada.");
			}
			catch (Exception ex)
			{
				LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Error al sincronizar hacia la nube: {ex.Message}");
			}
		}

		public async Task<List<ProductoModel>> ObtenerProductosDesdeApiAsync()
		{
			var productos = new List<ProductoModel>();

			try
			{
				_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", _settings.ApiPassword);

				var response = await _httpClient.GetAsync("/api/productos");

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
					productos = JsonSerializer.Deserialize<List<ProductoModel>>(json,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ProductoModel>();

					LogService.WriteLog(_settings.LogDirectory, $"[API] Se obtuvieron {productos.Count} productos.");
				}
				else
				{
					LogService.WriteLog(_settings.LogDirectory, $"[API] Error al obtener productos: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				LogService.WriteLog(_settings.LogDirectory, $"[API] Excepción: {ex.Message}");
			}

			return productos;
		}

		public async Task SincronizarDesdeApiAsync()
		{
			try
			{
				// 1. Consultar productos pendientes para este dispositivo
				var pendientesResponse = await _httpClient.GetAsync($"/api/prods_download/pendientes/{_settings.ApiUser}");
				if (!pendientesResponse.IsSuccessStatusCode)
				{
					LogService.WriteLog(_settings.LogDirectory, $"[API] Error al obtener pendientes: {pendientesResponse.StatusCode}");
					return;
				}

				var pendientesJson = await pendientesResponse.Content.ReadAsStringAsync();
				var pendientes = JsonSerializer.Deserialize<List<SincronizadorCore.Models.ProdPendienteModel>>(pendientesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<SincronizadorCore.Models.ProdPendienteModel>();

				if (pendientes.Count == 0)
				{
					LogService.WriteLog(_settings.LogDirectory, "[SYNC] No hay productos pendientes para actualizar precios.");
					return;
				}

				// 2. Obtener todos los productos de la API
				var productosApi = await ObtenerProductosDesdeApiAsync();

				// 3. Filtrar solo los productos pendientes
				var clavesPendientes = new HashSet<string>(pendientes.Select(p => p.Clave));
				var productosActualizar = productosApi.Where(p => clavesPendientes.Contains(p.articulo)).ToList();

				foreach (var producto in productosActualizar)
				{
					// Validar y registrar/actualizar línea
					var linea = new LineaModel { Linea = producto.linea ?? "SYS", Descrip = producto.linea ?? "" };
					SqlHelper.InsertarOActualizarLinea(linea, _settings.ConnectionStrings);

					// Validar y registrar/actualizar marca
					var marca = new MarcaModel { Marca = producto.marca ?? "SYS", Descrip = producto.marca ?? "" };
					SqlHelper.InsertarOActualizarMarca(marca, _settings.ConnectionStrings);

					// Validar y registrar/actualizar impuesto
					var impuesto = new ImpuestoModel { Impuesto = producto.impuesto ?? "SYS", Valor = 0 };
					SqlHelper.InsertarOActualizarImpuesto(impuesto, _settings.ConnectionStrings);

					// Actualizar producto
					SqlHelper.InsertarOActualizarProducto(producto, _settings.ConnectionStrings);

					// Confirmar la actualización con el endpoint POST
					var pendiente = pendientes.FirstOrDefault(p => p.Clave == producto.articulo);
					if (pendiente != null)
					{
						var confirmContent = new StringContent($"{{\"id\": {pendiente.Id}}}", Encoding.UTF8, "application/json");
						var confirmResponse = await _httpClient.PostAsync("/api/prods_download/aplicar", confirmContent);
						if (confirmResponse.IsSuccessStatusCode)
						{
							LogService.WriteLog(_settings.LogDirectory, $"[SYNC] Confirmado producto pendiente id={pendiente.Id} clave={pendiente.Clave}");
						}
						else
						{
							LogService.WriteLog(_settings.LogDirectory, $"[SYNC] Error al confirmar producto pendiente id={pendiente.Id} clave={pendiente.Clave}: {confirmResponse.StatusCode}");
						}
					}

					LogService.WriteLog(_settings.LogDirectory, $"[SYNC] Se actualizaron precios de {productosActualizar.Count} productos pendientes y se confirmó cada uno.");
				}
			}
			catch (Exception ex)
			{
				LogService.WriteLog(_settings.LogDirectory, $"[SYNC] Error en sincronización de precios pendientes: {ex.Message}");
			}
		}
		public async Task ConsultarDispositivosAsync()
		{
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", _settings.ApiPassword);

			var response = await _httpClient.GetAsync("/api/dispositivos");
			var result = await response.Content.ReadAsStringAsync();

			LogService.WriteLog(_settings.LogDirectory,
				$"GET dispositivos: {(int)response.StatusCode} - {result}");
		}

		public async Task SincronizarLineasDesdeApiAsync()
		{
			var response = await _httpClient.GetAsync("/api/lineas");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var lineasApi = JsonSerializer.Deserialize<List<LineaModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<LineaModel>();
				int count = 0;
				foreach (var linea in lineasApi)
				{
					// Solo usar los campos relevantes
					var lineaLocal = new LineaModel
					{
						Linea = linea.Linea ?? string.Empty,
						Descrip = linea.Descrip ?? string.Empty
					};
					SqlHelper.InsertarOActualizarLinea(lineaLocal, _settings.ConnectionStrings);
					count++;
				}
				LogService.WriteLog(_settings.LogDirectory, $"[API] Se sincronizaron {count} líneas.");
			}
			else
			{
				LogService.WriteLog(_settings.LogDirectory, $"[API] Error al obtener líneas: {response.StatusCode}");
			}
		}

		public async Task SincronizarMarcasDesdeApiAsync()
		{
			var response = await _httpClient.GetAsync("/api/marcas");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var marcasApi = JsonSerializer.Deserialize<List<MarcaModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<MarcaModel>();
				int count = 0;
				foreach (var marca in marcasApi)
				{
					// Solo usar los campos relevantes
					var marcaLocal = new MarcaModel
					{
						Marca = marca.Marca ?? string.Empty,
						Descrip = marca.Descrip ?? string.Empty
					};
					SqlHelper.InsertarOActualizarMarca(marcaLocal, _settings.ConnectionStrings);
					count++;
				}
				LogService.WriteLog(_settings.LogDirectory, $"[API] Se sincronizaron {count} marcas.");
			}
			else
			{
				LogService.WriteLog(_settings.LogDirectory, $"[API] Error al obtener marcas: {response.StatusCode}");
			}
		}

		public async Task SincronizarImpuestosDesdeApiAsync()
		{
			var response = await _httpClient.GetAsync("/api/impuestos");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var impuestosApi = JsonSerializer.Deserialize<List<ImpuestoModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ImpuestoModel>();
				int count = 0;
				foreach (var impuesto in impuestosApi)
				{
					// Solo usar los campos relevantes
					var impuestoLocal = new ImpuestoModel
					{
						Impuesto = impuesto.Impuesto ?? string.Empty,
						Valor = impuesto.Valor
					};
					SqlHelper.InsertarOActualizarImpuesto(impuestoLocal, _settings.ConnectionStrings);
					count++;
				}
				LogService.WriteLog(_settings.LogDirectory, $"[API] Se sincronizaron {count} impuestos.");
			}
			else
			{
				LogService.WriteLog(_settings.LogDirectory, $"[API] Error al obtener impuestos: {response.StatusCode}");
			}
		}
	}
}
