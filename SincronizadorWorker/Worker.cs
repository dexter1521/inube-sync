using SincronizadorCore.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using SincronizadorCore.Services;
using SincronizadorCore.Utils;


namespace SincronizadorWorker
{
	public class Worker : BackgroundService
	{

		private readonly ILogger<Worker> _logger;
		private readonly IOptions<AppSettings> _settings;
		private readonly IOptions<LoggingSettings> _logging;
		private SyncService _syncService;
		private Timer? _timer;

		public Worker(ILogger<Worker> logger, IOptions<AppSettings> options, IOptions<LoggingSettings> logging, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_settings = options;
			_logging = logging;
			_syncService = new SyncService(_settings.Value, _logging.Value.LogsPath);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// Validación de configuración crítica
			if (string.IsNullOrWhiteSpace(_settings.Value.ApiUrl) || string.IsNullOrWhiteSpace(_settings.Value.DeviceToken))
			{
				_logger.LogCritical("No se puede iniciar el servicio: falta ApiUrl o DeviceToken en la configuración.");
				throw new InvalidOperationException("Configuración incompleta: ApiUrl y DeviceToken son obligatorios.");
			}

			_logger.LogInformation("Servicio iniciado.");

			var intervalo = TimeSpan.FromMinutes(_settings.Value.IntervaloMinutos);
			var timer = new PeriodicTimer(intervalo);

			// Ejecuta la primera iteración inmediatamente
			async Task EjecutarCiclo()
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					try
					{
						_logger.LogInformation("Ejecutando consulta de worker ...");
						if (_settings.Value.SubirDatosANube)
						{
							_logger.LogInformation("Subiendo datos locales a la nube...");
							await _syncService.SincronizarHaciaNubeAsync();
						}
						await _syncService.SincronizarDesdeApiAsync();
						_logger.LogInformation("Ejecutando sincronización desde la API...");
					}
					catch (Exception ex)
					{
						_logger.LogError(ex, "Error en ciclo de sincronización");
					}

					// Espera el siguiente ciclo o termina si se cancela
					if (!await timer.WaitForNextTickAsync(stoppingToken))
						break;
				}
			}

			// Ejecuta la primera iteración antes del ciclo periódico
			_ = EjecutarCiclo();
			return Task.CompletedTask;
		}


		public override Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Servicio detenido.");
			return base.StopAsync(cancellationToken);
		}



	}



}
