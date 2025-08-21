using SincronizadorCore.Models;
using Polly;
using Polly.Extensions.Http;
using Polly.Contrib.WaitAndRetry;
using SincronizadorWorker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.WindowsServices;
using SincronizadorCore;
using SincronizadorCore.Utils;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration((hostingContext, config) =>
	{
		var env = hostingContext.HostingEnvironment;

		// Cargar configuraci�n principal
		config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

		// Si existe, tambi�n carga la configuraci�n por entorno
		config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

		// Variables de entorno (por si deseas agregar alguna en el futuro)
		config.AddEnvironmentVariables();
	})
	.ConfigureServices((hostContext, services) =>
	{
		// Registrar los valores de AppSettings para inyecci�n por IOptions<AppSettings>
		services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
		services.Configure<LoggingSettings>(hostContext.Configuration.GetSection("Logging"));

		// Registrar el handler de autenticación
		services.AddTransient<SincronizadorCore.Handlers.AuthHeaderHandler>();

		// Registrar HttpClient con el handler y la BaseAddress para el cliente 'api'
		services.AddHttpClient("api", (sp, client) =>
		{
			var settings = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<AppSettings>>().Value;
			var apiUrl = settings.ApiUrl?.TrimEnd('/');
			if (!string.IsNullOrWhiteSpace(apiUrl))
				client.BaseAddress = new Uri(apiUrl);
			client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds > 0 ? settings.TimeoutSeconds : 30);
		})
		.AddHttpMessageHandler<SincronizadorCore.Handlers.AuthHeaderHandler>()
		.AddPolicyHandler((sp, request) =>
		{
			// Solo aplicar la política a métodos idempotentes
			if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Put || request.Method.Method == "PATCH")
			{
				var delay = TimeSpan.FromSeconds(2);
				var maxRetries = 5;
				var jitter = Polly.Contrib.WaitAndRetry.Backoff.DecorrelatedJitterBackoffV2(delay, maxRetries);
				// Solo reintentar en errores transitorios: 5xx, 408, 429
				return Policy<HttpResponseMessage>
					.Handle<HttpRequestException>()
					.OrResult(r =>
						((int)r.StatusCode >= 500 && (int)r.StatusCode < 600) || // 5xx
						r.StatusCode == System.Net.HttpStatusCode.RequestTimeout || // 408
						(int)r.StatusCode == 429)
					.WaitAndRetryAsync(jitter);
			}
			// Sin política para POST/DELETE
			return Polly.Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
		});

		// Registrar el servicio del worker
		services.AddHostedService<Worker>();
	})
	.ConfigureLogging(logging =>
	{
		logging.ClearProviders(); // Limpia loggers por defecto
		logging.AddConsole();     // Habilita logs en consola (�til para depuraci�n)
	})
	.UseWindowsService() // Permite que funcione como servicio de Windows
	.Build();

await host.RunAsync();
