using SincronizadorCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SincronizadorCore.Models
{
	public class ProductoModel
	{
		public string articulo { get; set; } = string.Empty;
		public string descripcion { get; set; } = string.Empty;
		public string unidad { get; set; } = string.Empty;
		public string marca { get; set; } = "SYS";
		public string linea { get; set; } = "SYS";
		public string? impuesto { get; set; } = "SYS";
		public decimal costoultimo { get; set; } = 0;
		public decimal precio1 { get; set; }
		public decimal precio2 { get; set; }
		public decimal precio3 { get; set; }
		public decimal precio4 { get; set; }
		public decimal precio5 { get; set; }
		public decimal c2 { get; set; }
		public decimal c3 { get; set; }
		public decimal c4 { get; set; }
		public decimal c5 { get; set; }
		public decimal u1 { get; set; } = 0;
		public decimal u2 { get; set; } = 0;
		public decimal u3 { get; set; } = 0;
		public decimal u4 { get; set; } = 0;
		public decimal u5 { get; set; } = 0;
		public int bloqueado { get; set; } = 0;
		public int paraventa { get; set; } = 1;
		public int invent { get; set; } = 1;
		public int granel { get; set; } = 0;
		public int speso { get; set; } = 0;
		public decimal precio { get; set; } = 0;
		
	}

	

}
