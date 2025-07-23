using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SincronizadorCore.Models
{
	public class ProductoLocal
	{
		public string articulo { get; set; } = string.Empty;
		public string descrip { get; set; } = string.Empty;
		public string linea { get; set; } = string.Empty;
		public string marca { get; set; } = string.Empty;
		public string impuesto { get; set; } = string.Empty;
		public string ubicacion { get; set; } = string.Empty;
		public decimal precio1 { get; set; }
		public decimal precio2 { get; set; }
		public decimal precio3 { get; set; }
		public decimal precio4 { get; set; }
		public decimal precio5 { get; set; }
		public string c2 { get; set; } = string.Empty;
		public string c3 { get; set; } = string.Empty;
		public string c4 { get; set; } = string.Empty;
		public string c5 { get; set; } = string.Empty;
		public string u1 { get; set; } = string.Empty;
		public string u2 { get; set; } = string.Empty;
		public string u3 { get; set; } = string.Empty;
		public string u4 { get; set; } = string.Empty;
		public string u5 { get; set; } = string.Empty;
		public string unidad { get; set; } = string.Empty;
		public string bloqueado { get; set; } = "0";
		public string paraventa { get; set; } = "1";
		public string invent { get; set; } = "1";
		public string granel { get; set; } = "0";
		public string speso { get; set; } = "0";
		public string precio { get; set; } = "0";
	}
}


