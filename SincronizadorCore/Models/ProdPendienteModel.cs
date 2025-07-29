using System.Text.Json.Serialization;

namespace SincronizadorCore.Models
{
    public class ProdPendienteModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("clave")]
        public string Clave { get; set; } = string.Empty;

        [JsonPropertyName("dispositivo")]
        public string Dispositivo { get; set; } = string.Empty;

        [JsonPropertyName("aplicado")]
        public int Aplicado { get; set; }

        [JsonPropertyName("fecha_registro")]
        public string FechaRegistro { get; set; } = string.Empty;

        [JsonPropertyName("fecha_aplicado")]
        public string? FechaAplicado { get; set; }
    }
}
