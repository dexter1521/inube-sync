using System;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using SincronizadorCore.Models;

namespace SincronizadorConfigUIv8
{
	public static class ConfigHelper
	{
		// Permite obtener la ruta real del appsettings.json del Worker instalado como servicio
		public static string GetServiceExeFolder(string serviceName)
		{
			// Intenta primero en 64 bits, luego en 32 bits si no se encuentra
			string? imagePath = null;
			foreach (var view in new[] { RegistryView.Registry64, RegistryView.Registry32 })
			{
				try
				{
					using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view);
					using var key = baseKey.OpenSubKey($@"SYSTEM\\CurrentControlSet\\Services\\{serviceName}");
					if (key != null)
					{
						imagePath = key.GetValue("ImagePath")?.ToString() ?? "";
						break;
					}
				}
				catch (System.Security.SecurityException)
				{
					System.Windows.Forms.MessageBox.Show("Acceso denegado al registro. Ejecuta la aplicación como Administrador.", "Permisos requeridos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					throw;
				}
				catch (UnauthorizedAccessException)
				{
					System.Windows.Forms.MessageBox.Show("Acceso denegado al registro. Ejecuta la aplicación como Administrador.", "Permisos requeridos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					throw;
				}
				catch { }
			}
			if (string.IsNullOrWhiteSpace(imagePath)) throw new InvalidOperationException("Servicio no encontrado en el registro.");
			// Caso 1: entrecomillado
			var match = Regex.Match(imagePath, "^\"(?<exe>[^\"]+)\"");
			if (match.Success)
			{
				var exe = match.Groups["exe"].Value;
				if (string.IsNullOrWhiteSpace(exe)) throw new InvalidOperationException("ImagePath vacío.");
				return Path.GetDirectoryName(exe)!;
			}
			// Caso 2: sin comillas, con espacios y argumentos
			// Busca el primer token que termine en .exe
			var tokens = imagePath.Split(' ');
			foreach (var token in tokens)
			{
				if (token.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
				{
					return Path.GetDirectoryName(token)!;
				}
			}
			// Fallback: primer token
			var fallbackExe = tokens[0];
			if (string.IsNullOrWhiteSpace(fallbackExe)) throw new InvalidOperationException("ImagePath vacío.");
			return Path.GetDirectoryName(fallbackExe)!;
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

		public static RootConfig LoadConfig(string? customPath = null)
		{
			var path = customPath ?? ConfigPath;
			if (!File.Exists(path))
			{
				var defaultConfig = new RootConfig
				{
					Logging = new LoggingSettings { LogsPath = "C:\\Inube\\Logs" },
					AppSettings = new AppSettings
					{
						IntervaloMinutos = 15,
						ApiUrl = "",
						ApiUser = "",
						DeviceToken = "",
						SqlServer = "Server=.;Database=MiDB;Trusted_Connection=True;",
						TimeoutSeconds = 30,
						BatchSize = 100,
						SubirDatosANube = false,
						Retry = new RetrySettings()
					}
				};
				var options = new JsonSerializerOptions { WriteIndented = true };
				string json = JsonSerializer.Serialize(defaultConfig, options);
				File.WriteAllText(path, json);
			}
			var rawJson = File.ReadAllText(path);
			return JsonSerializer.Deserialize<RootConfig>(rawJson)!;
		}

		public static void SaveConfig(RootConfig config, string? customPath = null)
		{
			var path = customPath ?? ConfigPath;
			var options = new JsonSerializerOptions { WriteIndented = true };
			string json = JsonSerializer.Serialize(config, options);
			File.WriteAllText(path, json);
		}
	}
}
