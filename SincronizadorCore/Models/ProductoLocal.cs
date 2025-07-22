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
			public int linea { get; set; }
			public int marca { get; set; }
			public int impuesto { get; set; }
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
		}
	}


