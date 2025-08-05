using System.Text.Json.Serialization;

namespace SincronizadorCore.Models
{
	public class ProductoUploadModel
	{
		[JsonPropertyName("clave")]
		public string Clave { get; set; } = string.Empty;
		[JsonPropertyName("descripcion")]
		public string Descripcion { get; set; } = string.Empty;
		[JsonPropertyName("precio")]
		public decimal Precio { get; set; }
		[JsonPropertyName("linea")]
		public string Linea { get; set; } = string.Empty;
		[JsonPropertyName("marca")]
		public string Marca { get; set; } = string.Empty;
		[JsonPropertyName("fabricante")]
		public string Fabricante { get; set; } = string.Empty;
		[JsonPropertyName("ubicacion")]
		public string Ubicacion { get; set; } = string.Empty;
		[JsonPropertyName("unidad")]
		public string Unidad { get; set; } = string.Empty;
		[JsonPropertyName("bloqueado")]
		public int Bloqueado { get; set; }
		[JsonPropertyName("paraventa")]
		public int ParaVenta { get; set; }
		[JsonPropertyName("invent")]
		public int Invent { get; set; }
		[JsonPropertyName("granel")]
		public int Granel { get; set; }
		[JsonPropertyName("speso")]
		public int Speso { get; set; }
		[JsonPropertyName("bajocosto")]
		public int BajoCosto { get; set; }
		[JsonPropertyName("impuesto")]
		public string Impuesto { get; set; } = string.Empty;
		[JsonPropertyName("existencia")]
		public int Existencia { get; set; }
		[JsonPropertyName("precio2")]
		public decimal Precio2 { get; set; }
		[JsonPropertyName("precio3")]
		public decimal Precio3 { get; set; }
		[JsonPropertyName("precio4")]
		public decimal Precio4 { get; set; }
		[JsonPropertyName("precio5")]
		public decimal Precio5 { get; set; }
		[JsonPropertyName("precio6")]
		public decimal Precio6 { get; set; }
		[JsonPropertyName("precio7")]
		public decimal Precio7 { get; set; }
		[JsonPropertyName("precio8")]
		public decimal Precio8 { get; set; }
		[JsonPropertyName("precio9")]
		public decimal Precio9 { get; set; }
		[JsonPropertyName("precio10")]
		public decimal Precio10 { get; set; }
		[JsonPropertyName("u1")]
		public string U1 { get; set; } = string.Empty;
		[JsonPropertyName("u2")]
		public string U2 { get; set; } = string.Empty;
		[JsonPropertyName("u3")]
		public string U3 { get; set; } = string.Empty;
		[JsonPropertyName("u4")]
		public string U4 { get; set; } = string.Empty;
		[JsonPropertyName("u5")]
		public string U5 { get; set; } = string.Empty;
		[JsonPropertyName("u6")]
		public string U6 { get; set; } = string.Empty;
		[JsonPropertyName("u7")]
		public string U7 { get; set; } = string.Empty;
		[JsonPropertyName("u8")]
		public string U8 { get; set; } = string.Empty;
		[JsonPropertyName("u9")]
		public string U9 { get; set; } = string.Empty;
		[JsonPropertyName("u10")]
		public string U10 { get; set; } = string.Empty;
		[JsonPropertyName("c2")]
		public string C2 { get; set; } = string.Empty;
		[JsonPropertyName("c3")]
		public string C3 { get; set; } = string.Empty;
		[JsonPropertyName("c4")]
		public string C4 { get; set; } = string.Empty;
		[JsonPropertyName("c5")]
		public string C5 { get; set; } = string.Empty;
		[JsonPropertyName("c6")]
		public string C6 { get; set; } = string.Empty;
		[JsonPropertyName("c7")]
		public string C7 { get; set; } = string.Empty;
		[JsonPropertyName("c8")]
		public string C8 { get; set; } = string.Empty;
		[JsonPropertyName("c9")]
		public string C9 { get; set; } = string.Empty;
		[JsonPropertyName("c10")]
		public string C10 { get; set; } = string.Empty;
		[JsonPropertyName("costoultimo")]
		public decimal CostoUltimo { get; set; }
		[JsonPropertyName("claveprodserv")]
		public string ClaveProdServ { get; set; } = string.Empty;
		[JsonPropertyName("claveunidad")]
		public string ClaveUnidad { get; set; } = string.Empty;
	}
}
