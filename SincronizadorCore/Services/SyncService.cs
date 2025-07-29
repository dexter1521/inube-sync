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
			_httpClient = new HttpClient
			{
				BaseAddress = new Uri(_settings.ApiUrl)
			};
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", _settings.ApiPassword);
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
				}

				LogService.WriteLog(_settings.LogDirectory, $"[SYNC] Se actualizaron precios de {productosActualizar.Count} productos pendientes y se confirmó cada uno.");
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
