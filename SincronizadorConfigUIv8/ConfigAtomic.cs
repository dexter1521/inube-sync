using System;
using System.Collections.Generic;
using SincronizadorCore.Models;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SincronizadorConfigUIv8
{

	public class RootConfig
	{
		public LoggingSettings Logging { get; set; } = new();
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
