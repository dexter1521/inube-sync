namespace SincronizadorCore.Models
{
	public class AppSettings
	{
		public int IntervaloMinutos { get; set; }
		public string? ApiUrl { get; set; }
		public string? ApiUser { get; set; }
		public string? DeviceToken { get; set; }
		public string? SqlServer { get; set; }
		public int TimeoutSeconds { get; set; }
		public int BatchSize { get; set; }
		public RetrySettings? Retry { get; set; }
		public bool SubirDatosANube { get; set; }
		public string? LogsPath { get; set; } = "C:\\Inube\\Logs";
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
