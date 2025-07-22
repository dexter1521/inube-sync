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
		public string nombre { get; set; }
		public int marca_id { get; set; }
		public int linea_id { get; set; }
		public decimal precio { get; set; }
		public int stock { get; set; }
	}

	

}
