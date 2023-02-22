using System.Text.Json.Serialization;

namespace BankRayo.Entities.BusinessEntities
{
    [Serializable]
    public class FinancialReport
    {
        [JsonPropertyName("Fecha")]
        public DateTime Date { get; set; }

        [JsonPropertyName("Cliente")]

        public string? Name { get; set; }

        [JsonPropertyName("Numero Cuenta")]
        public int Number { get; set; }

        [JsonPropertyName("Tipo")]
        public string Type { get; set; }

        [JsonPropertyName("Saldo Inicial")]
        public decimal InitialBalance { get; set; }

        [JsonPropertyName("Estado")]
        public bool State { get; set; }

        [JsonPropertyName("Moviemiento")]
        public decimal Transaction { get; set; }

        [JsonPropertyName("Saldo Disponible")]
        public decimal Balance { get; set; }
    }
}
