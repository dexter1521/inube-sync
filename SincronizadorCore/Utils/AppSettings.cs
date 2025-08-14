using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorCore.Utils
{
	public class AppSettings
	{
		public int IntervaloMinutos { get; set; }
		public string ApiUrl { get; set; } = string.Empty;
		public string ApiUser { get; set; } = string.Empty;
		public string DeviceToken { get; set; } = string.Empty;
		public string SqlServer { get; set; } = string.Empty;
		public string LogsPath { get; set; } = "C:\\Inube\\Logs";
		public string ApiToken { get; set; } = string.Empty;
		// Bandera para subir datos locales a la nube
		public bool SubirDatosANube { get; set; } = false;
		public int TimeoutSeconds { get; set; } = 30;
		public RetrySettings? Retry { get; set; }
	}

	public sealed class RetrySettings
	{
		public int MaxRetries { get; set; } = 3;
		public int BaseDelaySeconds { get; set; } = 2;
	}

	public sealed class LoggingSettings
	{
		public string LogsPath { get; set; } = "C:\\Inube\\Logs";
	}
}
