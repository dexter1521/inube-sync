using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SincronizadorCore.Utils
{
	public class LogService
	{
		// Palabras clave sensibles a filtrar
		private static readonly string[] SensitiveKeywords =
			["token", "password", "authorization", "bearer", "secret", "apikey", "api_key"];

		// Filtra palabras sensibles en el mensaje
		private static string FilterSensitive(string message)
		{
			if (string.IsNullOrEmpty(message)) return message;
			string filtered = message;
			foreach (var keyword in SensitiveKeywords)
			{
				// Regex: busca keyword=valor o keyword: valor (case-insensitive)
				filtered = Regex.Replace(filtered, $@"({keyword})\s*[:=]\s*[^\s,;]+", "$1=[REDACTED]", RegexOptions.IgnoreCase);
			}
			return filtered;
		}

		// Nuevo método con RequestId opcional
	public static void WriteLog(string LogsPath, string message, string? requestId = null, string level = "INFO", string category = "General")
		{
			try
			{
				if (string.IsNullOrWhiteSpace(LogsPath))
					LogsPath = "Logs";
				try
				{
					if (!Directory.Exists(LogsPath))
						Directory.CreateDirectory(LogsPath);
				}
				catch (Exception dirEx)
				{
					Console.WriteLine($"[LogService] Error creando el directorio de logs '{LogsPath}': {dirEx.Message}");
					return;
				}

				string fileName = $"log_{DateTime.Now:yyyy-MM-dd}.txt";
				string fullPath = Path.Combine(LogsPath, fileName);

				string safeMessage = FilterSensitive(message);
				string logLine = $"[{DateTime.Now:HH:mm:ss}] [{level}] [{category}]";
				if (!string.IsNullOrWhiteSpace(requestId))
					logLine += $" [RequestId: {requestId}]";
				logLine += $" {safeMessage}{Environment.NewLine}";

				try
				{
					File.AppendAllText(fullPath, logLine);
				}
				catch (Exception fileEx)
				{
					Console.WriteLine($"[LogService] Error escribiendo el archivo de log '{fullPath}': {fileEx.Message}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("[LogService] Error inesperado: " + ex.Message);
			}
		}
	}
}
