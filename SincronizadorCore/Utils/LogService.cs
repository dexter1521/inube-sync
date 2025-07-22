using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorCore.Utils
{
	public class LogService
	{
		public static void WriteLog(string logDirectory, string message)
		{
			try
			{
				// Validar si viene vacío, nulo o con espacios, y usar "Logs" por defecto
				if (string.IsNullOrWhiteSpace(logDirectory))
					logDirectory = "Logs";

				// Crear directorio si no existe
				if (!Directory.Exists(logDirectory))
					Directory.CreateDirectory(logDirectory);

				string fileName = $"log_{DateTime.Now:yyyy-MM-dd}.txt";
				string fullPath = Path.Combine(logDirectory, fileName);

				File.AppendAllText(fullPath, $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error escribiendo log: " + ex.Message);
			}
		}
	}
}
