using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SincronizadorCore.Utils;

namespace SincronizadorWorker
{
	public class Worker : BackgroundService
	{

		private readonly ILogger<Worker> _logger;
		private readonly AppSettings _settings;
		private readonly SyncService _syncService;
		private Timer? _timer;

		public Worker(ILogger<Worker> logger, IOptions<AppSettings> options)
		{
			_logger = logger;
			_settings = options.Value;
			_syncService = new SyncService(_settings);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Servicio iniciado.");

			_timer = new Timer(async state =>
			{
				_logger.LogInformation("Ejecutando sincronización...");
				await _syncService.SincronizarProductosAsync();
			},
			null,
			TimeSpan.Zero,
			TimeSpan.FromMinutes(_settings.IntervaloMinutos));

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
