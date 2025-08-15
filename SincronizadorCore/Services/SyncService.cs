using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SincronizadorCore.Models;
using SincronizadorCore.Utils;
using Polly;
using Polly.Retry;


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
				BaseAddress = new Uri(apiUrl ?? string.Empty),
				Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds > 0 ? _settings.TimeoutSeconds : 30)
			};
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _settings.DeviceToken);
			// Aquí puedes agregar integración con Polly para reintentos usando _settings.Retry
		}

		private AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			var maxRetries = _settings.Retry?.MaxRetries ?? 3;
			var baseDelay = _settings.Retry?.BaseDelaySeconds ?? 2;
			return Policy<HttpResponseMessage>
				.Handle<HttpRequestException>()
				.OrResult(r =>
					!r.IsSuccessStatusCode &&
					(r.StatusCode != System.Net.HttpStatusCode.Unauthorized && // 401
					 r.StatusCode != System.Net.HttpStatusCode.Forbidden &&    // 403
					 (int)r.StatusCode != 422))
				.WaitAndRetryAsync(maxRetries,
					retryAttempt => TimeSpan.FromSeconds(baseDelay * retryAttempt));
		}

		// Método auxiliar para GET con reintentos
		private async Task<HttpResponseMessage> GetWithRetryAsync(string requestUri)
		{
			var policy = GetRetryPolicy();
			var response = await policy.ExecuteAsync(() => _httpClient.GetAsync(requestUri));
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
				response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
				(int)response.StatusCode == 422)
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Error de credenciales o validación: {response.StatusCode}");
				}
				throw new InvalidOperationException($"Error crítico de autenticación o validación ({response.StatusCode})");
			}
			return response;
		}

		// Método auxiliar para POST con reintentos
		private async Task<HttpResponseMessage> PostWithRetryAsync(string requestUri, HttpContent content)
		{
			var policy = GetRetryPolicy();
			var response = await policy.ExecuteAsync(() => _httpClient.PostAsync(requestUri, content));
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
				response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
				(int)response.StatusCode == 422)
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Error de credenciales o validación: {response.StatusCode}");
				}
				throw new InvalidOperationException($"Error crítico de autenticación o validación ({response.StatusCode})");
			}
			return response;
		}

		// Sincronizar datos locales hacia la nube
		public async Task SincronizarHaciaNubeAsync()
		{
			string requestId = Guid.NewGuid().ToString();
			try
			{

				// Subir productos

				// Obtener catálogos locales no exportados para referencia rápida
				if (string.IsNullOrWhiteSpace(_settings.SqlServer))
				{
					if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
					{
						if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
						{
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								LogService.WriteLog(_settings.LogsPath, "[ERROR] La cadena de conexión SqlServer no está configurada.");
							}
						}
					}
					return;
				}
				var lineasNoExportadas = await SqlHelper.ObtenerLineasNoExportadasAsync(_settings.SqlServer);
				var marcasNoExportadas = await SqlHelper.ObtenerMarcasNoExportadasAsync(_settings.SqlServer);
				var impuestosLocales = await SqlHelper.ObtenerTodosImpuestosAsync(_settings.SqlServer);

				// HashSets para evitar crear repetidamente
				var lineasVerificadas = new HashSet<string>();
				var marcasVerificadas = new HashSet<string>();
				var impuestosVerificados = new HashSet<string>();

				bool primeraVuelta = true;
				List<ProductoModel> productosOrdenados = new List<ProductoModel>();
				while (true)
				{
					if (string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
						{
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, "[ERROR] La cadena de conexión SqlServer no está configurada.");
								}
							}
						}
						break;
					}
					var productos = await SqlHelper.ObtenerTodosProductosAsync(_settings.SqlServer);
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
						var getLinea = await GetWithRetryAsync($"/api/lineas/{Uri.EscapeDataString(productoNoNull.linea)}");
						if ((int)getLinea.StatusCode == 404)
						{
							if (string.IsNullOrWhiteSpace(_settings.SqlServer))
							{
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, "[ERROR] La cadena de conexión SqlServer no está configurada.");
								}
								break;
							}
							lineasNoExportadas = await SqlHelper.ObtenerLineasNoExportadasAsync(_settings.SqlServer);
							var lineaLocal = lineasNoExportadas.Find(l => l.Linea == productoNoNull.linea);
							if (lineaLocal != null)
							{
								var lineaContent = new StringContent(JsonSerializer.Serialize(lineaLocal), Encoding.UTF8, "application/json");
								var postLinea = await PostWithRetryAsync("/api/lineas", lineaContent);
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Línea auto-creada para producto {productoNoNull.articulo}: {productoNoNull.linea} - {postLinea.StatusCode}", requestId);
								}
								if (postLinea.IsSuccessStatusCode)
								{
									if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
									{
										await SqlHelper.MarcarLineasComoExportadasAsync(new List<string> { productoNoNull.linea }, _settings.SqlServer);
									}
									if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
									{
										LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Línea marcada como exportada: {productoNoNull.linea}", requestId);
									}
									// Refrescar la lista después de marcar como exportada
									if (string.IsNullOrWhiteSpace(_settings.SqlServer))
									{
										LogService.WriteLog(_settings.LogsPath, "[ERROR] La cadena de conexión SqlServer no está configurada.");
										break;
									}
									lineasNoExportadas = await SqlHelper.ObtenerLineasNoExportadasAsync(_settings.SqlServer);
								}
							}
						}
						lineasVerificadas.Add(productoNoNull.linea);
					}
					// (Eliminado control de productosProcesados, ya no es necesario)
					{
						var getMarca = await GetWithRetryAsync($"/api/marcas/{Uri.EscapeDataString(productoNoNull.marca)}");
						if ((int)getMarca.StatusCode == 404)
						{
							if (string.IsNullOrWhiteSpace(_settings.SqlServer))
							{
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, "[ERROR] La cadena de conexión SqlServer no está configurada.");
								}
								break;
							}
							marcasNoExportadas = await SqlHelper.ObtenerMarcasNoExportadasAsync(_settings.SqlServer);
							var marcaLocal = marcasNoExportadas.Find(m => m.Marca == productoNoNull.marca);
							if (marcaLocal != null)
							{
								var marcaContent = new StringContent(JsonSerializer.Serialize(marcaLocal), Encoding.UTF8, "application/json");
								var postMarca = await PostWithRetryAsync("/api/marcas", marcaContent);
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Marca auto-creada para producto {productoNoNull.articulo}: {productoNoNull.marca} - {postMarca.StatusCode}", requestId);
								}
								if (postMarca.IsSuccessStatusCode)
								{
									if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
									{
										await SqlHelper.MarcarMarcasComoExportadasAsync(new List<string> { productoNoNull.marca }, _settings.SqlServer);
									}
									if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
									{
										LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Marca marcada como exportada: {productoNoNull.marca}", requestId);
									}
									// Refrescar la lista después de marcar como exportada
									if (string.IsNullOrWhiteSpace(_settings.SqlServer))
									{
										LogService.WriteLog(_settings.LogsPath, "[ERROR] La cadena de conexión SqlServer no está configurada.");
										break;
									}
									marcasNoExportadas = await SqlHelper.ObtenerMarcasNoExportadasAsync(_settings.SqlServer);
								}
							}
						}
						marcasVerificadas.Add(productoNoNull.marca);
					}

					// 3. Validar/crear impuesto solo si no se ha verificado
					if (!string.IsNullOrWhiteSpace(productoNoNull.impuesto) && !impuestosVerificados.Contains(productoNoNull.impuesto))
					{
						var getImpuesto = await GetWithRetryAsync($"/api/impuestos/{Uri.EscapeDataString(productoNoNull.impuesto)}");
						if ((int)getImpuesto.StatusCode == 404)
						{
							var impuestoLocal = impuestosLocales.Find(i => i.Impuesto == productoNoNull.impuesto);
							if (impuestoLocal != null)
							{
								var impuestoContent = new StringContent(JsonSerializer.Serialize(impuestoLocal), Encoding.UTF8, "application/json");
								var postImpuesto = await PostWithRetryAsync("/api/impuestos", impuestoContent);
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Impuesto auto-creado para producto {productoNoNull.articulo}: {productoNoNull.impuesto} - {postImpuesto.StatusCode}", requestId);
								}
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
					var getResp = await GetWithRetryAsync($"/api/productos/{Uri.EscapeDataString(producto.articulo)}");
					if (getResp.IsSuccessStatusCode)
					{
						// Solo PUT si el producto ya existe en la nube
						var putResp = await _httpClient.PutAsync($"/api/productos/{Uri.EscapeDataString(producto.articulo)}", content);
						var putMsg = putResp.IsSuccessStatusCode ? "Actualizado" : "Error al actualizar";
						var putError = putResp.IsSuccessStatusCode ? "" : $" | Error: {await putResp.Content.ReadAsStringAsync()}";
						if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
						{
							LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Producto {producto.articulo} (PUT): {putResp.StatusCode} - {putMsg}{putError}", requestId);
						}
						if (putResp.IsSuccessStatusCode)
						{
							if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
							{
								await SqlHelper.MarcarProductosComoExportadosAsync(new List<string> { producto.articulo }, _settings.SqlServer);
							}
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Producto marcado como exportado: {producto.articulo}", requestId);
							}
						}
					}
					else
					{
						// Solo POST si el producto no existe en la nube
						var postResp = await PostWithRetryAsync("/api/productos", content);
						var postMsg = postResp.IsSuccessStatusCode ? "Registrado" : "Error al registrar";
						var postError = postResp.IsSuccessStatusCode ? "" : $" | Error: {await postResp.Content.ReadAsStringAsync()}";
						if (!postResp.IsSuccessStatusCode)
						{
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								LogService.WriteLog(_settings.LogsPath, $"[DEBUG] JSON enviado para {producto.articulo}: {JsonSerializer.Serialize(upload)}", requestId);
							}
							// Si el error es 422, marcar como exportado y continuar
							if ((int)postResp.StatusCode == 422)
							{
								if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
								{
									await SqlHelper.MarcarProductosComoExportadosAsync(new List<string> { producto.articulo }, _settings.SqlServer);
								}
								if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
								{
									LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Producto {producto.articulo} (POST): 422 - Ya existe, marcado como exportado localmente.", requestId);
								}
								continue;
							}
						}
						if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
						{
							LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Producto {producto.articulo} (POST): {postResp.StatusCode} - {postMsg}{postError}", requestId);
						}
						if (postResp.IsSuccessStatusCode)
						{
							if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
							{
								await SqlHelper.MarcarProductosComoExportadosAsync(new List<string> { producto.articulo }, _settings.SqlServer);
							}
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Producto marcado como exportado: {producto.articulo}", requestId);
							}
						}
					}
				}


				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, "[UPLOAD] Sincronización hacia la nube finalizada.", requestId);
				}
			}
			catch (Exception ex)
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[UPLOAD] Error al sincronizar hacia la nube: {ex.Message}", requestId);
				}
			}
		}

		public async Task<List<ProductoModel>> ObtenerProductosDesdeApiAsync()
		{
			var productos = new List<ProductoModel>();

			try
			{
				_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", _settings.DeviceToken);

				var response = await _httpClient.GetAsync("/api/productos");

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
					productos = JsonSerializer.Deserialize<List<ProductoModel>>(json,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ProductoModel>();

					if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
					{
						LogService.WriteLog(_settings.LogsPath, $"[API] Se obtuvieron {productos.Count} productos.");
					}
				}
				else
				{
					if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
					{
						LogService.WriteLog(_settings.LogsPath, $"[API] Error al obtener productos: {response.StatusCode}");
					}
				}
			}
			catch (Exception ex)
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Excepción: {ex.Message}");
				}
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
					if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
					{
						LogService.WriteLog(_settings.LogsPath, $"[API] Error al obtener pendientes: {pendientesResponse.StatusCode}");
					}
					return;
				}

				var pendientesJson = await pendientesResponse.Content.ReadAsStringAsync();
				var pendientes = JsonSerializer.Deserialize<List<SincronizadorCore.Models.ProdPendienteModel>>(pendientesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<SincronizadorCore.Models.ProdPendienteModel>();

				if (pendientes.Count == 0)
				{
					if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
					{
						LogService.WriteLog(_settings.LogsPath, "[SYNC] No hay productos pendientes para actualizar precios.");
					}
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
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarLineaAsync(linea, _settings.SqlServer);
					}

					// Validar y registrar/actualizar marca
					var marca = new MarcaModel { Marca = producto.marca ?? "SYS", Descrip = producto.marca ?? "" };
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarMarcaAsync(marca, _settings.SqlServer);
					}

					// Validar y registrar/actualizar impuesto
					var impuesto = new ImpuestoModel { Impuesto = producto.impuesto ?? "SYS", Valor = 0 };
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarImpuestoAsync(impuesto, _settings.SqlServer);
					}

					// Actualizar producto
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarProductoAsync(producto, _settings.SqlServer);
					}

					// Confirmar la actualización con el endpoint POST
					var pendiente = pendientes.FirstOrDefault(p => p.Clave == producto.articulo);
					if (pendiente != null)
					{
						var confirmContent = new StringContent($"{{\"id\": {pendiente.Id}}}", Encoding.UTF8, "application/json");
						var confirmResponse = await _httpClient.PostAsync("/api/prods_download/aplicar", confirmContent);
						if (confirmResponse.IsSuccessStatusCode)
						{
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								LogService.WriteLog(_settings.LogsPath, $"[SYNC] Confirmado producto pendiente id={pendiente.Id} clave={pendiente.Clave}");
							}
						}
						else
						{
							if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
							{
								LogService.WriteLog(_settings.LogsPath, $"[SYNC] Error al confirmar producto pendiente id={pendiente.Id} clave={pendiente.Clave}: {confirmResponse.StatusCode}");
							}
						}
					}

					if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
					{
						LogService.WriteLog(_settings.LogsPath, $"[SYNC] Se actualizaron precios de {productosActualizar.Count} productos pendientes y se confirmó cada uno.");
					}
				}
			}
			catch (Exception ex)
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[SYNC] Error en sincronización de precios pendientes: {ex.Message}");
				}
			}
		}
		public async Task ConsultarDispositivosAsync()
		{
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", _settings.DeviceToken);

			var response = await _httpClient.GetAsync("/api/dispositivos");
			var result = await response.Content.ReadAsStringAsync();

			if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
			{
				LogService.WriteLog(_settings.LogsPath,
					$"GET dispositivos: {(int)response.StatusCode} - {result}");
			}
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
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarLineaAsync(lineaLocal, _settings.SqlServer);
					}
					count++;
				}
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Se sincronizaron {count} líneas.");
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Error al obtener líneas: {response.StatusCode}");
				}
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
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarMarcaAsync(marcaLocal, _settings.SqlServer);
					}
					count++;
				}
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Se sincronizaron {count} marcas.");
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Error al obtener marcas: {response.StatusCode}");
				}
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
					if (!string.IsNullOrWhiteSpace(_settings.SqlServer))
					{
						await SqlHelper.InsertarOActualizarImpuestoAsync(impuestoLocal, _settings.SqlServer);
					}
					count++;
				}
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Se sincronizaron {count} impuestos.");
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(_settings.LogsPath))
				{
					LogService.WriteLog(_settings.LogsPath, $"[API] Error al obtener impuestos: {response.StatusCode}");
				}
			}
		}
	}
}
