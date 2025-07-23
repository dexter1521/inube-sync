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
			var productosApi = await ObtenerProductosDesdeApiAsync();

			foreach (var producto in productosApi)
			{
				SqlHelper.InsertarOActualizarProducto(producto, _settings.ConnectionStrings);
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
