using SincronizadorCore.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace SincronizadorConfigUIv8
{
	public static class ConfigHelper
	{
		private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "appsettings.json");

		public static AppSettings LoadConfig()
		{
			if (!File.Exists(ConfigPath))
			{
				// Crear archivo con estructura por defecto
				var defaultConfig = new
				{
					Logging = new
					{
						LogLevel = new
						{
							Default = "Information",
							Microsoft = "Warning",
							MicrosoftHostingLifetime = "Information"
						}
					},
					AppSettings = new AppSettings
					{
						IntervaloMinutos = 15,
						ApiUrl = "https://miapi.com/api",
						ApiUser = "usuario",
						DeviceToken = "password",
						LogsPath = "Logs",
						SqlServer = "Server=.;Database=MiDB;Trusted_Connection=True;",
						ApiToken = "123456"
					}
				};

				var options = new JsonSerializerOptions { WriteIndented = true };
				string json = JsonSerializer.Serialize(defaultConfig, options);
				File.WriteAllText(ConfigPath, json);
			}

			var rawJson = File.ReadAllText(ConfigPath);
			var root = JsonDocument.Parse(rawJson).RootElement;
			var appSettings = root.GetProperty("AppSettings").GetRawText();
			return JsonSerializer.Deserialize<AppSettings>(appSettings)!;
		}

		public static void SaveConfig(AppSettings settings)
		{
			// Si el archivo no existe, crear con valores por defecto
			if (!File.Exists(ConfigPath))
			{
				LoadConfig(); // crea si no existe
			}

			var json = File.ReadAllText(ConfigPath);
			using JsonDocument doc = JsonDocument.Parse(json);
			var root = doc.RootElement;

			var options = new JsonSerializerOptions { WriteIndented = true };
			string newAppSettingsJson = JsonSerializer.Serialize(settings, options);

			string newJson = $"{{\"Logging\": {root.GetProperty("Logging").GetRawText()}, \"AppSettings\": {newAppSettingsJson}}}";
			File.WriteAllText(ConfigPath, newJson);
		}
	}
}
