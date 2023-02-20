namespace BankRayo.Models
{
    [Serializable]
    public class FinancialReport
    {
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public decimal InitialBalance { get; set; }
        public bool State { get; set; }
        public decimal Transaction { get; set; }
        public decimal Balance { get; set; }
    }
}
