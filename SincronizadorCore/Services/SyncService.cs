

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

				while (true)
				{
					var productos = SqlHelper.ObtenerTodosProductos(_settings.ConnectionStrings);
					if (productos == null || productos.Count == 0)
						break;
					// Procesar solo el primer producto pendiente
					var producto = productos[0];
					// 1. Validar/crear línea solo si no se ha verificado
					if (!string.IsNullOrWhiteSpace(producto.linea) && !lineasVerificadas.Contains(producto.linea))
					{
						var getLinea = await _httpClient.GetAsync($"/api/lineas/{Uri.EscapeDataString(producto.linea)}");
						if (!getLinea.IsSuccessStatusCode)
						{
							var lineaLocal = lineasNoExportadas.Find(l => l.Linea == producto.linea);
							if (lineaLocal != null)
							{
								var lineaContent = new StringContent(JsonSerializer.Serialize(lineaLocal), Encoding.UTF8, "application/json");
								var postLinea = await _httpClient.PostAsync("/api/lineas", lineaContent);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Línea auto-creada para producto {producto.articulo}: {producto.linea} - {postLinea.StatusCode}");
								if (postLinea.IsSuccessStatusCode)
								{
									SqlHelper.MarcarLineasComoExportadas(new List<string> { producto.linea }, _settings.ConnectionStrings);
									LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Línea marcada como exportada: {producto.linea}");
								}
							}
						}
						lineasVerificadas.Add(producto.linea);
					}
					// (Eliminado control de productosProcesados, ya no es necesario)
					{
						var getMarca = await _httpClient.GetAsync($"/api/marcas/{Uri.EscapeDataString(producto.marca)}");
						if (!getMarca.IsSuccessStatusCode)
						{
							var marcaLocal = marcasNoExportadas.Find(m => m.Marca == producto.marca);
							if (marcaLocal != null)
							{
								var marcaContent = new StringContent(JsonSerializer.Serialize(marcaLocal), Encoding.UTF8, "application/json");
								var postMarca = await _httpClient.PostAsync("/api/marcas", marcaContent);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Marca auto-creada para producto {producto.articulo}: {producto.marca} - {postMarca.StatusCode}");
								if (postMarca.IsSuccessStatusCode)
								{
									SqlHelper.MarcarMarcasComoExportadas(new List<string> { producto.marca }, _settings.ConnectionStrings);
									LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Marca marcada como exportada: {producto.marca}");
								}
							}
						}
						marcasVerificadas.Add(producto.marca);
					}

					// 3. Validar/crear impuesto solo si no se ha verificado
					if (!string.IsNullOrWhiteSpace(producto.impuesto) && !impuestosVerificados.Contains(producto.impuesto))
					{
						var getImpuesto = await _httpClient.GetAsync($"/api/impuestos/{Uri.EscapeDataString(producto.impuesto)}");
						if (!getImpuesto.IsSuccessStatusCode)
						{
							var impuestoLocal = impuestosLocales.Find(i => i.Impuesto == producto.impuesto);
							if (impuestoLocal != null)
							{
								var impuestoContent = new StringContent(JsonSerializer.Serialize(impuestoLocal), Encoding.UTF8, "application/json");
								var postImpuesto = await _httpClient.PostAsync("/api/impuestos", impuestoContent);
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Impuesto auto-creado para producto {producto.articulo}: {producto.impuesto} - {postImpuesto.StatusCode}");
							}
						}
						impuestosVerificados.Add(producto.impuesto);
					}

					// 4. Subir producto usando ProductoUploadModel
					var upload = new ProductoUploadModel
					{
						Clave = producto.articulo,
						Descripcion = producto.descripcion,
						Precio = producto.precio1,
						Linea = producto.linea,
						Marca = producto.marca,
						Fabricante = producto.fabricante,
						Ubicacion = producto.ubicacion,
						Unidad = string.IsNullOrWhiteSpace(producto.unidad) ? "PZA" : producto.unidad,
						Bloqueado = producto.bloqueado,
						ParaVenta = producto.paraventa,
						Invent = producto.invent,
						Granel = producto.granel,
						Speso = producto.speso,
						BajoCosto = producto.bajocosto,
						Impuesto = producto.impuesto,
						Existencia = producto.existencia,
						Precio2 = producto.precio2,
						Precio3 = producto.precio3,
						Precio4 = producto.precio4,
						Precio5 = producto.precio5,
						Precio6 = producto.precio6,
						Precio7 = producto.precio7,
						Precio8 = producto.precio8,
						Precio9 = producto.precio9,
						Precio10 = producto.precio10,
						U1 = producto.u1.ToString(),
						U2 = producto.u2.ToString(),
						U3 = producto.u3.ToString(),
						U4 = producto.u4.ToString(),
						U5 = producto.u5.ToString(),
						U6 = producto.u6.ToString(),
						U7 = producto.u7.ToString(),
						U8 = producto.u8.ToString(),
						U9 = producto.u9.ToString(),
						U10 = producto.u10.ToString(),
						C2 = producto.c2.ToString(),
						C3 = producto.c3.ToString(),
						C4 = producto.c4.ToString(),
						C5 = producto.c5.ToString(),
						C6 = producto.c6.ToString(),
						C7 = producto.c7.ToString(),
						C8 = producto.c8.ToString(),
						C9 = producto.c9.ToString(),
						C10 = producto.c10.ToString(),
						CostoUltimo = producto.costoultimo,
						ClaveProdServ = string.IsNullOrWhiteSpace(producto.claveprodserv) ? "01010101" : producto.claveprodserv,
						ClaveUnidad = string.IsNullOrWhiteSpace(producto.claveunidad) ? "H87" : producto.claveunidad
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
							// Si el error es 422, no reintentar ni marcar como exportado
							if ((int)postResp.StatusCode == 422)
							{
								LogService.WriteLog(_settings.LogDirectory, $"[UPLOAD] Producto {producto.articulo} (POST): 422 - No se puede procesar, no se marcará como exportado ni se reintentará en este ciclo.");
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
