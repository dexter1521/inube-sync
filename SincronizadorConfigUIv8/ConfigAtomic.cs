using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SincronizadorConfigUIv8
{
	public class RetrySettings
	{
		public int MaxRetries { get; set; } = 3;
		public int BaseDelaySeconds { get; set; } = 2;
	}

	public class AppSettings
	{
		public int IntervaloMinutos { get; set; } = 15;
		public string ApiUrl { get; set; } = "";
		public string ApiUser { get; set; } = "";
		public string DeviceToken { get; set; } = "";
		public string SqlServer { get; set; } = "";
		public int TimeoutSeconds { get; set; } = 30;
		public string LogsPath { get; set; } = "C:\\Inube\\Logs";
		public string ApiToken { get; set; } = "";
		public RetrySettings Retry { get; set; } = new();
		public bool SubirDatosANube { get; set; }
	}

	public class RootConfig
	{
		public Dictionary<string, object>? Logging { get; set; }
		public AppSettings AppSettings { get; set; } = new();
	}

	public static class ConfigAtomic
	{
		public static RootConfig LoadConfig(string path)
		{
			var json = File.ReadAllText(path);
			var cfg = JsonSerializer.Deserialize<RootConfig>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			return cfg ?? new RootConfig();
		}

		public static void SaveConfigAtomic(string path, RootConfig cfg)
		{
			var tmp = path + ".tmp";
			var bak = path + ".bak";

			var json = JsonSerializer.Serialize(cfg, new JsonSerializerOptions
			{
				WriteIndented = true
			});

			File.WriteAllText(tmp, json);
			if (!File.Exists(path))
			{
				File.Move(tmp, path);
			}
			else
			{
				File.Replace(tmp, path, bak);
				try { File.Delete(bak); } catch { }
			}
		}
	}
}
