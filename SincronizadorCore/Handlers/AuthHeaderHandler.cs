using SincronizadorCore.Models;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
// ...existing code...

namespace SincronizadorCore.Handlers
{
	public class AuthHeaderHandler : DelegatingHandler
	{
		private readonly IOptions<AppSettings> _settings;

		public AuthHeaderHandler(IOptions<AppSettings> settings)
		{
			_settings = settings;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// Agrega el header Authorization si no existe
			if (!request.Headers.Contains("Authorization") && !string.IsNullOrWhiteSpace(_settings.Value.DeviceToken))
			{
				request.Headers.Add("Authorization", $"Bearer {_settings.Value.DeviceToken}");
			}
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
