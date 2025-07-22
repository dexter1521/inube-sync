using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;


namespace SincronizadorConfigUI
{
	public static class ConfigHelper
	{
		// Obtiene la ruta completa del archivo appsettings.json en el directorio raíz de la solución
		private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "appsettings.json");

		public static AppSettings LoadConfig()
		{
			if (!File.Exists(ConfigPath))
			{
				throw new FileNotFoundException($"No se encontró el archivo de configuración en la ruta: {ConfigPath}");
			}

			var json = File.ReadAllText(ConfigPath);
			var root = JsonDocument.Parse(json).RootElement;
			var appSettings = root.GetProperty("AppSettings").GetRawText();
			return JsonSerializer.Deserialize<AppSettings>(appSettings);
		}

		public static void SaveConfig(AppSettings settings)
		{
			if (!File.Exists(ConfigPath))
			{
				throw new FileNotFoundException($"No se encontró el archivo de configuración en la ruta: {ConfigPath}");
			}

			var json = File.ReadAllText(ConfigPath);
			JsonDocument doc = JsonDocument.Parse(json);
			try
			{
				var root = doc.RootElement.Clone();

				var options = new JsonSerializerOptions { WriteIndented = true };
				string newAppSettingsJson = JsonSerializer.Serialize(settings, options);

				var newJson = $"{{\"Logging\": {root.GetProperty("Logging").GetRawText()}, \"AppSettings\": {newAppSettingsJson}}}";
				File.WriteAllText(ConfigPath, newJson);
			}
			finally
			{
				doc.Dispose();
			}
		}
	}
}
