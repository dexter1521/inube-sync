using SincronizadorWorker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.WindowsServices;
using SincronizadorCore;
using SincronizadorCore.Utils;

if (!System.IO.File.Exists("appsettings.json"))
{
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.WriteLine("ADVERTENCIA: El archivo 'appsettings.json' no se encuentra en el directorio de ejecución: " + Environment.CurrentDirectory);
	Console.ResetColor();
	// Crear archivo con estructura completa proporcionada
	var contenido =
		"{\n" +
		"  \"Logging\": {\n" +
		"    \"LogLevel\": {\n" +
		"      \"Default\": \"Information\",\n" +
		"      \"Microsoft\": \"Warning\",\n" +
		"      \"MicrosoftHostingLifetime\": \"Information\"\n" +
		"    }\n" +
		"  },\n" +
		"  \"AppSettings\": {\n" +
		"    \"IntervaloMinutos\": 1,\n" +
		"    \"ApiUrl\": \"https://minube.dritec.com.mx/api/\",\n" +
		"    \"ApiUser\": \"\",\n" +
		"    \"ApiPassword\": \"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE3NTI3MjQ1MzksImV4cCI6MTc1MjcyODEzOSwidWlkIjoxLCJub21icmUiOiJFRFVBUkRPIE1BUlZJTCIsImVtYWlsIjoidW5AY29ycmVvLmNvbSIsInBlcmZpbCI6Mn0.EQv5lfUyjezdcAguBNA4hoKY4PZsYwD1LJwPGu5GF20\",\n" +
		"    \"ConnectionStrings\": \"Provider=SQLNCLI;Data Source=TCP:DESKTOP-OOGIDH8\\\\SQLEXPRESS,1433;\",\n" +
		"    \"LogDirectory\": \"Logs\",\n" +
		"    \"ApiToken\": \"123456\"\n" +
		"  }\n" +
		"}";
	System.IO.File.WriteAllText("appsettings.json", contenido);
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine("Se creó automáticamente un archivo 'appsettings.json' con la estructura completa.");
	Console.ResetColor();
}

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
