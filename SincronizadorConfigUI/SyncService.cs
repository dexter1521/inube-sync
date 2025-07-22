using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SincronizadorConfigUI
{
	internal class SyncService
	{
		private readonly HttpClient _httpClient;
		private readonly AppSettings _settings;

		public SyncService(AppSettings settings)
		{
			_settings = settings;
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri(_settings.ApiUrl);
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", _settings.ApiToken);
		}

		public async Task SincronizarProductosAsync()
		{
			try
			{
				// TODO: Obtener productos desde la BD local y recorrerlos
				var producto = new ProductoModel
				{
					nombre = "Prueba API",
					marca_id = 1,
					linea_id = 1,
					precio = 10.5m,
					stock = 15
				};

				var json = JsonSerializer.Serialize(producto);
				var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

				var response = await _httpClient.PostAsync("/api/productos", content);
				var result = await response.Content.ReadAsStringAsync();

				LogService.WriteLog(_settings.LogDirectory, $"POST productos: {(int)response.StatusCode} - {result}");
			}
			catch (Exception ex)
			{
				LogService.WriteLog(_settings.LogDirectory, $"Error al sincronizar productos: {ex.Message}");
			}
		}
	}
}
