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
				new AuthenticationHeaderValue("Bearer", _settings.ApiToken);
		}

		public async Task SincronizarProductosAsync()
		{
			var productosLocales = SqlHelper.ObtenerProductos(_settings.ConnectionStrings);

			foreach (var local in productosLocales)
			{
				var productoApi = new ProductoModel
				{
					nombre = local.descrip,
					linea_id = local.linea,
					marca_id = local.marca,
					precio = local.precio1,
					stock = 10 // puedes mapearlo desde tu campo real si lo tienes
				};

				var json = JsonSerializer.Serialize(productoApi);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await _httpClient.PostAsync("/api/productos", content);
				var result = await response.Content.ReadAsStringAsync();

				LogService.WriteLog(_settings.LogDirectory,
					$"POST producto {local.articulo}: {(int)response.StatusCode} - {result}");
			}
		}
	}
}
