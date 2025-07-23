namespace SincronizadorCore.Models
{
   using System.Text.Json.Serialization;

   public class LineaModel
   {
	  [JsonPropertyName("linea")]
	  public string Linea { get; set; } = string.Empty;

	  [JsonPropertyName("descripcion")]
	  public string Descrip { get; set; } = string.Empty;

	  [JsonPropertyName("usuario")]
	  public string Usuario { get; set; } = string.Empty;

	  [JsonPropertyName("usufecha")]
	  public long? UsuFecha { get; set; }

	  [JsonPropertyName("usuhora")]
	  public string? UsuHora { get; set; }

	  [JsonPropertyName("numero")]
	  public int? Numero { get; set; }
   }

   public class MarcaModel
   {
	  [JsonPropertyName("marca")]
	  public string Marca { get; set; } = string.Empty;

	  [JsonPropertyName("descripcion")]
	  public string Descrip { get; set; } = string.Empty;

	  [JsonPropertyName("usuario")]
	  public string Usuario { get; set; } = string.Empty;

	  [JsonPropertyName("usufecha")]
	  public long? UsuFecha { get; set; }

	  [JsonPropertyName("usuhora")]
	  public string? UsuHora { get; set; }
   }

   public class ImpuestoModel
   {
	  [JsonPropertyName("nombreImpuesto")]
	  public string Impuesto { get; set; } = string.Empty;

	  [JsonPropertyName("porcentajeImpuesto")]
	  public decimal Valor { get; set; }

	  [JsonPropertyName("usuario")]
	  public string Usuario { get; set; } = string.Empty;

	  [JsonPropertyName("usufecha")]
	  public long? UsuFecha { get; set; }

	  [JsonPropertyName("usuhora")]
	  public string? UsuHora { get; set; }

	  [JsonPropertyName("componente1")]
	  public string Componente1 { get; set; } = string.Empty;

	  [JsonPropertyName("componente2")]
	  public string Componente2 { get; set; } = string.Empty;

	  [JsonPropertyName("componente3")]
	  public string Componente3 { get; set; } = string.Empty;

	  [JsonPropertyName("componente4")]
	  public string Componente4 { get; set; } = string.Empty;

	  [JsonPropertyName("componente5")]
	  public string Componente5 { get; set; } = string.Empty;
   }
}
