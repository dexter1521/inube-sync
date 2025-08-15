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
			_logger.LogInformation("Servicio iniciado.");


			_timer = new Timer(async state =>
			{
				_logger.LogInformation("Ejecutando consulta de worker ...");
				if (_settings.Value.SubirDatosANube)
				{
					_logger.LogInformation("Subiendo datos locales a la nube...");
					await _syncService.SincronizarHaciaNubeAsync();
				}
				await _syncService.SincronizarDesdeApiAsync();
				_logger.LogInformation("Ejecutando sincronización desde la API...");
			},
			null,
			TimeSpan.Zero,
			TimeSpan.FromMinutes(_settings.Value.IntervaloMinutos));

			return Task.CompletedTask;
		}


		public override Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Servicio detenido.");
			_timer?.Change(Timeout.Infinite, 0);
			return base.StopAsync(cancellationToken);
		}



	}



}
