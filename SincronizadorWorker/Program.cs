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

		// Cargar configuración principal
		config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

		// Si existe, también carga la configuración por entorno
		config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

		// Variables de entorno (por si deseas agregar alguna en el futuro)
		config.AddEnvironmentVariables();
	})
	.ConfigureServices((hostContext, services) =>
	{
		// Registrar los valores de AppSettings para inyección por IOptions<AppSettings>
		services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));

		// Registrar el servicio del worker
		services.AddHostedService<Worker>();
	})
	.ConfigureLogging(logging =>
	{
		logging.ClearProviders(); // Limpia loggers por defecto
		logging.AddConsole();     // Habilita logs en consola (útil para depuración)
	})
	.UseWindowsService() // Permite que funcione como servicio de Windows
	.Build();

await host.RunAsync();
