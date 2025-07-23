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
		public string ApiPassword { get; set; } = string.Empty;
		public string ConnectionStrings { get; set; } = string.Empty;
		public string LogDirectory { get; set; } = "Logs";
		public string ApiToken { get; set; } = string.Empty;
	}
}
