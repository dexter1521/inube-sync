using Microsoft.Win32;
using System.Text.RegularExpressions;
using SincronizadorCore.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace SincronizadorConfigUIv8
{
	public static class ConfigHelper
	{
		// Permite obtener la ruta real del appsettings.json del Worker instalado como servicio
		public static string GetServiceExeFolder(string serviceName)
		{
			using var key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\\CurrentControlSet\\Services\\{serviceName}");
			if (key == null) throw new InvalidOperationException("Servicio no encontrado en el registro.");
			var imagePath = key.GetValue("ImagePath")?.ToString() ?? "";
			var match = Regex.Match(imagePath, "^\"(?<exe>[^\"]+)\"");
			var exe = match.Success ? match.Groups["exe"].Value : imagePath.Split(' ')[0];
			if (string.IsNullOrWhiteSpace(exe)) throw new InvalidOperationException("ImagePath vacío.");
			return Path.GetDirectoryName(exe)!;
		}

		public static string GetAppSettingsPath(string serviceName)
		{
			var folder = GetServiceExeFolder(serviceName);
			return Path.Combine(folder, "appsettings.json");
		}

		// Por defecto, usa el appsettings.json local, pero permite sobreescribir la ruta
		private static string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "appsettings.json");

		public static void SetConfigPath(string path)
		{
			ConfigPath = path;
		}

		public static AppSettings LoadConfig(string? customPath = null)
		{
			var path = customPath ?? ConfigPath;
			if (!File.Exists(path))
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
				File.WriteAllText(path, json);
			}

			var rawJson = File.ReadAllText(path);
			var root = JsonDocument.Parse(rawJson).RootElement;
			var appSettings = root.GetProperty("AppSettings").GetRawText();
			return JsonSerializer.Deserialize<AppSettings>(appSettings)!;
		}

		public static void SaveConfig(AppSettings settings, string? customPath = null)
		{
			// Si el archivo no existe, crear con valores por defecto
			var path = customPath ?? ConfigPath;
			if (!File.Exists(path))
			{
				LoadConfig(path); // crea si no existe
			}

			var json = File.ReadAllText(path);
			using JsonDocument doc = JsonDocument.Parse(json);
			var root = doc.RootElement;

			var options = new JsonSerializerOptions { WriteIndented = true };
			string newAppSettingsJson = JsonSerializer.Serialize(settings, options);

			string newJson = $"{{\"Logging\": {root.GetProperty("Logging").GetRawText()}, \"AppSettings\": {newAppSettingsJson}}}";
			File.WriteAllText(path, newJson);
		}
	}
}
